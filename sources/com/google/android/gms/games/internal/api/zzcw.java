package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzcw extends zzdl {
    private /* synthetic */ String zzhif;

    zzcw(zzcu zzcu, GoogleApiClient googleApiClient, String str) {
        this.zzhif = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzg(this, this.zzhif);
    }
}
