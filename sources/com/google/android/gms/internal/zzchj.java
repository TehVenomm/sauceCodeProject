package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.PayloadCallback;

final class zzchj extends zzcjj {
    private final zzcj<PayloadCallback> zzjbr;

    zzchj(zzcj<PayloadCallback> zzcj) {
        this.zzjbr = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzckf zzckf) {
        this.zzjbr.zza(new zzchk(this, zzckf));
    }

    public final void zza(zzckh zzckh) {
        this.zzjbr.zza(new zzchl(this, zzckh));
    }
}
