package com.google.android.gms.dynamite;

import android.content.Context;
import com.google.android.gms.dynamite.DynamiteModule.zzd;

final class zzc implements zzd {
    zzc() {
    }

    public final zzj zza(Context context, String str, zzi zzi) throws com.google.android.gms.dynamite.DynamiteModule.zzc {
        zzj zzj = new zzj();
        zzj.zzgpp = zzi.zzae(context, str);
        if (zzj.zzgpp != 0) {
            zzj.zzgpr = -1;
        } else {
            zzj.zzgpq = zzi.zzb(context, str, true);
            if (zzj.zzgpq != 0) {
                zzj.zzgpr = 1;
            }
        }
        return zzj;
    }
}
