package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzcif extends zzcim {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zzjbw;
    private /* synthetic */ zzcj zzjcf;

    zzcif(zzchp zzchp, GoogleApiClient googleApiClient, String str, String str2, zzcj zzcj) {
        this.val$name = str;
        this.zzjbw = str2;
        this.zzjcf = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.val$name;
        String str2 = this.zzjbw;
        zzcj zzcj = this.zzjcf;
        ((zzcjg) zzcgp.zzajj()).zza(new zzckw(new zzchm(this).asBinder(), null, null, str, str2, null, new zzcgr(zzcj).asBinder()));
    }
}
