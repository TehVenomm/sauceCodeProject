package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzcc extends zzcl {
    private /* synthetic */ boolean zzhgy;

    zzcc(zzcb zzcb, GoogleApiClient googleApiClient, boolean z) {
        this.zzhgy = z;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzf((zzn) this, this.zzhgy);
    }
}
