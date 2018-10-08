package com.google.android.gms.common;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.util.Log;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbdp;

public class zzp {
    private static zzp zzffw;
    private final Context mContext;

    private zzp(Context context) {
        this.mContext = context.getApplicationContext();
    }

    static zzg zza(PackageInfo packageInfo, zzg... zzgArr) {
        int i = 0;
        if (packageInfo.signatures == null) {
            return null;
        }
        if (packageInfo.signatures.length != 1) {
            Log.w("GoogleSignatureVerifier", "Package has more than one signature.");
            return null;
        }
        zzh zzh = new zzh(packageInfo.signatures[0].toByteArray());
        while (i < zzgArr.length) {
            if (zzgArr[i].equals(zzh)) {
                return zzgArr[i];
            }
            i++;
        }
        return null;
    }

    private static boolean zza(PackageInfo packageInfo, boolean z) {
        if (!(packageInfo == null || packageInfo.signatures == null)) {
            zzg zza;
            if (z) {
                zza = zza(packageInfo, zzj.zzffm);
            } else {
                zza = zza(packageInfo, zzj.zzffm[0]);
            }
            if (zza != null) {
                return true;
            }
        }
        return false;
    }

    private static boolean zzb(PackageInfo packageInfo, boolean z) {
        boolean z2 = false;
        if (packageInfo.signatures.length != 1) {
            Log.w("GoogleSignatureVerifier", "Package has more than one signature.");
        } else {
            zzg zzh = new zzh(packageInfo.signatures[0].toByteArray());
            String str = packageInfo.packageName;
            z2 = z ? zzf.zzb(str, zzh) : zzf.zza(str, zzh);
            if (!z2) {
                Log.d("GoogleSignatureVerifier", "Cert not in list. atk=" + z);
            }
        }
        return z2;
    }

    public static zzp zzca(Context context) {
        zzbp.zzu(context);
        synchronized (zzp.class) {
            try {
                if (zzffw == null) {
                    zzf.zzby(context);
                    zzffw = new zzp(context);
                }
            } catch (Throwable th) {
                while (true) {
                    Class cls = zzp.class;
                }
            }
        }
        return zzffw;
    }

    private final boolean zzfr(String str) {
        try {
            PackageInfo packageInfo = zzbdp.zzcs(this.mContext).getPackageInfo(str, 64);
            if (packageInfo == null) {
                return false;
            }
            if (zzo.zzbz(this.mContext)) {
                return zzb(packageInfo, true);
            }
            boolean zzb = zzb(packageInfo, false);
            if (zzb || !zzb(packageInfo, true)) {
                return zzb;
            }
            Log.w("GoogleSignatureVerifier", "Test-keys aren't accepted on this build.");
            return zzb;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    @Deprecated
    public final boolean zza(PackageManager packageManager, int i) {
        String[] packagesForUid = zzbdp.zzcs(this.mContext).getPackagesForUid(i);
        if (packagesForUid == null || packagesForUid.length == 0) {
            return false;
        }
        for (String zzfr : packagesForUid) {
            if (zzfr(zzfr)) {
                return true;
            }
        }
        return false;
    }

    @Deprecated
    public final boolean zza(PackageManager packageManager, PackageInfo packageInfo) {
        if (packageInfo != null) {
            if (zza(packageInfo, false)) {
                return true;
            }
            if (zza(packageInfo, true)) {
                if (zzo.zzbz(this.mContext)) {
                    return true;
                }
                Log.w("GoogleSignatureVerifier", "Test-keys aren't accepted on this build.");
            }
        }
        return false;
    }
}
