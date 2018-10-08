package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.nearby.connection.ConnectionLifecycleCallback;
import com.google.android.gms.nearby.connection.ConnectionResolution;

final class zzcgt extends zzchi<ConnectionLifecycleCallback> {
    private /* synthetic */ zzcjx zzjbi;

    zzcgt(zzcgr zzcgr, zzcjx zzcjx) {
        this.zzjbi = zzcjx;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((ConnectionLifecycleCallback) obj).onConnectionResult(this.zzjbi.zzbaj(), new ConnectionResolution(new Status(this.zzjbi.getStatusCode())));
    }
}
