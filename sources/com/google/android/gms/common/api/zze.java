package com.google.android.gms.common.api;

import android.os.Looper;
import com.google.android.gms.common.api.GoogleApi.zza;
import com.google.android.gms.common.api.internal.zzcz;
import com.google.android.gms.common.api.internal.zzg;
import com.google.android.gms.common.internal.zzbp;

public final class zze {
    private Looper zzakl;
    private zzcz zzfgo;

    public final zze zza(Looper looper) {
        zzbp.zzb((Object) looper, (Object) "Looper must not be null.");
        this.zzakl = looper;
        return this;
    }

    public final zze zza(zzcz zzcz) {
        zzbp.zzb((Object) zzcz, (Object) "StatusExceptionMapper must not be null.");
        this.zzfgo = zzcz;
        return this;
    }

    public final zza zzafm() {
        if (this.zzfgo == null) {
            this.zzfgo = new zzg();
        }
        if (this.zzakl == null) {
            if (Looper.myLooper() != null) {
                this.zzakl = Looper.myLooper();
            } else {
                this.zzakl = Looper.getMainLooper();
            }
        }
        return new zza(this.zzfgo, this.zzakl);
    }
}
