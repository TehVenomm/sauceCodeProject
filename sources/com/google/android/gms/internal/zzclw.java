package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.PublishCallback;
import com.google.android.gms.nearby.messages.internal.zzv;

public final class zzclw extends zzv implements zzclq<PublishCallback> {
    private static final zzclv<PublishCallback> zzjhc = new zzclx();
    private final zzcj<PublishCallback> zzjgz;

    public zzclw(zzcj<PublishCallback> zzcj) {
        this.zzjgz = zzcj;
    }

    public final void onExpired() {
        this.zzjgz.zza(zzjhc);
    }

    public final zzcj<PublishCallback> zzbbb() {
        return this.zzjgz;
    }
}
