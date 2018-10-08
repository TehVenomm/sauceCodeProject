package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzdd extends zzdf {
    private /* synthetic */ String zzhif;

    zzdd(zzcu zzcu, String str, GoogleApiClient googleApiClient, String str2) {
        this.zzhif = str2;
        super(str, googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzf((zzn) this, this.zzhif);
    }
}
