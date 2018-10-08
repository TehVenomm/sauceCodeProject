package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.List;

final class zzbkf extends zzbiv {
    private /* synthetic */ zzbkc zzgig;
    private /* synthetic */ List zzgih;

    zzbkf(zzbkc zzbkc, GoogleApiClient googleApiClient, List list) {
        this.zzgig = zzbkc;
        this.zzgih = list;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbnd(this.zzgig.zzgcx, this.zzgih), new zzbnf(this));
    }
}
