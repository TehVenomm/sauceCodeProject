package com.google.android.gms.internal;

import com.google.android.gms.nearby.connection.DiscoveredEndpointInfo;
import com.google.android.gms.nearby.connection.EndpointDiscoveryCallback;

final class zzcha extends zzchi<EndpointDiscoveryCallback> {
    private /* synthetic */ zzckb zzjbn;

    zzcha(zzcgz zzcgz, zzckb zzckb) {
        this.zzjbn = zzckb;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((EndpointDiscoveryCallback) obj).onEndpointFound(this.zzjbn.zzban(), new DiscoveredEndpointInfo(this.zzjbn.getServiceId(), this.zzjbn.getEndpointName()));
    }
}
