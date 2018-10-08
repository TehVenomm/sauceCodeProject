package com.google.android.gms.games.leaderboard;

import com.google.android.gms.common.data.AbstractDataBuffer;
import com.google.android.gms.common.data.DataHolder;

public final class LeaderboardScoreBuffer extends AbstractDataBuffer<LeaderboardScore> {
    private final zza zzhkz;

    public LeaderboardScoreBuffer(DataHolder dataHolder) {
        super(dataHolder);
        this.zzhkz = new zza(dataHolder.zzafh());
    }

    public final LeaderboardScore get(int i) {
        return new LeaderboardScoreRef(this.zzfkz, i);
    }

    public final zza zzarq() {
        return this.zzhkz;
    }
}
