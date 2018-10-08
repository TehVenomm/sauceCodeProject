package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzcig extends zzcim {
    private /* synthetic */ String zzjbw;
    private /* synthetic */ zzcj zzjcj;

    zzcig(zzchp zzchp, GoogleApiClient googleApiClient, String str, zzcj zzcj) {
        this.zzjbw = str;
        this.zzjcj = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        byte[] bArr = null;
        ((zzcjg) ((zzcgp) zzb).zzajj()).zza(new zzcgl(new zzchm(this).asBinder(), null, this.zzjbw, bArr, new zzchj(this.zzjcj).asBinder()));
    }
}
