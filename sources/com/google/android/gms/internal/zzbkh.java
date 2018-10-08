package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbkh extends zzbiv {
    private /* synthetic */ zzbkc zzgig;

    zzbkh(zzbkc zzbkc, GoogleApiClient googleApiClient) {
        this.zzgig = zzbkc;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbhz(this.zzgig.zzgcx), new zzbnf(this));
    }
}
