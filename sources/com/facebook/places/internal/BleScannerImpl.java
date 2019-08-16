package com.facebook.places.internal;

import android.annotation.TargetApi;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.le.BluetoothLeScanner;
import android.bluetooth.le.ScanCallback;
import android.bluetooth.le.ScanRecord;
import android.bluetooth.le.ScanResult;
import android.bluetooth.le.ScanSettings.Builder;
import android.content.Context;
import android.os.Build.VERSION;
import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.internal.Validate;
import com.facebook.places.internal.ScannerException.Type;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

@TargetApi(21)
public class BleScannerImpl implements BleScanner {
    private static final byte[] EDDYSTONE_PREFIX = fromHexString("16aafe");
    private static final byte[] GRAVITY_PREFIX = fromHexString("17ffab01");
    private static final byte[] IBEACON_PREFIX = fromHexString("ff4c000215");
    private static final String TAG = "BleScannerImpl";
    private BluetoothAdapter bluetoothAdapter;
    private BluetoothLeScanner bluetoothLeScanner;
    private Context context;
    /* access modifiers changed from: private */
    public int errorCode;
    private boolean isScanInProgress;
    private LocationPackageRequestParams params;
    private ScanCallBackImpl scanCallBack;
    /* access modifiers changed from: private */
    public final List<BluetoothScanResult> scanResults = new ArrayList();

    private class ScanCallBackImpl extends ScanCallback {
        private ScanCallBackImpl() {
        }

        public void onBatchScanResults(List<ScanResult> list) {
            super.onBatchScanResults(list);
            try {
                synchronized (BleScannerImpl.this.scanResults) {
                    for (ScanResult access$400 : list) {
                        BluetoothScanResult access$4002 = BleScannerImpl.newBluetoothScanResult(access$400);
                        if (access$4002 != null) {
                            BleScannerImpl.this.scanResults.add(access$4002);
                        }
                    }
                }
            } catch (Exception e) {
                BleScannerImpl.logException("Exception in ble scan callback", e);
            }
        }

        public void onScanFailed(int i) {
            super.onScanFailed(i);
            BleScannerImpl.this.errorCode = i;
        }

        public void onScanResult(int i, ScanResult scanResult) {
            super.onScanResult(i, scanResult);
            try {
                synchronized (BleScannerImpl.this.scanResults) {
                    BluetoothScanResult access$400 = BleScannerImpl.newBluetoothScanResult(scanResult);
                    if (access$400 != null) {
                        BleScannerImpl.this.scanResults.add(access$400);
                    }
                }
            } catch (Exception e) {
                BleScannerImpl.logException("Exception in ble scan callback", e);
            }
        }
    }

    BleScannerImpl(Context context2, LocationPackageRequestParams locationPackageRequestParams) {
        this.context = context2;
        this.params = locationPackageRequestParams;
    }

    private static String formatPayload(byte[] bArr) {
        if (bArr == null || bArr.length == 0) {
            return null;
        }
        return toHexString(bArr, getPayloadLength(bArr));
    }

    private static byte[] fromHexString(String str) {
        int length = str.length();
        byte[] bArr = new byte[(length / 2)];
        for (int i = 0; i < length; i += 2) {
            bArr[i / 2] = (byte) ((byte) ((Character.digit(str.charAt(i), 16) << 4) + Character.digit(str.charAt(i + 1), 16)));
        }
        return bArr;
    }

    private static int getPayloadLength(byte[] bArr) {
        int i = 0;
        while (i < bArr.length) {
            byte b = bArr[i];
            if (b == 0) {
                return i;
            }
            if (b < 0) {
                return bArr.length;
            }
            i += b + 1;
        }
        return bArr.length;
    }

    private static boolean isAdvPacketBeacon(byte[] bArr, int i) {
        return isArrayContained(bArr, i + 1, IBEACON_PREFIX) || isArrayContained(bArr, i + 1, EDDYSTONE_PREFIX) || isArrayContained(bArr, i, GRAVITY_PREFIX) || isAltBeacon(bArr, i);
    }

    private static boolean isAltBeacon(byte[] bArr, int i) {
        if (i + 5 >= bArr.length) {
            return false;
        }
        return bArr[i] == 27 && bArr[i + 1] == -1 && bArr[i + 4] == -66 && bArr[i + 5] == -84;
    }

    private static boolean isArrayContained(byte[] bArr, int i, byte[] bArr2) {
        int length = bArr2.length;
        if (i + length > bArr.length) {
            return false;
        }
        for (int i2 = 0; i2 < length; i2++) {
            if (bArr[i + i2] != bArr2[i2]) {
                return false;
            }
        }
        return true;
    }

    private static boolean isBeacon(byte[] bArr) {
        if (bArr == null) {
            return false;
        }
        int length = bArr.length;
        int i = 0;
        while (i < length) {
            byte b = bArr[i];
            if (b <= 0) {
                return false;
            }
            int i2 = b + 1;
            if (i + i2 > length) {
                return false;
            }
            if (isAdvPacketBeacon(bArr, i)) {
                return true;
            }
            i += i2;
        }
        return false;
    }

    /* access modifiers changed from: private */
    public static void logException(String str, Exception exc) {
        if (FacebookSdk.isDebugEnabled()) {
            Log.e(TAG, str, exc);
        }
    }

    /* access modifiers changed from: private */
    public static BluetoothScanResult newBluetoothScanResult(ScanResult scanResult) {
        ScanRecord scanRecord = scanResult.getScanRecord();
        if (isBeacon(scanRecord.getBytes())) {
            return new BluetoothScanResult(formatPayload(scanRecord.getBytes()), scanResult.getRssi(), scanResult.getTimestampNanos());
        }
        return null;
    }

    private static String toHexString(byte[] bArr, int i) {
        StringBuffer stringBuffer = new StringBuffer();
        if (i < 0 || i > bArr.length) {
            i = bArr.length;
        }
        for (int i2 = 0; i2 < i; i2++) {
            stringBuffer.append(String.format("%02x", new Object[]{Byte.valueOf(bArr[i2])}));
        }
        return stringBuffer.toString();
    }

    private void waitForMainLooper(long j) {
        try {
            final Object obj = new Object();
            synchronized (obj) {
                new Handler(Looper.getMainLooper()).post(new Runnable() {
                    public void run() {
                        try {
                            synchronized (obj) {
                                obj.notify();
                            }
                        } catch (Exception e) {
                            BleScannerImpl.logException("Exception waiting for main looper", e);
                        }
                    }
                });
                obj.wait(j);
            }
        } catch (Exception e) {
            logException("Exception waiting for main looper", e);
        }
    }

    public int getErrorCode() {
        int i;
        synchronized (this) {
            i = this.errorCode;
        }
        return i;
    }

    public List<BluetoothScanResult> getScanResults() {
        ArrayList arrayList;
        synchronized (this) {
            synchronized (this.scanResults) {
                int bluetoothMaxScanResults = this.params.getBluetoothMaxScanResults();
                if (this.scanResults.size() > bluetoothMaxScanResults) {
                    arrayList = new ArrayList(bluetoothMaxScanResults);
                    Collections.sort(this.scanResults, new Comparator<BluetoothScanResult>() {
                        public int compare(BluetoothScanResult bluetoothScanResult, BluetoothScanResult bluetoothScanResult2) {
                            return bluetoothScanResult2.rssi - bluetoothScanResult.rssi;
                        }
                    });
                    arrayList.addAll(this.scanResults.subList(0, bluetoothMaxScanResults));
                } else {
                    arrayList = new ArrayList(this.scanResults.size());
                    arrayList.addAll(this.scanResults);
                }
            }
        }
        return arrayList;
    }

    public void initAndCheckEligibility() throws ScannerException {
        synchronized (this) {
            if (VERSION.SDK_INT < 21) {
                throw new ScannerException(Type.NOT_SUPPORTED);
            } else if (!Validate.hasBluetoothPermission(this.context)) {
                throw new ScannerException(Type.PERMISSION_DENIED);
            } else if (!Validate.hasLocationPermission(this.context)) {
                throw new ScannerException(Type.PERMISSION_DENIED);
            } else {
                this.bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
                if (this.bluetoothAdapter == null || !this.bluetoothAdapter.isEnabled()) {
                    throw new ScannerException(Type.DISABLED);
                }
                this.bluetoothLeScanner = this.bluetoothAdapter.getBluetoothLeScanner();
                if (this.bluetoothLeScanner == null) {
                    throw new ScannerException(Type.UNKNOWN_ERROR);
                }
            }
        }
    }

    public void startScanning() throws ScannerException {
        synchronized (this) {
            if (this.isScanInProgress) {
                throw new ScannerException(Type.SCAN_ALREADY_IN_PROGRESS);
            }
            this.scanCallBack = new ScanCallBackImpl();
            this.isScanInProgress = true;
            this.errorCode = 0;
            synchronized (this.scanResults) {
                this.scanResults.clear();
            }
            if (this.bluetoothLeScanner == null) {
                throw new ScannerException(Type.UNKNOWN_ERROR);
            }
            try {
                Builder builder = new Builder();
                builder.setScanMode(2);
                builder.setReportDelay(0);
                this.bluetoothLeScanner.startScan(null, builder.build(), this.scanCallBack);
                this.isScanInProgress = true;
            } catch (Exception e) {
                throw new ScannerException(Type.UNKNOWN_ERROR);
            }
        }
    }

    public void stopScanning() {
        synchronized (this) {
            this.bluetoothLeScanner.flushPendingScanResults(this.scanCallBack);
            this.bluetoothLeScanner.stopScan(this.scanCallBack);
            waitForMainLooper(this.params.getBluetoothFlushResultsTimeoutMs());
            this.isScanInProgress = false;
        }
    }
}
