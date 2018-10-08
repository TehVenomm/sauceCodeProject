package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbix extends zzbiv {
    private /* synthetic */ zzbhg zzghd;
    private /* synthetic */ zzbkr zzghe;

    zzbix(zzbiw zzbiw, GoogleApiClient googleApiClient, zzbhg zzbhg, zzbkr zzbkr) {
        this.zzghd = zzbhg;
        this.zzghe = zzbkr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(this.zzghd, this.zzghe, null, new zzbnf(this));
    }
}
