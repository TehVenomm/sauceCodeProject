package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzcx extends zzdh {
    private /* synthetic */ String zzhif;

    zzcx(zzcu zzcu, GoogleApiClient googleApiClient, String str) {
        this.zzhif = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzc((zzn) this, this.zzhif);
    }
}
