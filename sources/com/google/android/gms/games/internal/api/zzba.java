package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzba extends zzbf {
    private /* synthetic */ boolean zzhgy;
    private /* synthetic */ int zzhhn;

    zzba(zzax zzax, GoogleApiClient googleApiClient, int i, boolean z) {
        this.zzhhn = i;
        this.zzhgy = z;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhn, false, this.zzhgy);
    }
}
