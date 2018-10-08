package com.google.android.gms.dynamite;

import android.content.Context;
import com.google.android.gms.dynamite.DynamiteModule.zzc;
import com.google.android.gms.dynamite.DynamiteModule.zzd;

final class zzb implements zzd {
    zzb() {
    }

    public final zzj zza(Context context, String str, zzi zzi) throws zzc {
        zzj zzj = new zzj();
        zzj.zzgpq = zzi.zzb(context, str, true);
        if (zzj.zzgpq != 0) {
            zzj.zzgpr = 1;
        } else {
            zzj.zzgpp = zzi.zzae(context, str);
            if (zzj.zzgpp != 0) {
                zzj.zzgpr = -1;
            }
        }
        return zzj;
    }
}
