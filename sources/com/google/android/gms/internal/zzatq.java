package com.google.android.gms.internal;

import com.google.android.gms.auth.api.proxy.ProxyApi.ProxyResult;
import com.google.android.gms.auth.api.proxy.ProxyResponse;
import com.google.android.gms.common.api.Status;

final class zzatq implements ProxyResult {
    private Status mStatus;
    private ProxyResponse zzebv;

    public zzatq(ProxyResponse proxyResponse) {
        this.zzebv = proxyResponse;
        this.mStatus = Status.zzfhp;
    }

    public zzatq(Status status) {
        this.mStatus = status;
    }

    public final ProxyResponse getResponse() {
        return this.zzebv;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
