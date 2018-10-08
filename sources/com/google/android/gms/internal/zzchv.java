package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.connection.DiscoveryOptions;
import com.google.android.gms.nearby.connection.Strategy;

final class zzchv extends zzcim {
    private /* synthetic */ long zzjbx;
    private /* synthetic */ String zzjbz;
    private /* synthetic */ zzcj zzjca;

    zzchv(zzchp zzchp, GoogleApiClient googleApiClient, String str, long j, zzcj zzcj) {
        this.zzjbz = str;
        this.zzjbx = j;
        this.zzjca = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.zzjbz;
        long j = this.zzjbx;
        zzcj zzcj = this.zzjca;
        ((zzcjg) zzcgp.zzajj()).zza(new zzclc(new zzchm(this).asBinder(), null, str, j, new DiscoveryOptions(Strategy.P2P_CLUSTER), new zzchc(zzcj).asBinder()));
    }
}
