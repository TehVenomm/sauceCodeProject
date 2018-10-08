package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.Connections.EndpointDiscoveryListener;

final class zzchd extends zzchi<EndpointDiscoveryListener> {
    private /* synthetic */ zzckb zzjbn;

    zzchd(zzchc zzchc, zzckb zzckb) {
        this.zzjbn = zzckb;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((EndpointDiscoveryListener) obj).onEndpointFound(this.zzjbn.zzban(), this.zzjbn.getServiceId(), this.zzjbn.getEndpointName());
    }
}
