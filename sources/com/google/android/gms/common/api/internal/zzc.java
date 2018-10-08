package com.google.android.gms.common.api.internal;

import android.os.DeadObjectException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

public final class zzc<A extends zzm<? extends Result, zzb>> extends zza {
    private A zzfhw;

    public zzc(int i, A a) {
        super(i);
        this.zzfhw = a;
    }

    public final void zza(@NonNull zzah zzah, boolean z) {
        zzah.zza(this.zzfhw, z);
    }

    public final void zza(zzbr<?> zzbr) throws DeadObjectException {
        this.zzfhw.zzb(zzbr.zzagm());
    }

    public final void zzq(@NonNull Status status) {
        this.zzfhw.zzs(status);
    }
}
