package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzr extends zzt {
    private /* synthetic */ boolean zzhgy;

    zzr(zzp zzp, GoogleApiClient googleApiClient, boolean z) {
        this.zzhgy = z;
        super(googleApiClient);
    }

    public final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzd((zzn) this, this.zzhgy);
    }
}
