package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.nearby.connection.Payload;

final class zzchz extends zzcim {
    private /* synthetic */ String zzjbw;
    private /* synthetic */ byte[] zzjce;

    zzchz(zzchp zzchp, GoogleApiClient googleApiClient, String str, byte[] bArr) {
        this.zzjbw = str;
        this.zzjce = bArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.zzjbw;
        String[] strArr = new String[]{str};
        zzcgp.zza(this, strArr, Payload.fromBytes(this.zzjce), true);
    }
}
