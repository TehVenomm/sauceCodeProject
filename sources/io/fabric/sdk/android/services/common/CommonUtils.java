package io.fabric.sdk.android.services.common;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager.MemoryInfo;
import android.app.ActivityManager.RunningAppProcessInfo;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.SharedPreferences;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.hardware.SensorManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.Debug;
import android.os.StatFs;
import android.provider.Settings.Secure;
import android.text.TextUtils;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.Fabric;
import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.Closeable;
import java.io.File;
import java.io.FileReader;
import java.io.Flushable;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.security.InvalidKeyException;
import java.security.Key;
import java.security.MessageDigest;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Scanner;
import java.util.regex.Pattern;
import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;
import jp.colopl.drapro.LocalNotificationAlarmReceiver;

public class CommonUtils {
    static final int BYTES_IN_A_GIGABYTE = 1073741824;
    static final int BYTES_IN_A_KILOBYTE = 1024;
    static final int BYTES_IN_A_MEGABYTE = 1048576;
    private static final String CLS_SHARED_PREFERENCES_NAME = "com.crashlytics.prefs";
    static final boolean CLS_TRACE_DEFAULT = false;
    static final String CLS_TRACE_PREFERENCE_NAME = "com.crashlytics.Trace";
    static final String CRASHLYTICS_BUILD_ID = "com.crashlytics.android.build_id";
    public static final int DEVICE_STATE_BETAOS = 8;
    public static final int DEVICE_STATE_COMPROMISEDLIBRARIES = 32;
    public static final int DEVICE_STATE_DEBUGGERATTACHED = 4;
    public static final int DEVICE_STATE_ISSIMULATOR = 1;
    public static final int DEVICE_STATE_JAILBROKEN = 2;
    public static final int DEVICE_STATE_VENDORINTERNAL = 16;
    static final String ENCRYPTION_AES = "AES/ECB/PKCS7Padding";
    static final String FABRIC_BUILD_ID = "io.fabric.android.build_id";
    public static final Comparator<File> FILE_MODIFIED_COMPARATOR = new C12341();
    public static final String GOOGLE_SDK = "google_sdk";
    private static final char[] HEX_VALUES = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};
    private static final boolean LOGGING_DISABLED_DEFAULT = false;
    private static final String LOGGING_DISABLED_KEY = "com.crashlytics.SilenceCrashlyticsLogCat";
    private static final String LOG_PRIORITY_NAME_ASSERT = "A";
    private static final String LOG_PRIORITY_NAME_DEBUG = "D";
    private static final String LOG_PRIORITY_NAME_ERROR = "E";
    private static final String LOG_PRIORITY_NAME_INFO = "I";
    private static final String LOG_PRIORITY_NAME_UNKNOWN = "?";
    private static final String LOG_PRIORITY_NAME_VERBOSE = "V";
    private static final String LOG_PRIORITY_NAME_WARN = "W";
    public static final String MD5_INSTANCE = "MD5";
    public static final String SDK = "sdk";
    public static final String SHA1_INSTANCE = "SHA-1";
    private static final long UNCALCULATED_TOTAL_RAM = -1;
    private static Boolean clsTrace = null;
    private static Boolean loggingEnabled = null;
    private static long totalRamInBytes = -1;

    /* renamed from: io.fabric.sdk.android.services.common.CommonUtils$1 */
    static final class C12341 implements Comparator<File> {
        C12341() {
        }

        public int compare(File file, File file2) {
            return (int) (file.lastModified() - file2.lastModified());
        }
    }

    enum Architecture {
        X86_32,
        X86_64,
        ARM_UNKNOWN,
        PPC,
        PPC64,
        ARMV6,
        ARMV7,
        UNKNOWN,
        ARMV7S,
        ARM64;
        
        private static final Map<String, Architecture> matcher = null;

        static {
            matcher = new HashMap(4);
            matcher.put("armeabi-v7a", ARMV7);
            matcher.put("armeabi", ARMV6);
            matcher.put("x86", X86_32);
        }

        static Architecture getValue() {
            Object obj = Build.CPU_ABI;
            if (TextUtils.isEmpty(obj)) {
                Fabric.getLogger().mo4753d("Fabric", "Architecture#getValue()::Build.CPU_ABI returned null or empty");
                return UNKNOWN;
            }
            Architecture architecture = (Architecture) matcher.get(obj.toLowerCase(Locale.US));
            return architecture == null ? UNKNOWN : architecture;
        }
    }

    public static long calculateFreeRamInBytes(Context context) {
        MemoryInfo memoryInfo = new MemoryInfo();
        ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getMemoryInfo(memoryInfo);
        return memoryInfo.availMem;
    }

    public static long calculateUsedDiskSpaceInBytes(String str) {
        StatFs statFs = new StatFs(str);
        long blockSize = (long) statFs.getBlockSize();
        return (((long) statFs.getBlockCount()) * blockSize) - (((long) statFs.getAvailableBlocks()) * blockSize);
    }

    public static boolean canTryConnection(Context context) {
        if (!checkPermission(context, "android.permission.ACCESS_NETWORK_STATE")) {
            return true;
        }
        NetworkInfo activeNetworkInfo = ((ConnectivityManager) context.getSystemService("connectivity")).getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnectedOrConnecting();
    }

    public static boolean checkPermission(Context context, String str) {
        return context.checkCallingOrSelfPermission(str) == 0;
    }

    public static void closeOrLog(Closeable closeable, String str) {
        if (closeable != null) {
            try {
                closeable.close();
            } catch (Throwable e) {
                Fabric.getLogger().mo4756e("Fabric", str, e);
            }
        }
    }

    public static void closeQuietly(Closeable closeable) {
        if (closeable != null) {
            try {
                closeable.close();
            } catch (RuntimeException e) {
                throw e;
            } catch (Exception e2) {
            }
        }
    }

    static long convertMemInfoToBytes(String str, String str2, int i) {
        return Long.parseLong(str.split(str2)[0].trim()) * ((long) i);
    }

    public static void copyStream(InputStream inputStream, OutputStream outputStream, byte[] bArr) throws IOException {
        while (true) {
            int read = inputStream.read(bArr);
            if (read != -1) {
                outputStream.write(bArr, 0, read);
            } else {
                return;
            }
        }
    }

    @SuppressLint({"GetInstance"})
    public static Cipher createCipher(int i, String str) throws InvalidKeyException {
        if (str.length() < 32) {
            throw new InvalidKeyException("Key must be at least 32 bytes.");
        }
        Key secretKeySpec = new SecretKeySpec(str.getBytes(), 0, 32, ENCRYPTION_AES);
        try {
            Cipher instance = Cipher.getInstance(ENCRYPTION_AES);
            instance.init(i, secretKeySpec);
            return instance;
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Could not create Cipher for AES/ECB/PKCS7Padding - should never happen.", e);
            throw new RuntimeException(e);
        }
    }

    public static String createInstanceIdFrom(String... strArr) {
        if (strArr != null) {
            if (strArr.length == 0) {
                return null;
            }
            List<String> arrayList = new ArrayList();
            for (String str : strArr) {
                if (str != null) {
                    arrayList.add(str.replace("-", "").toLowerCase(Locale.US));
                }
            }
            Collections.sort(arrayList);
            StringBuilder stringBuilder = new StringBuilder();
            for (String append : arrayList) {
                stringBuilder.append(append);
            }
            String append2 = stringBuilder.toString();
            if (append2.length() > 0) {
                return sha1(append2);
            }
        }
        return null;
    }

    public static byte[] dehexify(String str) {
        int length = str.length();
        byte[] bArr = new byte[(length / 2)];
        for (int i = 0; i < length; i += 2) {
            bArr[i / 2] = (byte) ((byte) ((Character.digit(str.charAt(i), 16) << 4) + Character.digit(str.charAt(i + 1), 16)));
        }
        return bArr;
    }

    public static String extractFieldFromSystemFile(File file, String str) {
        Closeable bufferedReader;
        Throwable e;
        Throwable th;
        String str2 = null;
        if (file.exists()) {
            try {
                String[] split;
                bufferedReader = new BufferedReader(new FileReader(file), 1024);
                while (true) {
                    try {
                        CharSequence readLine = bufferedReader.readLine();
                        if (readLine == null) {
                            break;
                        }
                        split = Pattern.compile("\\s*:\\s*").split(readLine, 2);
                        if (split.length > 1 && split[0].equals(str)) {
                            break;
                        }
                    } catch (Exception e2) {
                        e = e2;
                    } catch (Throwable th2) {
                        th = th2;
                    }
                }
                str2 = split[1];
                closeOrLog(bufferedReader, "Failed to close system file reader.");
            } catch (Exception e3) {
                e = e3;
                bufferedReader = null;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Error parsing " + file, e);
                    closeOrLog(bufferedReader, "Failed to close system file reader.");
                    return str2;
                } catch (Throwable th3) {
                    th = th3;
                    closeOrLog(bufferedReader, "Failed to close system file reader.");
                    throw th;
                }
            } catch (Throwable e4) {
                bufferedReader = null;
                th = e4;
                closeOrLog(bufferedReader, "Failed to close system file reader.");
                throw th;
            }
        }
        return str2;
    }

    @TargetApi(16)
    public static void finishAffinity(Activity activity, int i) {
        if (activity != null) {
            if (VERSION.SDK_INT >= 16) {
                activity.finishAffinity();
                return;
            }
            activity.setResult(i);
            activity.finish();
        }
    }

    @TargetApi(16)
    public static void finishAffinity(Context context, int i) {
        if (context instanceof Activity) {
            finishAffinity((Activity) context, i);
        }
    }

    public static void flushOrLog(Flushable flushable, String str) {
        if (flushable != null) {
            try {
                flushable.flush();
            } catch (Throwable e) {
                Fabric.getLogger().mo4756e("Fabric", str, e);
            }
        }
    }

    public static String getAppIconHashOrNull(Context context) {
        Throwable e;
        Throwable th;
        String str = null;
        Closeable openRawResource;
        try {
            openRawResource = context.getResources().openRawResource(getAppIconResourceId(context));
            try {
                String sha1 = sha1((InputStream) openRawResource);
                if (!isNullOrEmpty(sha1)) {
                    str = sha1;
                }
                closeOrLog(openRawResource, "Failed to close icon input stream.");
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4756e("Fabric", "Could not calculate hash for app icon.", e);
                    closeOrLog(openRawResource, "Failed to close icon input stream.");
                    return str;
                } catch (Throwable th2) {
                    th = th2;
                    closeOrLog(openRawResource, "Failed to close icon input stream.");
                    throw th;
                }
            }
        } catch (Exception e3) {
            e = e3;
            openRawResource = null;
            Fabric.getLogger().mo4756e("Fabric", "Could not calculate hash for app icon.", e);
            closeOrLog(openRawResource, "Failed to close icon input stream.");
            return str;
        } catch (Throwable e4) {
            openRawResource = null;
            th = e4;
            closeOrLog(openRawResource, "Failed to close icon input stream.");
            throw th;
        }
        return str;
    }

    public static int getAppIconResourceId(Context context) {
        return context.getApplicationContext().getApplicationInfo().icon;
    }

    public static RunningAppProcessInfo getAppProcessInfo(String str, Context context) {
        List<RunningAppProcessInfo> runningAppProcesses = ((ActivityManager) context.getSystemService(LocalNotificationAlarmReceiver.EXTRA_ACTIVITY)).getRunningAppProcesses();
        if (runningAppProcesses != null) {
            for (RunningAppProcessInfo runningAppProcessInfo : runningAppProcesses) {
                if (runningAppProcessInfo.processName.equals(str)) {
                    return runningAppProcessInfo;
                }
            }
        }
        return null;
    }

    public static float getBatteryLevel(Context context) {
        Intent registerReceiver = context.registerReceiver(null, new IntentFilter("android.intent.action.BATTERY_CHANGED"));
        return ((float) registerReceiver.getIntExtra(Param.LEVEL, -1)) / ((float) registerReceiver.getIntExtra("scale", -1));
    }

    public static int getBatteryVelocity(Context context, boolean z) {
        float batteryLevel = getBatteryLevel(context);
        return !z ? 1 : (!z || ((double) batteryLevel) < 99.0d) ? (!z || ((double) batteryLevel) >= 99.0d) ? 0 : 2 : 3;
    }

    public static byte[] getBitmapBytes(Bitmap bitmap) {
        OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        bitmap.compress(CompressFormat.PNG, 100, byteArrayOutputStream);
        return byteArrayOutputStream.toByteArray();
    }

    public static boolean getBooleanResourceValue(Context context, String str, boolean z) {
        if (context == null) {
            return z;
        }
        Resources resources = context.getResources();
        if (resources == null) {
            return z;
        }
        int resourcesIdentifier = getResourcesIdentifier(context, str, "bool");
        if (resourcesIdentifier > 0) {
            return resources.getBoolean(resourcesIdentifier);
        }
        int resourcesIdentifier2 = getResourcesIdentifier(context, str, "string");
        return resourcesIdentifier2 > 0 ? Boolean.parseBoolean(context.getString(resourcesIdentifier2)) : z;
    }

    public static int getCpuArchitectureInt() {
        return Architecture.getValue().ordinal();
    }

    public static int getDeviceState(Context context) {
        int i = 0;
        if (isEmulator(context)) {
            i = 1;
        }
        if (isRooted(context)) {
            i |= 2;
        }
        return isDebuggerAttached() ? i | 4 : i;
    }

    public static boolean getProximitySensorEnabled(Context context) {
        return (isEmulator(context) || ((SensorManager) context.getSystemService("sensor")).getDefaultSensor(8) == null) ? false : true;
    }

    public static String getResourcePackageName(Context context) {
        int i = context.getApplicationContext().getApplicationInfo().icon;
        return i > 0 ? context.getResources().getResourcePackageName(i) : context.getPackageName();
    }

    public static int getResourcesIdentifier(Context context, String str, String str2) {
        return context.getResources().getIdentifier(str, str2, getResourcePackageName(context));
    }

    public static SharedPreferences getSharedPrefs(Context context) {
        return context.getSharedPreferences(CLS_SHARED_PREFERENCES_NAME, 0);
    }

    public static String getStringsFileValue(Context context, String str) {
        int resourcesIdentifier = getResourcesIdentifier(context, str, "string");
        return resourcesIdentifier > 0 ? context.getString(resourcesIdentifier) : "";
    }

    public static long getTotalRamInBytes() {
        long j;
        synchronized (CommonUtils.class) {
            String toUpperCase;
            try {
                if (totalRamInBytes == -1) {
                    Object extractFieldFromSystemFile = extractFieldFromSystemFile(new File("/proc/meminfo"), "MemTotal");
                    if (TextUtils.isEmpty(extractFieldFromSystemFile)) {
                        j = 0;
                    } else {
                        toUpperCase = extractFieldFromSystemFile.toUpperCase(Locale.US);
                        if (toUpperCase.endsWith("KB")) {
                            j = convertMemInfoToBytes(toUpperCase, "KB", 1024);
                        } else if (toUpperCase.endsWith("MB")) {
                            j = convertMemInfoToBytes(toUpperCase, "MB", 1048576);
                        } else if (toUpperCase.endsWith("GB")) {
                            j = convertMemInfoToBytes(toUpperCase, "GB", BYTES_IN_A_GIGABYTE);
                        } else {
                            Fabric.getLogger().mo4753d("Fabric", "Unexpected meminfo format while computing RAM: " + toUpperCase);
                            j = 0;
                        }
                    }
                    totalRamInBytes = j;
                }
            } catch (Throwable e) {
                Fabric.getLogger().mo4756e("Fabric", "Unexpected meminfo format while computing RAM: " + toUpperCase, e);
                j = 0;
            } catch (Throwable th) {
                Class cls = CommonUtils.class;
            }
            j = totalRamInBytes;
        }
        return j;
    }

    private static String hash(InputStream inputStream, String str) {
        try {
            MessageDigest instance = MessageDigest.getInstance(SHA1_INSTANCE);
            byte[] bArr = new byte[1024];
            while (true) {
                int read = inputStream.read(bArr);
                if (read == -1) {
                    return hexify(instance.digest());
                }
                instance.update(bArr, 0, read);
            }
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Could not calculate hash for app icon.", e);
            return "";
        }
    }

    private static String hash(String str, String str2) {
        return hash(str.getBytes(), str2);
    }

    private static String hash(byte[] bArr, String str) {
        try {
            MessageDigest instance = MessageDigest.getInstance(str);
            instance.update(bArr);
            return hexify(instance.digest());
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Could not create hashing algorithm: " + str + ", returning empty string.", e);
            return "";
        }
    }

    public static String hexify(byte[] bArr) {
        char[] cArr = new char[(bArr.length * 2)];
        for (int i = 0; i < bArr.length; i++) {
            int i2 = bArr[i] & 255;
            cArr[i * 2] = (char) HEX_VALUES[i2 >>> 4];
            cArr[(i * 2) + 1] = (char) HEX_VALUES[i2 & 15];
        }
        return new String(cArr);
    }

    public static void hideKeyboard(Context context, View view) {
        InputMethodManager inputMethodManager = (InputMethodManager) context.getSystemService("input_method");
        if (inputMethodManager != null) {
            inputMethodManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
        }
    }

    public static boolean isAppDebuggable(Context context) {
        return (context.getApplicationInfo().flags & 2) != 0;
    }

    public static boolean isClsTrace(Context context) {
        if (clsTrace == null) {
            clsTrace = Boolean.valueOf(getBooleanResourceValue(context, CLS_TRACE_PREFERENCE_NAME, false));
        }
        return clsTrace.booleanValue();
    }

    public static boolean isDebuggerAttached() {
        return Debug.isDebuggerConnected() || Debug.waitingForDebugger();
    }

    public static boolean isEmulator(Context context) {
        return "sdk".equals(Build.PRODUCT) || GOOGLE_SDK.equals(Build.PRODUCT) || Secure.getString(context.getContentResolver(), "android_id") == null;
    }

    public static boolean isLoggingEnabled(Context context) {
        boolean z = false;
        if (loggingEnabled == null) {
            if (!getBooleanResourceValue(context, LOGGING_DISABLED_KEY, false)) {
                z = true;
            }
            loggingEnabled = Boolean.valueOf(z);
        }
        return loggingEnabled.booleanValue();
    }

    public static boolean isNullOrEmpty(String str) {
        return str == null || str.length() == 0;
    }

    public static boolean isRooted(Context context) {
        boolean isEmulator = isEmulator(context);
        String str = Build.TAGS;
        if ((!isEmulator && str != null && str.contains("test-keys")) || new File("/system/app/Superuser.apk").exists()) {
            return true;
        }
        return !isEmulator && new File("/system/xbin/su").exists();
    }

    public static void logControlled(Context context, int i, String str, String str2) {
        if (isClsTrace(context)) {
            Fabric.getLogger().log(i, "Fabric", str2);
        }
    }

    public static void logControlled(Context context, String str) {
        if (isClsTrace(context)) {
            Fabric.getLogger().mo4753d("Fabric", str);
        }
    }

    public static void logControlledError(Context context, String str, Throwable th) {
        if (isClsTrace(context)) {
            Fabric.getLogger().mo4755e("Fabric", str);
        }
    }

    public static void logOrThrowIllegalArgumentException(String str, String str2) {
        if (Fabric.isDebuggable()) {
            throw new IllegalArgumentException(str2);
        }
        Fabric.getLogger().mo4766w(str, str2);
    }

    public static void logOrThrowIllegalStateException(String str, String str2) {
        if (Fabric.isDebuggable()) {
            throw new IllegalStateException(str2);
        }
        Fabric.getLogger().mo4766w(str, str2);
    }

    public static String logPriorityToString(int i) {
        switch (i) {
            case 2:
                return LOG_PRIORITY_NAME_VERBOSE;
            case 3:
                return LOG_PRIORITY_NAME_DEBUG;
            case 4:
                return LOG_PRIORITY_NAME_INFO;
            case 5:
                return LOG_PRIORITY_NAME_WARN;
            case 6:
                return LOG_PRIORITY_NAME_ERROR;
            case 7:
                return LOG_PRIORITY_NAME_ASSERT;
            default:
                return LOG_PRIORITY_NAME_UNKNOWN;
        }
    }

    public static String md5(String str) {
        return hash(str, MD5_INSTANCE);
    }

    public static String md5(byte[] bArr) {
        return hash(bArr, MD5_INSTANCE);
    }

    public static void openKeyboard(Context context, View view) {
        InputMethodManager inputMethodManager = (InputMethodManager) context.getSystemService("input_method");
        if (inputMethodManager != null) {
            inputMethodManager.showSoftInputFromInputMethod(view.getWindowToken(), 0);
        }
    }

    public static String padWithZerosToMaxIntWidth(int i) {
        if (i < 0) {
            throw new IllegalArgumentException("value must be zero or greater");
        }
        return String.format(Locale.US, "%1$10s", new Object[]{Integer.valueOf(i)}).replace(' ', '0');
    }

    public static String resolveBuildId(Context context) {
        int resourcesIdentifier = getResourcesIdentifier(context, FABRIC_BUILD_ID, "string");
        if (resourcesIdentifier == 0) {
            resourcesIdentifier = getResourcesIdentifier(context, CRASHLYTICS_BUILD_ID, "string");
        }
        if (resourcesIdentifier == 0) {
            return null;
        }
        String string = context.getResources().getString(resourcesIdentifier);
        Fabric.getLogger().mo4753d("Fabric", "Build ID is: " + string);
        return string;
    }

    public static String sha1(InputStream inputStream) {
        return hash(inputStream, SHA1_INSTANCE);
    }

    public static String sha1(String str) {
        return hash(str, SHA1_INSTANCE);
    }

    public static String sha1(byte[] bArr) {
        return hash(bArr, SHA1_INSTANCE);
    }

    public static String streamToString(InputStream inputStream) throws IOException {
        Scanner useDelimiter = new Scanner(inputStream).useDelimiter("\\A");
        return useDelimiter.hasNext() ? useDelimiter.next() : "";
    }

    public static boolean stringsEqualIncludingNull(String str, String str2) {
        return str == str2 ? true : str != null ? str.equals(str2) : false;
    }
}
