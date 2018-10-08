package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.Connections.ConnectionRequestListener;

final class zzcgw extends zzchi<ConnectionRequestListener> {
    private /* synthetic */ zzcjt zzjbk;

    zzcgw(zzcgv zzcgv, zzcjt zzcjt) {
        this.zzjbk = zzcjt;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((ConnectionRequestListener) obj).onConnectionRequest(this.zzjbk.zzbaj(), this.zzjbk.zzbak(), this.zzjbk.zzbam());
    }
}
