package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.nearby.connection.Connections.ConnectionResponseCallback;

final class zzcgy extends zzchi<ConnectionResponseCallback> {
    private /* synthetic */ zzcjv zzjbm;

    zzcgy(zzcgx zzcgx, zzcjv zzcjv) {
        this.zzjbm = zzcjv;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((ConnectionResponseCallback) obj).onConnectionResponse(this.zzjbm.zzbaj(), new Status(this.zzjbm.getStatusCode()), this.zzjbm.zzbam());
    }
}
