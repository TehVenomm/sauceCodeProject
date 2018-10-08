package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.Connections.EndpointDiscoveryListener;

final class zzche extends zzchi<EndpointDiscoveryListener> {
    private /* synthetic */ zzckd zzjbo;

    zzche(zzchc zzchc, zzckd zzckd) {
        this.zzjbo = zzckd;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((EndpointDiscoveryListener) obj).onEndpointLost(this.zzjbo.zzban());
    }
}
