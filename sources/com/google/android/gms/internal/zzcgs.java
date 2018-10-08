package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.ConnectionInfo;
import com.google.android.gms.nearby.connection.ConnectionLifecycleCallback;

final class zzcgs extends zzchi<ConnectionLifecycleCallback> {
    private /* synthetic */ zzcjr zzjbh;

    zzcgs(zzcgr zzcgr, zzcjr zzcjr) {
        this.zzjbh = zzcjr;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((ConnectionLifecycleCallback) obj).onConnectionInitiated(this.zzjbh.zzbaj(), new ConnectionInfo(this.zzjbh.zzbak(), this.zzjbh.getAuthenticationToken(), this.zzjbh.zzbal()));
    }
}
