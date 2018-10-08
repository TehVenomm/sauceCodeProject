package com.google.android.gms.common;

import android.annotation.TargetApi;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageInstaller.SessionInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.res.Resources;
import android.os.Bundle;
import android.os.UserManager;
import android.util.Log;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.C0603R;
import com.google.android.gms.common.internal.zzbe;
import com.google.android.gms.common.util.zzi;
import com.google.android.gms.common.util.zzp;
import com.google.android.gms.common.util.zzv;
import java.util.concurrent.atomic.AtomicBoolean;

public class zzo {
    @Deprecated
    public static final String GOOGLE_PLAY_SERVICES_PACKAGE = "com.google.android.gms";
    @Deprecated
    public static final int GOOGLE_PLAY_SERVICES_VERSION_CODE = 11200000;
    public static final String GOOGLE_PLAY_STORE_PACKAGE = "com.android.vending";
    private static boolean zzffq = false;
    private static boolean zzffr = false;
    private static boolean zzffs = false;
    private static boolean zzfft = false;
    static final AtomicBoolean zzffu = new AtomicBoolean();
    private static final AtomicBoolean zzffv = new AtomicBoolean();

    zzo() {
    }

    @Deprecated
    public static PendingIntent getErrorPendingIntent(int i, Context context, int i2) {
        return zze.zzaew().getErrorResolutionPendingIntent(context, i, i2);
    }

    @Deprecated
    public static String getErrorString(int i) {
        return ConnectionResult.getStatusString(i);
    }

    public static Context getRemoteContext(Context context) {
        try {
            return context.createPackageContext("com.google.android.gms", 3);
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    public static Resources getRemoteResource(Context context) {
        try {
            return context.getPackageManager().getResourcesForApplication("com.google.android.gms");
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    @Deprecated
    public static int isGooglePlayServicesAvailable(Context context) {
        PackageManager packageManager = context.getPackageManager();
        try {
            context.getResources().getString(C0603R.string.common_google_play_services_unknown_issue);
        } catch (Throwable th) {
            Log.e("GooglePlayServicesUtil", "The Google Play services resources were not found. Check your project configuration to ensure that the resources are included.");
        }
        if (!("com.google.android.gms".equals(context.getPackageName()) || zzffv.get())) {
            int zzch = zzbe.zzch(context);
            if (zzch == 0) {
                throw new IllegalStateException("A required meta-data tag in your app's AndroidManifest.xml does not exist.  You must have the following declaration within the <application> element:     <meta-data android:name=\"com.google.android.gms.version\" android:value=\"@integer/google_play_services_version\" />");
            } else if (zzch != GOOGLE_PLAY_SERVICES_VERSION_CODE) {
                throw new IllegalStateException(new StringBuilder(String.valueOf("com.google.android.gms.version").length() + 290).append("The meta-data tag in your app's AndroidManifest.xml does not have the right value.  Expected ").append(GOOGLE_PLAY_SERVICES_VERSION_CODE).append(" but found ").append(zzch).append(".  You must have the following declaration within the <application> element:     <meta-data android:name=\"").append("com.google.android.gms.version").append("\" android:value=\"@integer/google_play_services_version\" />").toString());
            }
        }
        int i = (zzi.zzck(context) || zzi.zzcm(context)) ? 0 : 1;
        PackageInfo packageInfo = null;
        if (i != 0) {
            try {
                packageInfo = packageManager.getPackageInfo("com.android.vending", 8256);
            } catch (NameNotFoundException e) {
                Log.w("GooglePlayServicesUtil", "Google Play Store is missing.");
                return 9;
            }
        }
        try {
            PackageInfo packageInfo2 = packageManager.getPackageInfo("com.google.android.gms", 64);
            zzp.zzca(context);
            if (i != 0) {
                if (zzp.zza(packageInfo, zzj.zzffm) == null) {
                    Log.w("GooglePlayServicesUtil", "Google Play Store signature invalid.");
                    return 9;
                }
                if (zzp.zza(packageInfo2, zzp.zza(packageInfo, zzj.zzffm)) == null) {
                    Log.w("GooglePlayServicesUtil", "Google Play services signature invalid.");
                    return 9;
                }
            } else if (zzp.zza(packageInfo2, zzj.zzffm) == null) {
                Log.w("GooglePlayServicesUtil", "Google Play services signature invalid.");
                return 9;
            }
            if (packageInfo2.versionCode / 1000 < GOOGLE_PLAY_SERVICES_VERSION_CODE / 1000) {
                Log.w("GooglePlayServicesUtil", "Google Play services out of date.  Requires " + GOOGLE_PLAY_SERVICES_VERSION_CODE + " but found " + packageInfo2.versionCode);
                return 2;
            }
            ApplicationInfo applicationInfo = packageInfo2.applicationInfo;
            if (applicationInfo == null) {
                try {
                    applicationInfo = packageManager.getApplicationInfo("com.google.android.gms", 0);
                } catch (Throwable e2) {
                    Log.wtf("GooglePlayServicesUtil", "Google Play services missing when getting application info.", e2);
                    return 1;
                }
            }
            return !applicationInfo.enabled ? 3 : 0;
        } catch (NameNotFoundException e3) {
            Log.w("GooglePlayServicesUtil", "Google Play services is missing.");
            return 1;
        }
    }

    @Deprecated
    public static boolean isUserRecoverableError(int i) {
        switch (i) {
            case 1:
            case 2:
            case 3:
            case 9:
                return true;
            default:
                return false;
        }
    }

    @TargetApi(19)
    @Deprecated
    public static boolean zzb(Context context, int i, String str) {
        return zzv.zzb(context, i, str);
    }

    @Deprecated
    public static void zzbk(Context context) throws GooglePlayServicesRepairableException, GooglePlayServicesNotAvailableException {
        int isGooglePlayServicesAvailable = zze.zzaew().isGooglePlayServicesAvailable(context);
        if (isGooglePlayServicesAvailable != 0) {
            zze.zzaew();
            Intent zza = zze.zza(context, isGooglePlayServicesAvailable, "e");
            Log.e("GooglePlayServicesUtil", "GooglePlayServices not available due to error " + isGooglePlayServicesAvailable);
            if (zza == null) {
                throw new GooglePlayServicesNotAvailableException(isGooglePlayServicesAvailable);
            }
            throw new GooglePlayServicesRepairableException(isGooglePlayServicesAvailable, "Google Play Services not available", zza);
        }
    }

    @Deprecated
    public static void zzbw(Context context) {
        if (!zzffu.getAndSet(true)) {
            try {
                NotificationManager notificationManager = (NotificationManager) context.getSystemService("notification");
                if (notificationManager != null) {
                    notificationManager.cancel(10436);
                }
            } catch (SecurityException e) {
            }
        }
    }

    @Deprecated
    public static int zzbx(Context context) {
        int i = 0;
        try {
            return context.getPackageManager().getPackageInfo("com.google.android.gms", 0).versionCode;
        } catch (NameNotFoundException e) {
            Log.w("GooglePlayServicesUtil", "Google Play services is missing.");
            return i;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static boolean zzbz(android.content.Context r7) {
        /*
        r0 = 0;
        r1 = 1;
        r2 = zzfft;
        if (r2 != 0) goto L_0x002d;
    L_0x0006:
        r2 = com.google.android.gms.internal.zzbdp.zzcs(r7);	 Catch:{ NameNotFoundException -> 0x0041 }
        r3 = "com.google.android.gms";
        r4 = 64;
        r2 = r2.getPackageInfo(r3, r4);	 Catch:{ NameNotFoundException -> 0x0041 }
        if (r2 == 0) goto L_0x003d;
    L_0x0014:
        com.google.android.gms.common.zzp.zzca(r7);	 Catch:{ NameNotFoundException -> 0x0041 }
        r3 = 1;
        r3 = new com.google.android.gms.common.zzg[r3];	 Catch:{ NameNotFoundException -> 0x0041 }
        r4 = 0;
        r5 = com.google.android.gms.common.zzj.zzffm;	 Catch:{ NameNotFoundException -> 0x0041 }
        r6 = 1;
        r5 = r5[r6];	 Catch:{ NameNotFoundException -> 0x0041 }
        r3[r4] = r5;	 Catch:{ NameNotFoundException -> 0x0041 }
        r2 = com.google.android.gms.common.zzp.zza(r2, r3);	 Catch:{ NameNotFoundException -> 0x0041 }
        if (r2 == 0) goto L_0x003d;
    L_0x0028:
        r2 = 1;
        zzffs = r2;	 Catch:{ NameNotFoundException -> 0x0041 }
    L_0x002b:
        zzfft = r1;
    L_0x002d:
        r2 = zzffs;
        if (r2 != 0) goto L_0x003b;
    L_0x0031:
        r2 = "user";
        r3 = android.os.Build.TYPE;
        r2 = r2.equals(r3);
        if (r2 != 0) goto L_0x003c;
    L_0x003b:
        r0 = r1;
    L_0x003c:
        return r0;
    L_0x003d:
        r2 = 0;
        zzffs = r2;	 Catch:{ NameNotFoundException -> 0x0041 }
        goto L_0x002b;
    L_0x0041:
        r2 = move-exception;
        r3 = "GooglePlayServicesUtil";
        r4 = "Cannot find Google Play services package name.";
        android.util.Log.w(r3, r4, r2);	 Catch:{ all -> 0x004c }
        zzfft = r1;
        goto L_0x002d;
    L_0x004c:
        r0 = move-exception;
        zzfft = r1;
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.zzo.zzbz(android.content.Context):boolean");
    }

    @Deprecated
    public static boolean zze(Context context, int i) {
        return i == 18 ? true : i == 1 ? zzy(context, "com.google.android.gms") : false;
    }

    @Deprecated
    public static boolean zzf(Context context, int i) {
        return zzv.zzf(context, i);
    }

    @TargetApi(21)
    static boolean zzy(Context context, String str) {
        boolean equals = str.equals("com.google.android.gms");
        if (zzp.zzalj()) {
            for (SessionInfo appPackageName : context.getPackageManager().getPackageInstaller().getAllSessions()) {
                if (str.equals(appPackageName.getAppPackageName())) {
                    return true;
                }
            }
        }
        try {
            ApplicationInfo applicationInfo = context.getPackageManager().getApplicationInfo(str, 8192);
            if (equals) {
                return applicationInfo.enabled;
            }
            if (applicationInfo.enabled) {
                Object obj;
                if (zzp.zzalg()) {
                    Bundle applicationRestrictions = ((UserManager) context.getSystemService("user")).getApplicationRestrictions(context.getPackageName());
                    if (applicationRestrictions != null && ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equals(applicationRestrictions.getString("restricted_profile"))) {
                        obj = 1;
                        if (obj == null) {
                            return true;
                        }
                    }
                }
                obj = null;
                if (obj == null) {
                    return true;
                }
            }
            return false;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
