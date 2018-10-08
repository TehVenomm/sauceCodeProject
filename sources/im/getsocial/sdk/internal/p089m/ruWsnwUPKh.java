package im.getsocial.sdk.internal.p089m;

import android.content.Context;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.SystemClock;
import android.telephony.TelephonyManager;
import im.getsocial.sdk.internal.p033c.qZypgoeblR;
import im.getsocial.sdk.internal.p033c.rFvvVpjzZH;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.File;
import java.util.Calendar;
import java.util.Locale;

/* renamed from: im.getsocial.sdk.internal.m.ruWsnwUPKh */
public final class ruWsnwUPKh {
    /* renamed from: a */
    private static final String[] f2224a = new String[]{"com.noshufou.android.su", "com.noshufou.android.su.elite", "eu.chainfire.supersu", "com.koushikdutta.superuser", "com.thirdparty.superuser", "com.yellowes.su", "com.topjohnwu.magisk"};
    /* renamed from: b */
    private static final String[] f2225b = new String[]{"com.koushikdutta.rommanager", "com.koushikdutta.rommanager.license", "com.dimonvideo.luckypatcher", "com.chelpus.lackypatch", "com.ramdroid.appquarantine", "com.ramdroid.appquarantinepro", "com.android.vending.billing.InAppBillingService.COIN", "com.chelpus.luckypatcher"};
    /* renamed from: c */
    private static final String[] f2226c = new String[]{"com.devadvance.rootcloak", "com.devadvance.rootcloakplus", "de.robv.android.xposed.installer", "com.saurik.substrate", "com.zachspong.temprootremovejb", "com.amphoras.hidemyroot", "com.amphoras.hidemyrootadfree", "com.formyhm.hiderootPremium", "com.formyhm.hideroot"};
    /* renamed from: d */
    private static final String[] f2227d = new String[]{"/data/local/", "/data/local/bin/", "/data/local/xbin/", "/sbin/", "/su/bin/", "/system/bin/", "/system/bin/.ext/", "/system/bin/failsafe/", "/system/sd/xbin/", "/system/usr/we-need-root/", "/system/xbin/", "/cache", "/data", "/dev"};
    /* renamed from: e */
    private static final String[] f2228e = new String[]{"su", "magisk", "busybox"};

    private ruWsnwUPKh() {
    }

    /* renamed from: a */
    public static int m2124a(Context context) {
        return context.getResources().getDisplayMetrics().heightPixels;
    }

    /* renamed from: a */
    public static String m2125a() {
        return "ANDROID";
    }

    /* renamed from: a */
    public static String m2126a(rFvvVpjzZH rfvvvpjzzh) {
        String a = rfvvvpjzzh.mo4367a("im.getsocial.sdk.Runtime");
        return a == null ? "NATIVE" : a;
    }

    /* renamed from: a */
    private static String m2127a(String str) {
        return str == null ? "" : str;
    }

    /* renamed from: a */
    private static boolean m2128a(PackageManager packageManager, String str) {
        try {
            packageManager.getPackageInfo(str, 0);
            return true;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    /* renamed from: a */
    private static boolean m2129a(PackageManager packageManager, String... strArr) {
        for (String a : strArr) {
            if (ruWsnwUPKh.m2128a(packageManager, a)) {
                return true;
            }
        }
        return false;
    }

    /* renamed from: b */
    public static int m2130b(Context context) {
        return context.getResources().getDisplayMetrics().widthPixels;
    }

    /* renamed from: b */
    public static String m2131b() {
        return ruWsnwUPKh.m2127a(VERSION.RELEASE);
    }

    /* renamed from: b */
    public static String m2132b(rFvvVpjzZH rfvvvpjzzh) {
        return ruWsnwUPKh.m2127a(rfvvvpjzzh.mo4367a("im.getsocial.sdk.RuntimeVersion"));
    }

    /* renamed from: c */
    public static float m2133c(Context context) {
        return context.getResources().getDisplayMetrics().density;
    }

    /* renamed from: c */
    public static String m2134c() {
        return Calendar.getInstance().getTimeZone().getID();
    }

    /* renamed from: c */
    public static String m2135c(rFvvVpjzZH rfvvvpjzzh) {
        return ruWsnwUPKh.m2127a(rfvvvpjzzh.mo4367a("im.getsocial.sdk.WrapperVersion"));
    }

    /* renamed from: d */
    public static long m2136d() {
        return System.currentTimeMillis() / 1000;
    }

    /* renamed from: d */
    public static String m2137d(Context context) {
        return context.getApplicationInfo().loadLabel(context.getPackageManager()).toString();
    }

    /* renamed from: e */
    public static long m2138e() {
        return SystemClock.uptimeMillis() / 1000;
    }

    /* renamed from: e */
    public static String m2139e(Context context) {
        return context.getApplicationInfo().packageName;
    }

    /* renamed from: f */
    public static String m2140f() {
        return Locale.getDefault().getLanguage() + "-" + Locale.getDefault().getCountry();
    }

    /* renamed from: f */
    public static String m2141f(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionName;
        } catch (NameNotFoundException e) {
            return "";
        }
    }

    /* renamed from: g */
    public static String m2142g() {
        return ruWsnwUPKh.m2127a(Build.MANUFACTURER);
    }

    /* renamed from: g */
    public static String m2143g(Context context) {
        int i = 0;
        try {
            i = context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionCode;
        } catch (NameNotFoundException e) {
        }
        return Integer.toString(i);
    }

    /* renamed from: h */
    public static String m2144h() {
        return ruWsnwUPKh.m2127a(Build.MODEL);
    }

    /* renamed from: h */
    public static String m2145h(Context context) {
        TelephonyManager telephonyManager = (TelephonyManager) context.getSystemService("phone");
        return telephonyManager == null ? "" : ruWsnwUPKh.m2127a(telephonyManager.getNetworkOperatorName());
    }

    /* renamed from: i */
    public static String m2146i(Context context) {
        return upgqDBbsrL.m2150a(context);
    }

    /* renamed from: j */
    public static boolean m2147j(Context context) {
        PackageManager packageManager = context.getPackageManager();
        try {
            return packageManager.getPackageInfo(context.getPackageName(), 0).firstInstallTime == packageManager.getPackageInfo(context.getPackageName(), 0).lastUpdateTime;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    /* renamed from: k */
    public static qZypgoeblR m2148k(Context context) {
        NetworkInfo activeNetworkInfo = ((ConnectivityManager) context.getSystemService("connectivity")).getActiveNetworkInfo();
        return activeNetworkInfo == null ? new qZypgoeblR() : new qZypgoeblR(activeNetworkInfo.getTypeName(), activeNetworkInfo.getSubtypeName());
    }

    /* renamed from: l */
    public static boolean m2149l(Context context) {
        String a = ruWsnwUPKh.m2127a(Build.MODEL);
        boolean z = a.startsWith("sdk") || CommonUtils.GOOGLE_SDK.equals(a) || a.contains("Emulator") || a.contains("Android SDK");
        String str = Build.TAGS;
        if (!z && str != null && str.contains("test-keys")) {
            return true;
        }
        PackageManager packageManager = context.getPackageManager();
        boolean z2 = ruWsnwUPKh.m2129a(packageManager, f2224a) || ruWsnwUPKh.m2129a(packageManager, f2225b) || ruWsnwUPKh.m2129a(packageManager, f2226c);
        if (z2) {
            return true;
        }
        if (!z) {
            String[] strArr = f2228e;
            for (int i = 0; i < 3; i++) {
                String str2 = strArr[i];
                String[] strArr2 = f2227d;
                for (int i2 = 0; i2 < 14; i2++) {
                    if (new File(strArr2[i2], str2).exists()) {
                        z2 = true;
                        break;
                    }
                }
                z2 = false;
                if (z2) {
                    z = true;
                    break;
                }
            }
            z = false;
            if (z) {
                return true;
            }
        }
        return false;
    }
}
