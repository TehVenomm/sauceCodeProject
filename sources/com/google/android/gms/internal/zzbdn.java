package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.PackageManager;
import com.google.android.gms.common.util.zzp;
import java.lang.reflect.InvocationTargetException;

public final class zzbdn {
    private static Context zzfzk;
    private static Boolean zzfzl;

    public static boolean zzcq(Context context) {
        Context applicationContext;
        boolean booleanValue;
        synchronized (zzbdn.class) {
            try {
                applicationContext = context.getApplicationContext();
                if (zzfzk == null || zzfzl == null || zzfzk != applicationContext) {
                    zzfzl = null;
                    if (zzp.isAtLeastO()) {
                        zzfzl = (Boolean) PackageManager.class.getDeclaredMethod("isInstantApp", new Class[0]).invoke(applicationContext.getPackageManager(), new Object[0]);
                    } else {
                        try {
                            context.getClassLoader().loadClass("com.google.android.instantapps.supervisor.InstantAppsRuntime");
                            zzfzl = Boolean.valueOf(true);
                        } catch (ClassNotFoundException e) {
                            zzfzl = Boolean.valueOf(false);
                        }
                    }
                    zzfzk = applicationContext;
                    booleanValue = zzfzl.booleanValue();
                } else {
                    booleanValue = zzfzl.booleanValue();
                }
            } catch (NoSuchMethodException e2) {
                zzfzl = Boolean.valueOf(false);
                zzfzk = applicationContext;
                booleanValue = zzfzl.booleanValue();
                return booleanValue;
            } catch (InvocationTargetException e3) {
                zzfzl = Boolean.valueOf(false);
                zzfzk = applicationContext;
                booleanValue = zzfzl.booleanValue();
                return booleanValue;
            } catch (IllegalAccessException e4) {
                zzfzl = Boolean.valueOf(false);
                zzfzk = applicationContext;
                booleanValue = zzfzl.booleanValue();
                return booleanValue;
            } catch (Throwable th) {
                Class cls = zzbdn.class;
            }
        }
        return booleanValue;
    }
}
