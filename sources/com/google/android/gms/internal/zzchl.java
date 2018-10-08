package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.PayloadCallback;

final class zzchl extends zzchi<PayloadCallback> {
    private /* synthetic */ zzckh zzjbs;

    zzchl(zzchj zzchj, zzckh zzckh) {
        this.zzjbs = zzckh;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((PayloadCallback) obj).onPayloadTransferUpdate(this.zzjbs.zzbaj(), this.zzjbs.zzbaq());
    }
}
