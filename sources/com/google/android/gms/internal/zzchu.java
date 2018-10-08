package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.connection.AdvertisingOptions;
import com.google.android.gms.nearby.connection.Strategy;

final class zzchu extends zzcik {
    private /* synthetic */ String val$name;
    private /* synthetic */ long zzjbx;
    private /* synthetic */ zzcj zzjby;

    zzchu(zzchp zzchp, GoogleApiClient googleApiClient, String str, long j, zzcj zzcj) {
        this.val$name = str;
        this.zzjbx = j;
        this.zzjby = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.val$name;
        long j = this.zzjbx;
        zzcj zzcj = this.zzjby;
        ((zzcjg) zzcgp.zzajj()).zza(new zzcla(new zzcho(this).asBinder(), new zzcgv(zzcj).asBinder(), str, "__LEGACY_SERVICE_ID__", j, new AdvertisingOptions(Strategy.P2P_CLUSTER), null));
    }
}
