package com.google.android.gms.games.multiplayer.turnbased;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzg;

public final class TurnBasedMatchBuffer extends zzg<TurnBasedMatch> {
    public TurnBasedMatchBuffer(DataHolder dataHolder) {
        super(dataHolder);
    }

    protected final String zzaiw() {
        return "external_match_id";
    }

    protected final /* synthetic */ Object zzk(int i, int i2) {
        return new zzd(this.zzfkz, i, i2);
    }
}
