package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzb extends zzk {
    private /* synthetic */ boolean zzhgy;

    zzb(zza zza, GoogleApiClient googleApiClient, boolean z) {
        this.zzhgy = z;
        super(googleApiClient);
    }

    public final /* synthetic */ void zza(com.google.android.gms.common.api.Api.zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzc((zzn) this, this.zzhgy);
    }
}
