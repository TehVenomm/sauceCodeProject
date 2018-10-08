package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatchConfig;

final class zzcv extends zzdh {
    private /* synthetic */ TurnBasedMatchConfig zzhie;

    zzcv(zzcu zzcu, GoogleApiClient googleApiClient, TurnBasedMatchConfig turnBasedMatchConfig) {
        this.zzhie = turnBasedMatchConfig;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhie);
    }
}
