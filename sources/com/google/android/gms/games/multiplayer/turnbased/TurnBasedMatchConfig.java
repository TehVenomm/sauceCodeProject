package com.google.android.gms.games.multiplayer.turnbased;

import android.os.Bundle;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.games.multiplayer.Multiplayer;
import java.util.ArrayList;

public abstract class TurnBasedMatchConfig {

    public static final class Builder {
        int zzhmd;
        ArrayList<String> zzhmt;
        Bundle zzhmu;
        int zzhnd;

        private Builder() {
            this.zzhmd = -1;
            this.zzhmt = new ArrayList();
            this.zzhmu = null;
            this.zzhnd = 2;
        }

        public final Builder addInvitedPlayer(String str) {
            zzbp.zzu(str);
            this.zzhmt.add(str);
            return this;
        }

        public final Builder addInvitedPlayers(ArrayList<String> arrayList) {
            zzbp.zzu(arrayList);
            this.zzhmt.addAll(arrayList);
            return this;
        }

        public final TurnBasedMatchConfig build() {
            return new zzb(this);
        }

        public final Builder setAutoMatchCriteria(Bundle bundle) {
            this.zzhmu = bundle;
            return this;
        }

        public final Builder setVariant(int i) {
            boolean z = i == -1 || i > 0;
            zzbp.zzb(z, (Object) "Variant must be a positive integer or TurnBasedMatch.MATCH_VARIANT_ANY");
            this.zzhmd = i;
            return this;
        }
    }

    protected TurnBasedMatchConfig() {
    }

    public static Builder builder() {
        return new Builder();
    }

    public static Bundle createAutoMatchCriteria(int i, int i2, long j) {
        Bundle bundle = new Bundle();
        bundle.putInt(Multiplayer.EXTRA_MIN_AUTOMATCH_PLAYERS, i);
        bundle.putInt(Multiplayer.EXTRA_MAX_AUTOMATCH_PLAYERS, i2);
        bundle.putLong(Multiplayer.EXTRA_EXCLUSIVE_BIT_MASK, j);
        return bundle;
    }

    public abstract Bundle getAutoMatchCriteria();

    public abstract String[] getInvitedPlayerIds();

    public abstract int getVariant();

    public abstract int zzarv();
}
