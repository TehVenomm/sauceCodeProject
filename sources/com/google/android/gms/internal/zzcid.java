package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.connection.DiscoveryOptions;

final class zzcid extends zzcim {
    private /* synthetic */ String zzjbz;
    private /* synthetic */ zzcj zzjch;
    private /* synthetic */ DiscoveryOptions zzjci;

    zzcid(zzchp zzchp, GoogleApiClient googleApiClient, String str, zzcj zzcj, DiscoveryOptions discoveryOptions) {
        this.zzjbz = str;
        this.zzjch = zzcj;
        this.zzjci = discoveryOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.zzjbz;
        zzcj zzcj = this.zzjch;
        ((zzcjg) zzcgp.zzajj()).zza(new zzclc(new zzchm(this).asBinder(), null, str, 0, this.zzjci, new zzcgz(zzcj).asBinder()));
    }
}
