package com.google.android.gms.nearby.messages.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzar extends zzav {
    private /* synthetic */ zzcj zzhwy;

    zzar(zzak zzak, GoogleApiClient googleApiClient, zzcj zzcj) {
        this.zzhwy = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzah) zzb).zza(zzbbb(), this.zzhwy);
    }
}
