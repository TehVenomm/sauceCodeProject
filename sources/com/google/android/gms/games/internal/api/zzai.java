package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzai extends zzap {
    private /* synthetic */ String zzhhf;
    private /* synthetic */ int zzhhg;
    private /* synthetic */ int zzhhh;

    zzai(zzaf zzaf, GoogleApiClient googleApiClient, String str, int i, int i2) {
        this.zzhhf = str;
        this.zzhhg = i;
        this.zzhhh = i2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, null, this.zzhhf, this.zzhhg, this.zzhhh);
    }
}
