package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbiz extends zzbiv {
    private /* synthetic */ zzbhg zzghd;

    zzbiz(zzbiw zzbiw, GoogleApiClient googleApiClient, zzbhg zzbhg) {
        this.zzghd = zzbhg;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(this.zzghd, null, null, new zzbnf(this));
    }
}
