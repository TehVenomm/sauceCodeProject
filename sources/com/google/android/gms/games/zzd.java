package com.google.android.gms.games;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzd extends zzc {
    private /* synthetic */ String zzhcc;

    zzd(GoogleApiClient googleApiClient, String str) {
        this.zzhcc = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzb(this.zzhcc, (zzn) this);
    }
}
