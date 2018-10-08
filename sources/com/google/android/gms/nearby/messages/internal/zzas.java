package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.internal.zzclt;

final class zzas extends zzav {
    private /* synthetic */ PendingIntent zzgwu;

    zzas(zzak zzak, GoogleApiClient googleApiClient, PendingIntent pendingIntent) {
        this.zzgwu = pendingIntent;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzah zzah = (zzah) zzb;
        zzcj zzbbb = zzbbb();
        ((zzs) zzah.zzajj()).zza(new zzbe(null, new zzclt(zzbbb), this.zzgwu));
    }
}
