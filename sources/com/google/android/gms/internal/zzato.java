package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.auth.api.proxy.ProxyRequest;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzato extends zzatm {
    private /* synthetic */ ProxyRequest zzebt;

    zzato(zzatn zzatn, GoogleApiClient googleApiClient, ProxyRequest proxyRequest) {
        this.zzebt = proxyRequest;
        super(googleApiClient);
    }

    protected final void zza(Context context, zzatb zzatb) throws RemoteException {
        zzatb.zza(new zzatp(this), this.zzebt);
    }
}
