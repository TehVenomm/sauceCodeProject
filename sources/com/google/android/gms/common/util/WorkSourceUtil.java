package com.google.android.gms.common.util;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.Process;
import android.os.WorkSource;
import android.support.annotation.Nullable;
import android.util.Log;
import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.wrappers.Wrappers;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

@KeepForSdk
public class WorkSourceUtil {
    private static final int zzhj = Process.myUid();
    private static final Method zzhk = zzx();
    private static final Method zzhl = zzy();
    private static final Method zzhm = zzz();
    private static final Method zzhn = zzaa();
    private static final Method zzho = zzab();
    private static final Method zzhp = zzac();
    private static final Method zzhq = zzad();

    private WorkSourceUtil() {
    }

    @Nullable
    @KeepForSdk
    public static WorkSource fromPackage(Context context, @Nullable String str) {
        if (context == null || context.getPackageManager() == null || str == null) {
            return null;
        }
        try {
            ApplicationInfo applicationInfo = Wrappers.packageManager(context).getApplicationInfo(str, 0);
            if (applicationInfo != null) {
                return zza(applicationInfo.uid, str);
            }
            String valueOf = String.valueOf(str);
            Log.e("WorkSourceUtil", valueOf.length() != 0 ? "Could not get applicationInfo from package: ".concat(valueOf) : new String("Could not get applicationInfo from package: "));
            return null;
        } catch (NameNotFoundException e) {
            String valueOf2 = String.valueOf(str);
            Log.e("WorkSourceUtil", valueOf2.length() != 0 ? "Could not find package: ".concat(valueOf2) : new String("Could not find package: "));
            return null;
        }
    }

    @KeepForSdk
    public static WorkSource fromPackageAndModuleExperimentalPi(Context context, String str, String str2) {
        WorkSource workSource = null;
        if (context == null || context.getPackageManager() == null || str2 == null || str == null) {
            Log.w("WorkSourceUtil", "Unexpected null arguments");
        } else {
            int zzd = zzd(context, str);
            if (zzd >= 0) {
                workSource = new WorkSource();
                if (zzhp == null || zzhq == null) {
                    zza(workSource, zzd, str);
                } else {
                    try {
                        Object invoke = zzhp.invoke(workSource, new Object[0]);
                        if (zzd != zzhj) {
                            zzhq.invoke(invoke, new Object[]{Integer.valueOf(zzd), str});
                        }
                        zzhq.invoke(invoke, new Object[]{Integer.valueOf(zzhj), str2});
                    } catch (Exception e) {
                        Log.w("WorkSourceUtil", "Unable to assign chained blame through WorkSource", e);
                    }
                }
            }
        }
        return workSource;
    }

    @KeepForSdk
    public static List<String> getNames(@Nullable WorkSource workSource) {
        int zza = workSource == null ? 0 : zza(workSource);
        if (zza == 0) {
            return Collections.emptyList();
        }
        ArrayList arrayList = new ArrayList();
        for (int i = 0; i < zza; i++) {
            String zza2 = zza(workSource, i);
            if (!Strings.isEmptyOrWhitespace(zza2)) {
                arrayList.add(zza2);
            }
        }
        return arrayList;
    }

    @KeepForSdk
    public static boolean hasWorkSourcePermission(Context context) {
        return (context == null || context.getPackageManager() == null || Wrappers.packageManager(context).checkPermission("android.permission.UPDATE_DEVICE_STATS", context.getPackageName()) != 0) ? false : true;
    }

    private static int zza(WorkSource workSource) {
        if (zzhm != null) {
            try {
                return ((Integer) zzhm.invoke(workSource, new Object[0])).intValue();
            } catch (Exception e) {
                Log.wtf("WorkSourceUtil", "Unable to assign blame through WorkSource", e);
            }
        }
        return 0;
    }

    private static WorkSource zza(int i, String str) {
        WorkSource workSource = new WorkSource();
        zza(workSource, i, str);
        return workSource;
    }

    @Nullable
    private static String zza(WorkSource workSource, int i) {
        if (zzho != null) {
            try {
                return (String) zzho.invoke(workSource, new Object[]{Integer.valueOf(i)});
            } catch (Exception e) {
                Log.wtf("WorkSourceUtil", "Unable to assign blame through WorkSource", e);
            }
        }
        return null;
    }

    private static void zza(WorkSource workSource, int i, @Nullable String str) {
        if (zzhl != null) {
            if (str == null) {
                str = "";
            }
            try {
                zzhl.invoke(workSource, new Object[]{Integer.valueOf(i), str});
            } catch (Exception e) {
                Log.wtf("WorkSourceUtil", "Unable to assign blame through WorkSource", e);
            }
        } else if (zzhk != null) {
            try {
                zzhk.invoke(workSource, new Object[]{Integer.valueOf(i)});
            } catch (Exception e2) {
                Log.wtf("WorkSourceUtil", "Unable to assign blame through WorkSource", e2);
            }
        }
    }

    private static Method zzaa() {
        boolean z = false;
        try {
            return WorkSource.class.getMethod("get", new Class[]{Integer.TYPE});
        } catch (Exception e) {
            return z;
        }
    }

    private static Method zzab() {
        Method method = null;
        if (!PlatformVersion.isAtLeastJellyBeanMR2()) {
            return method;
        }
        try {
            return WorkSource.class.getMethod("getName", new Class[]{Integer.TYPE});
        } catch (Exception e) {
            return method;
        }
    }

    private static final Method zzac() {
        Method method = null;
        if (!PlatformVersion.isAtLeastP()) {
            return method;
        }
        try {
            return WorkSource.class.getMethod("createWorkChain", new Class[0]);
        } catch (Exception e) {
            Log.w("WorkSourceUtil", "Missing WorkChain API createWorkChain", e);
            return method;
        }
    }

    @SuppressLint({"PrivateApi"})
    private static final Method zzad() {
        Method method = null;
        if (!PlatformVersion.isAtLeastP()) {
            return method;
        }
        try {
            return Class.forName("android.os.WorkSource$WorkChain").getMethod("addNode", new Class[]{Integer.TYPE, String.class});
        } catch (Exception e) {
            Log.w("WorkSourceUtil", "Missing WorkChain class", e);
            return method;
        }
    }

    private static int zzd(Context context, String str) {
        try {
            ApplicationInfo applicationInfo = Wrappers.packageManager(context).getApplicationInfo(str, 0);
            if (applicationInfo != null) {
                return applicationInfo.uid;
            }
            String valueOf = String.valueOf(str);
            Log.e("WorkSourceUtil", valueOf.length() != 0 ? "Could not get applicationInfo from package: ".concat(valueOf) : new String("Could not get applicationInfo from package: "));
            return -1;
        } catch (NameNotFoundException e) {
            String valueOf2 = String.valueOf(str);
            Log.e("WorkSourceUtil", valueOf2.length() != 0 ? "Could not find package: ".concat(valueOf2) : new String("Could not find package: "));
            return -1;
        }
    }

    private static Method zzx() {
        boolean z = false;
        try {
            return WorkSource.class.getMethod("add", new Class[]{Integer.TYPE});
        } catch (Exception e) {
            return z;
        }
    }

    private static Method zzy() {
        Method method = null;
        if (!PlatformVersion.isAtLeastJellyBeanMR2()) {
            return method;
        }
        try {
            return WorkSource.class.getMethod("add", new Class[]{Integer.TYPE, String.class});
        } catch (Exception e) {
            return method;
        }
    }

    private static Method zzz() {
        boolean z = false;
        try {
            return WorkSource.class.getMethod("size", new Class[0]);
        } catch (Exception e) {
            return z;
        }
    }
}
