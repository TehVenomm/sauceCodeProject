package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.auth.api.proxy.ProxyApi.ProxyResult;
import com.google.android.gms.auth.api.zzd;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzm;

abstract class zzatm extends zzm<ProxyResult, zzasy> {
    public zzatm(GoogleApiClient googleApiClient) {
        super(zzd.API, googleApiClient);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((ProxyResult) obj);
    }

    protected abstract void zza(Context context, zzatb zzatb) throws RemoteException;

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzasy zzasy = (zzasy) zzb;
        zza(zzasy.getContext(), (zzatb) zzasy.zzajj());
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return new zzatq(status);
    }
}
