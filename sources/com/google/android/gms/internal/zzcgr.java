package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.ConnectionLifecycleCallback;

final class zzcgr extends zzciw {
    private final zzcj<ConnectionLifecycleCallback> zzjbg;

    zzcgr(zzcj<ConnectionLifecycleCallback> zzcj) {
        this.zzjbg = (zzcj) zzbp.zzu(zzcj);
    }

    public final void zza(zzcjr zzcjr) {
        this.zzjbg.zza(new zzcgs(this, zzcjr));
    }

    public final void zza(zzcjx zzcjx) {
        this.zzjbg.zza(new zzcgt(this, zzcjx));
    }

    public final void zza(zzcjz zzcjz) {
        this.zzjbg.zza(new zzcgu(this, zzcjz));
    }
}
