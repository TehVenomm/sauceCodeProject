package com.google.android.gms.nearby.messages.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzau extends zzav {
    private /* synthetic */ zzcj zzjge;

    zzau(zzak zzak, GoogleApiClient googleApiClient, zzcj zzcj) {
        this.zzjge = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzah) zzb).zzb(zzbbb(), this.zzjge);
    }
}
