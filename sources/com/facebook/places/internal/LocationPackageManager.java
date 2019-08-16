package com.facebook.places.internal;

import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.internal.Utility;
import com.facebook.places.internal.ScannerException.Type;
import java.util.Locale;
import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;

public class LocationPackageManager {
    private static final String TAG = "LocationPackageManager";

    public interface Listener {
        void onLocationPackage(LocationPackage locationPackage);
    }

    /* access modifiers changed from: private */
    public static void logException(String str, Throwable th) {
        if (FacebookSdk.isDebugEnabled()) {
            Log.e(TAG, str, th);
        }
    }

    /* access modifiers changed from: private */
    public static FutureTask<LocationPackage> newBluetoothScanFuture(final LocationPackageRequestParams locationPackageRequestParams) {
        return new FutureTask<>(new Callable<LocationPackage>() {
            public LocationPackage call() throws Exception {
                BleScanner newBleScanner;
                LocationPackage locationPackage = new LocationPackage();
                try {
                    newBleScanner = ScannerFactory.newBleScanner(FacebookSdk.getApplicationContext(), locationPackageRequestParams);
                    newBleScanner.initAndCheckEligibility();
                    newBleScanner.startScanning();
                    try {
                        Thread.sleep(locationPackageRequestParams.getBluetoothScanDurationMs());
                    } catch (Exception e) {
                    }
                    newBleScanner.stopScanning();
                    int errorCode = newBleScanner.getErrorCode();
                    if (errorCode == 0) {
                        locationPackage.ambientBluetoothLe = newBleScanner.getScanResults();
                        locationPackage.isBluetoothScanningEnabled = true;
                    } else {
                        if (FacebookSdk.isDebugEnabled()) {
                            Utility.logd(LocationPackageManager.TAG, String.format(Locale.getDefault(), "Bluetooth LE scan failed with error: %d", new Object[]{Integer.valueOf(errorCode)}));
                        }
                        locationPackage.isBluetoothScanningEnabled = false;
                    }
                } catch (Exception e2) {
                    LocationPackageManager.logException("Exception scanning for bluetooth beacons", e2);
                    locationPackage.isBluetoothScanningEnabled = false;
                } catch (Throwable th) {
                    newBleScanner.stopScanning();
                    throw th;
                }
                return locationPackage;
            }
        });
    }

    /* access modifiers changed from: private */
    public static FutureTask<LocationPackage> newLocationScanFuture(final LocationScanner locationScanner, LocationPackageRequestParams locationPackageRequestParams) {
        return new FutureTask<>(new Callable<LocationPackage>() {
            public LocationPackage call() throws Exception {
                LocationPackage locationPackage = new LocationPackage();
                try {
                    locationPackage.location = locationScanner.getLocation();
                } catch (ScannerException e) {
                    locationPackage.locationError = e.type;
                    LocationPackageManager.logException("Exception while getting location", e);
                } catch (Exception e2) {
                    locationPackage.locationError = Type.UNKNOWN_ERROR;
                }
                return locationPackage;
            }
        });
    }

    /* access modifiers changed from: private */
    public static FutureTask<LocationPackage> newWifiScanFuture(final LocationPackageRequestParams locationPackageRequestParams) {
        return new FutureTask<>(new Callable<LocationPackage>() {
            public LocationPackage call() throws Exception {
                LocationPackage locationPackage = new LocationPackage();
                try {
                    WifiScanner newWifiScanner = ScannerFactory.newWifiScanner(FacebookSdk.getApplicationContext(), locationPackageRequestParams);
                    newWifiScanner.initAndCheckEligibility();
                    locationPackage.connectedWifi = newWifiScanner.getConnectedWifi();
                    locationPackage.isWifiScanningEnabled = newWifiScanner.isWifiScanningEnabled();
                    if (locationPackage.isWifiScanningEnabled) {
                        locationPackage.ambientWifi = newWifiScanner.getWifiScans();
                    }
                } catch (Exception e) {
                    LocationPackageManager.logException("Exception scanning for wifi access points", e);
                    locationPackage.isWifiScanningEnabled = false;
                }
                return locationPackage;
            }
        });
    }

    public static void requestLocationPackage(final LocationPackageRequestParams locationPackageRequestParams, final Listener listener) {
        FacebookSdk.getExecutor().execute(new Runnable() {
            /* JADX WARNING: Code restructure failed: missing block: B:22:0x008d, code lost:
                r0 = move-exception;
             */
            /* JADX WARNING: Code restructure failed: missing block: B:25:?, code lost:
                com.facebook.places.internal.LocationPackageManager.access$300("Exception scanning for bluetooth beacons", r0);
             */
            /* JADX WARNING: Code restructure failed: missing block: B:26:0x0094, code lost:
                r0 = move-exception;
             */
            /* JADX WARNING: Code restructure failed: missing block: B:27:0x0095, code lost:
                com.facebook.places.internal.LocationPackageManager.access$300("Exception scanning for locations", r0);
                r3.locationError = r0.type;
             */
            /* JADX WARNING: Code restructure failed: missing block: B:28:0x009f, code lost:
                r0 = move-exception;
             */
            /* JADX WARNING: Code restructure failed: missing block: B:31:?, code lost:
                com.facebook.places.internal.LocationPackageManager.access$300("Exception scanning for wifi access points", r0);
             */
            /* JADX WARNING: Code restructure failed: missing block: B:32:0x00a6, code lost:
                r0 = move-exception;
             */
            /* JADX WARNING: Code restructure failed: missing block: B:33:0x00a7, code lost:
                com.facebook.places.internal.LocationPackageManager.access$300("Exception requesting a location package", r0);
             */
            /* JADX WARNING: Failed to process nested try/catch */
            /* JADX WARNING: Removed duplicated region for block: B:26:0x0094 A[ExcHandler: ScannerException (r0v2 'e' com.facebook.places.internal.ScannerException A[CUSTOM_DECLARE]), Splitter:B:1:0x0006] */
            /* Code decompiled incorrectly, please refer to instructions dump. */
            public void run() {
                /*
                    r5 = this;
                    r0 = 0
                    com.facebook.places.internal.LocationPackage r3 = new com.facebook.places.internal.LocationPackage
                    r3.<init>()
                    com.facebook.places.internal.LocationPackageRequestParams r1 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    boolean r1 = r1.isLocationScanEnabled()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    if (r1 == 0) goto L_0x00b6
                    android.content.Context r1 = com.facebook.FacebookSdk.getApplicationContext()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    com.facebook.places.internal.LocationPackageRequestParams r2 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    com.facebook.places.internal.LocationScanner r1 = com.facebook.places.internal.ScannerFactory.newLocationScanner(r1, r2)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    r1.initAndCheckEligibility()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    com.facebook.places.internal.LocationPackageRequestParams r2 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.FutureTask r1 = com.facebook.places.internal.LocationPackageManager.newLocationScanFuture(r1, r2)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.Executor r2 = com.facebook.FacebookSdk.getExecutor()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    r2.execute(r1)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    r2 = r1
                L_0x0029:
                    com.facebook.places.internal.LocationPackageRequestParams r1 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    boolean r1 = r1.isWifiScanEnabled()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    if (r1 == 0) goto L_0x00b4
                    com.facebook.places.internal.LocationPackageRequestParams r1 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.FutureTask r1 = com.facebook.places.internal.LocationPackageManager.newWifiScanFuture(r1)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.Executor r4 = com.facebook.FacebookSdk.getExecutor()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    r4.execute(r1)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                L_0x003e:
                    com.facebook.places.internal.LocationPackageRequestParams r4 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    boolean r4 = r4.isBluetoothScanEnabled()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    if (r4 == 0) goto L_0x0053
                    com.facebook.places.internal.LocationPackageRequestParams r0 = r2     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.FutureTask r0 = com.facebook.places.internal.LocationPackageManager.newBluetoothScanFuture(r0)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    java.util.concurrent.Executor r4 = com.facebook.FacebookSdk.getExecutor()     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    r4.execute(r0)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                L_0x0053:
                    if (r0 == 0) goto L_0x0063
                    java.lang.Object r0 = r0.get()     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                    com.facebook.places.internal.LocationPackage r0 = (com.facebook.places.internal.LocationPackage) r0     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                    java.util.List<com.facebook.places.internal.BluetoothScanResult> r4 = r0.ambientBluetoothLe     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                    r3.ambientBluetoothLe = r4     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                    boolean r0 = r0.isBluetoothScanningEnabled     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                    r3.isBluetoothScanningEnabled = r0     // Catch:{ Exception -> 0x008d, ScannerException -> 0x0094 }
                L_0x0063:
                    if (r1 == 0) goto L_0x0077
                    java.lang.Object r0 = r1.get()     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    com.facebook.places.internal.LocationPackage r0 = (com.facebook.places.internal.LocationPackage) r0     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    boolean r1 = r0.isWifiScanningEnabled     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    r3.isWifiScanningEnabled = r1     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    com.facebook.places.internal.WifiScanResult r1 = r0.connectedWifi     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    r3.connectedWifi = r1     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    java.util.List<com.facebook.places.internal.WifiScanResult> r0 = r0.ambientWifi     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                    r3.ambientWifi = r0     // Catch:{ Exception -> 0x009f, ScannerException -> 0x0094 }
                L_0x0077:
                    if (r2 == 0) goto L_0x0087
                    java.lang.Object r0 = r2.get()     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                    com.facebook.places.internal.LocationPackage r0 = (com.facebook.places.internal.LocationPackage) r0     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                    com.facebook.places.internal.ScannerException$Type r1 = r0.locationError     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                    r3.locationError = r1     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                    android.location.Location r0 = r0.location     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                    r3.location = r0     // Catch:{ Exception -> 0x00ad, ScannerException -> 0x0094 }
                L_0x0087:
                    com.facebook.places.internal.LocationPackageManager$Listener r0 = r3
                    r0.onLocationPackage(r3)
                    return
                L_0x008d:
                    r0 = move-exception
                    java.lang.String r4 = "Exception scanning for bluetooth beacons"
                    com.facebook.places.internal.LocationPackageManager.logException(r4, r0)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    goto L_0x0063
                L_0x0094:
                    r0 = move-exception
                    java.lang.String r1 = "Exception scanning for locations"
                    com.facebook.places.internal.LocationPackageManager.logException(r1, r0)
                    com.facebook.places.internal.ScannerException$Type r0 = r0.type
                    r3.locationError = r0
                    goto L_0x0087
                L_0x009f:
                    r0 = move-exception
                    java.lang.String r1 = "Exception scanning for wifi access points"
                    com.facebook.places.internal.LocationPackageManager.logException(r1, r0)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    goto L_0x0077
                L_0x00a6:
                    r0 = move-exception
                    java.lang.String r1 = "Exception requesting a location package"
                    com.facebook.places.internal.LocationPackageManager.logException(r1, r0)
                    goto L_0x0087
                L_0x00ad:
                    r0 = move-exception
                    java.lang.String r1 = "Exception getting location"
                    com.facebook.places.internal.LocationPackageManager.logException(r1, r0)     // Catch:{ ScannerException -> 0x0094, Exception -> 0x00a6 }
                    goto L_0x0087
                L_0x00b4:
                    r1 = r0
                    goto L_0x003e
                L_0x00b6:
                    r2 = r0
                    goto L_0x0029
                */
                throw new UnsupportedOperationException("Method not decompiled: com.facebook.places.internal.LocationPackageManager.C07301.run():void");
            }
        });
    }
}
