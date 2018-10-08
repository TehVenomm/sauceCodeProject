package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.EndpointDiscoveryCallback;

final class zzchb extends zzchi<EndpointDiscoveryCallback> {
    private /* synthetic */ zzckd zzjbo;

    zzchb(zzcgz zzcgz, zzckd zzckd) {
        this.zzjbo = zzckd;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((EndpointDiscoveryCallback) obj).onEndpointLost(this.zzjbo.zzban());
    }
}
