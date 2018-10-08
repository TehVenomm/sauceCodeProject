package com.google.android.gms.games.multiplayer.turnbased;

import android.os.Bundle;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMatchConfig.Builder;

public final class zzb extends TurnBasedMatchConfig {
    private final int zzhmd;
    private final Bundle zzhmu;
    private final String[] zzhmv;
    private final int zzhnd;

    zzb(Builder builder) {
        this.zzhmd = builder.zzhmd;
        this.zzhnd = builder.zzhnd;
        this.zzhmu = builder.zzhmu;
        this.zzhmv = (String[]) builder.zzhmt.toArray(new String[builder.zzhmt.size()]);
    }

    public final Bundle getAutoMatchCriteria() {
        return this.zzhmu;
    }

    public final String[] getInvitedPlayerIds() {
        return this.zzhmv;
    }

    public final int getVariant() {
        return this.zzhmd;
    }

    public final int zzarv() {
        return this.zzhnd;
    }
}
