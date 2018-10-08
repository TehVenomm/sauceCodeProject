package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;

final class zzbkd extends zzbkn {
    private /* synthetic */ boolean zzgif = false;
    private /* synthetic */ zzbkc zzgig;

    zzbkd(zzbkc zzbkc, GoogleApiClient googleApiClient, boolean z) {
        this.zzgig = zzbkc;
        super(zzbkc, googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbkx(this.zzgig.zzgcx, this.zzgif), new zzbkl(this));
    }
}
