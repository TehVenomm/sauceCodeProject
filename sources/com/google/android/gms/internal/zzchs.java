package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzchs extends zzcim {
    private /* synthetic */ String zzjbw;

    zzchs(zzchp zzchp, GoogleApiClient googleApiClient, String str) {
        this.zzjbw = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzcjg) ((zzcgp) zzb).zzajj()).zza(new zzcin(this.zzjbw));
    }
}
