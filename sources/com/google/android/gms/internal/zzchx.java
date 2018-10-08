package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzchx extends zzcim {
    private /* synthetic */ String zzjbw;
    private /* synthetic */ byte[] zzjcb;
    private /* synthetic */ zzcj zzjcd;

    zzchx(zzchp zzchp, GoogleApiClient googleApiClient, String str, byte[] bArr, zzcj zzcj) {
        this.zzjbw = str;
        this.zzjcb = bArr;
        this.zzjcd = zzcj;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzcjg) ((zzcgp) zzb).zzajj()).zza(new zzcgl(new zzchm(this).asBinder(), new zzchf(this.zzjcd).asBinder(), this.zzjbw, this.zzjcb, null));
    }
}
