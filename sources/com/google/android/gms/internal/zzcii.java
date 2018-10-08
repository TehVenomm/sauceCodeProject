package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.nearby.connection.Payload;

final class zzcii extends zzcim {
    private /* synthetic */ Payload zzjbv;
    private /* synthetic */ String zzjbw;

    zzcii(zzchp zzchp, GoogleApiClient googleApiClient, String str, Payload payload) {
        this.zzjbw = str;
        this.zzjbv = payload;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzcgp zzcgp = (zzcgp) zzb;
        String str = this.zzjbw;
        String[] strArr = new String[]{str};
        zzcgp.zza(this, strArr, this.zzjbv, false);
    }
}
