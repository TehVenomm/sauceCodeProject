package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.List;

final class zzbjb extends zzbiv {
    private /* synthetic */ List zzghi;

    zzbjb(zzbiw zzbiw, GoogleApiClient googleApiClient, List list) {
        this.zzghi = list;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbhj(this.zzghi), new zzbnf(this));
    }
}
