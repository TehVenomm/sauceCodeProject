package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.internal.zzclt;
import com.google.android.gms.internal.zzcma;
import com.google.android.gms.nearby.messages.SubscribeOptions;

final class zzaq extends zzav {
    private /* synthetic */ PendingIntent zzgwu;
    private /* synthetic */ zzcj zzjgg;
    private /* synthetic */ SubscribeOptions zzjgi;

    zzaq(zzak zzak, GoogleApiClient googleApiClient, PendingIntent pendingIntent, zzcj zzcj, SubscribeOptions subscribeOptions) {
        this.zzgwu = pendingIntent;
        this.zzjgg = zzcj;
        this.zzjgi = subscribeOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzah zzah = (zzah) zzb;
        zzcj zzbbb = zzbbb();
        PendingIntent pendingIntent = this.zzgwu;
        zzcj zzcj = this.zzjgg;
        SubscribeOptions subscribeOptions = this.zzjgi;
        ((zzs) zzah.zzajj()).zza(new SubscribeRequest(null, subscribeOptions.getStrategy(), new zzclt(zzbbb), subscribeOptions.getFilter(), pendingIntent, null, zzcj == null ? null : new zzcma(zzcj), subscribeOptions.zzjev, subscribeOptions.zzjew));
    }
}
