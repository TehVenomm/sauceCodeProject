package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.Connections.MessageListener;

final class zzchh extends zzchi<MessageListener> {
    private /* synthetic */ zzcjz zzjbj;

    zzchh(zzchf zzchf, zzcjz zzcjz) {
        this.zzjbj = zzcjz;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((MessageListener) obj).onDisconnected(this.zzjbj.zzbaj());
    }
}
