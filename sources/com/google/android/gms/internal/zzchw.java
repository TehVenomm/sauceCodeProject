package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzcj;

final class zzchw extends zzcim {
    private /* synthetic */ String val$name;
    private /* synthetic */ String zzjbw;
    private /* synthetic */ byte[] zzjcb;
    private /* synthetic */ zzcj zzjcc;
    private /* synthetic */ zzcj zzjcd;

    zzchw(zzchp zzchp, GoogleApiClient googleApiClient, String str, String str2, byte[] bArr, zzcj zzcj, zzcj zzcj2) {
        this.val$name = str;
        this.zzjbw = str2;
        this.zzjcb = bArr;
        this.zzjcc = zzcj;
        this.zzjcd = zzcj2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.val$name;
        String str2 = this.zzjbw;
        byte[] bArr = this.zzjcb;
        zzcj zzcj = this.zzjcc;
        ((zzcjg) zzcgp.zzajj()).zza(new zzckw(new zzchm(this).asBinder(), new zzchf(this.zzjcd).asBinder(), new zzcgx(zzcj).asBinder(), str, str2, bArr, null));
    }
}
