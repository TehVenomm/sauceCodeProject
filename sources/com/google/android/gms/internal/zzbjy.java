package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbjy extends zzbiv {
    private /* synthetic */ zzbkv zzgid;

    zzbjy(zzbjw zzbjw, GoogleApiClient googleApiClient, zzbkv zzbkv) {
        this.zzgid = zzbkv;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbnb(this.zzgid), new zzbnf(this));
    }
}
