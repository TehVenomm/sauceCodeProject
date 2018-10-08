package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.SubscribeCallback;
import com.google.android.gms.nearby.messages.internal.zzab;

public final class zzcma extends zzab implements zzclq<SubscribeCallback> {
    private static final zzclv<SubscribeCallback> zzjhc = new zzcmb();
    private final zzcj<SubscribeCallback> zzjgz;

    public zzcma(zzcj<SubscribeCallback> zzcj) {
        this.zzjgz = zzcj;
    }

    public final void onExpired() {
        this.zzjgz.zza(zzjhc);
    }

    public final zzcj<SubscribeCallback> zzbbb() {
        return this.zzjgz;
    }
}
