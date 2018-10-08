package com.google.android.gms.internal;

import com.google.android.gms.auth.api.proxy.ProxyResponse;

final class zzatp extends zzasx {
    private /* synthetic */ zzato zzebu;

    zzatp(zzato zzato) {
        this.zzebu = zzato;
    }

    public final void zza(ProxyResponse proxyResponse) {
        this.zzebu.setResult(new zzatq(proxyResponse));
    }
}
