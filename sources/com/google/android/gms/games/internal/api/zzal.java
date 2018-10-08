package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.leaderboard.LeaderboardScoreBuffer;

final class zzal extends zzar {
    private /* synthetic */ int zzhhi;
    private /* synthetic */ LeaderboardScoreBuffer zzhhj;
    private /* synthetic */ int zzhhk;

    zzal(zzaf zzaf, GoogleApiClient googleApiClient, LeaderboardScoreBuffer leaderboardScoreBuffer, int i, int i2) {
        this.zzhhj = leaderboardScoreBuffer;
        this.zzhhi = i;
        this.zzhhk = i2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhj, this.zzhhi, this.zzhhk);
    }
}
