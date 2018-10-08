package com.google.android.gms.nearby.messages.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.SubscribeOptions;

final class zzap extends zzav {
    private /* synthetic */ zzcj zzhwy;
    private /* synthetic */ zzcj zzjgg;
    private /* synthetic */ SubscribeOptions zzjgi;

    zzap(zzak zzak, GoogleApiClient googleApiClient, zzcj zzcj, zzcj zzcj2, SubscribeOptions subscribeOptions) {
        this.zzhwy = zzcj;
        this.zzjgg = zzcj2;
        this.zzjgi = subscribeOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzah) zzb).zza(zzbbb(), this.zzhwy, this.zzjgg, this.zzjgi, null);
    }
}
