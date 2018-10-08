package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbjg extends zzbiv {
    private /* synthetic */ zzbjc zzghm;

    zzbjg(zzbjc zzbjc, GoogleApiClient googleApiClient) {
        this.zzghm = zzbjc;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbhn(this.zzghm.zzghj.getRequestId(), false), new zzbnf(this));
    }
}
