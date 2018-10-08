package com.google.android.gms.internal;

import com.google.android.gms.nearby.messages.StatusCallback;

final class zzclz extends zzclv<StatusCallback> {
    private /* synthetic */ boolean zzjhd;

    zzclz(zzcly zzcly, boolean z) {
        this.zzjhd = z;
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((StatusCallback) obj).onPermissionChanged(this.zzjhd);
    }
}
