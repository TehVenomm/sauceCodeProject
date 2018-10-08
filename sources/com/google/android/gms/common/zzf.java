package com.google.android.gms.common;

import android.content.Context;
import android.util.Log;
import com.google.android.gms.common.internal.zzaz;
import com.google.android.gms.common.internal.zzba;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.dynamite.DynamiteModule;

final class zzf {
    private static zzaz zzfff;
    private static final Object zzffg = new Object();
    private static Context zzffh;

    static boolean zza(String str, zzg zzg) {
        return zza(str, zzg, false);
    }

    private static boolean zza(String str, zzg zzg, boolean z) {
        boolean z2 = false;
        if (zzaex()) {
            zzbp.zzu(zzffh);
            try {
                z2 = zzfff.zza(new zzm(str, zzg, z), zzn.zzw(zzffh.getPackageManager()));
            } catch (Throwable e) {
                Log.e("GoogleCertificates", "Failed to get Google certificates from remote", e);
            }
        }
        return z2;
    }

    private static boolean zzaex() {
        boolean z = true;
        if (zzfff == null) {
            zzbp.zzu(zzffh);
            synchronized (zzffg) {
                if (zzfff == null) {
                    try {
                        zzfff = zzba.zzal(DynamiteModule.zza(zzffh, DynamiteModule.zzgpi, "com.google.android.gms.googlecertificates").zzgv("com.google.android.gms.common.GoogleCertificatesImpl"));
                    } catch (Throwable e) {
                        Log.e("GoogleCertificates", "Failed to load com.google.android.gms.googlecertificates", e);
                        z = false;
                    }
                }
            }
        }
        return z;
    }

    static boolean zzb(String str, zzg zzg) {
        return zza(str, zzg, true);
    }

    static void zzby(Context context) {
        synchronized (zzf.class) {
            try {
                if (zzffh != null) {
                    Log.w("GoogleCertificates", "GoogleCertificates has been initialized already");
                } else if (context != null) {
                    zzffh = context.getApplicationContext();
                }
            } catch (Throwable th) {
                Class cls = zzf.class;
            }
        }
    }
}
