package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.stats.PlayerStats;
import com.google.android.gms.games.stats.Stats.LoadPlayerStatsResult;

final class zzct implements LoadPlayerStatsResult {
    private /* synthetic */ Status zzeik;

    zzct(zzcs zzcs, Status status) {
        this.zzeik = status;
    }

    public final PlayerStats getPlayerStats() {
        return null;
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
