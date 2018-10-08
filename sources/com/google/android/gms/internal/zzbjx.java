package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbjx extends zzbkb {
    private /* synthetic */ zzbjw zzgic;

    zzbjx(zzbjw zzbjw, GoogleApiClient googleApiClient) {
        this.zzgic = zzbjw;
        super(zzbjw, googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zzb(new zzbjz(this.zzgic, this, null));
    }
}
