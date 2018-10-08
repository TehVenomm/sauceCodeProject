package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbi extends zzbm {
    private /* synthetic */ String zzhho;

    zzbi(zzbh zzbh, GoogleApiClient googleApiClient, String str) {
        this.zzhho = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzh(this, this.zzhho);
    }
}
