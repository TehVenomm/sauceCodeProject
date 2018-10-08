package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.ConnectionLifecycleCallback;

final class zzcgu extends zzchi<ConnectionLifecycleCallback> {
    private /* synthetic */ zzcjz zzjbj;

    zzcgu(zzcgr zzcgr, zzcjz zzcjz) {
        this.zzjbj = zzcjz;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((ConnectionLifecycleCallback) obj).onDisconnected(this.zzjbj.zzbaj());
    }
}
