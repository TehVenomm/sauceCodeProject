package com.google.android.gms.games.leaderboard;

import android.database.CharArrayBuffer;
import android.net.Uri;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameRef;
import java.util.ArrayList;

public final class LeaderboardRef extends zzc implements Leaderboard {
    private final Game zzhkx;
    private final int zzhky;

    LeaderboardRef(DataHolder dataHolder, int i, int i2) {
        super(dataHolder, i);
        this.zzhky = i2;
        this.zzhkx = new GameRef(dataHolder, i);
    }

    public final boolean equals(Object obj) {
        return LeaderboardEntity.zza(this, obj);
    }

    public final /* synthetic */ Object freeze() {
        return new LeaderboardEntity(this);
    }

    public final String getDisplayName() {
        return getString("name");
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        zza("name", charArrayBuffer);
    }

    public final Game getGame() {
        return this.zzhkx;
    }

    public final Uri getIconImageUri() {
        return zzfu("board_icon_image_uri");
    }

    public final String getIconImageUrl() {
        return getString("board_icon_image_url");
    }

    public final String getLeaderboardId() {
        return getString("external_leaderboard_id");
    }

    public final int getScoreOrder() {
        return getInteger("score_order");
    }

    public final ArrayList<LeaderboardVariant> getVariants() {
        ArrayList<LeaderboardVariant> arrayList = new ArrayList(this.zzhky);
        for (int i = 0; i < this.zzhky; i++) {
            arrayList.add(new zzc(this.zzfkz, this.zzfqb + i));
        }
        return arrayList;
    }

    public final int hashCode() {
        return LeaderboardEntity.zza(this);
    }

    public final String toString() {
        return LeaderboardEntity.zzb(this);
    }
}
