package com.google.android.gms.common.wrappers;

import android.content.Context;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.util.PlatformVersion;

@KeepForSdk
public class InstantApps {
    private static Context zzhv;
    private static Boolean zzhw;

    @KeepForSdk
    public static boolean isInstantApp(Context context) {
        boolean booleanValue;
        synchronized (InstantApps.class) {
            try {
                Context applicationContext = context.getApplicationContext();
                if (zzhv == null || zzhw == null || zzhv != applicationContext) {
                    zzhw = null;
                    if (PlatformVersion.isAtLeastO()) {
                        zzhw = Boolean.valueOf(applicationContext.getPackageManager().isInstantApp());
                    } else {
                        context.getClassLoader().loadClass("com.google.android.instantapps.supervisor.InstantAppsRuntime");
                        zzhw = Boolean.valueOf(true);
                    }
                    zzhv = applicationContext;
                    booleanValue = zzhw.booleanValue();
                } else {
                    booleanValue = zzhw.booleanValue();
                }
            } catch (ClassNotFoundException e) {
                zzhw = Boolean.valueOf(false);
            } catch (Throwable th) {
                Class<InstantApps> cls = InstantApps.class;
                throw th;
            }
        }
        return booleanValue;
    }
}
