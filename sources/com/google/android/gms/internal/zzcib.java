package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.connection.AdvertisingOptions;

final class zzcib extends zzcik {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zzjbz;
    private /* synthetic */ zzcj zzjcf;
    private /* synthetic */ AdvertisingOptions zzjcg;

    zzcib(zzchp zzchp, GoogleApiClient googleApiClient, String str, String str2, zzcj zzcj, AdvertisingOptions advertisingOptions) {
        this.val$name = str;
        this.zzjbz = str2;
        this.zzjcf = zzcj;
        this.zzjcg = advertisingOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.val$name;
        String str2 = this.zzjbz;
        zzcj zzcj = this.zzjcf;
        ((zzcjg) zzcgp.zzajj()).zza(new zzcla(new zzcho(this).asBinder(), null, str, str2, 0, this.zzjcg, new zzcgr(zzcj).asBinder()));
    }
}
