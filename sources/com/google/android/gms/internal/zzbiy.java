package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbiy extends zzbiv {
    private /* synthetic */ zzbkr zzghe;
    private /* synthetic */ zzbmz zzghf;

    zzbiy(zzbiw zzbiw, GoogleApiClient googleApiClient, zzbmz zzbmz, zzbkr zzbkr) {
        this.zzghf = zzbmz;
        this.zzghe = zzbkr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(this.zzghf, this.zzghe, null, new zzbnf(this));
    }
}
