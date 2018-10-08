package com.google.android.gms.games.leaderboard;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

public final class LeaderboardBuffer extends zzg<Leaderboard> {
    public LeaderboardBuffer(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_leaderboard_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new LeaderboardRef(this.zzfkz, i, i2);
    }
}
