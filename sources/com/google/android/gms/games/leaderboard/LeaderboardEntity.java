package com.google.android.gms.games.leaderboard;

import android.database.CharArrayBuffer;
import android.net.Uri;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Game;
import com.google.android.gms.games.GameEntity;
import java.util.ArrayList;
import java.util.Arrays;

public final class LeaderboardEntity implements Leaderboard {
    private final String zzeby;
    private final Uri zzhbd;
    private final String zzhbo;
    private final String zzhku;
    private final int zzhkv;
    private final ArrayList<zzb> zzhkw;
    private final Game zzhkx;

    public LeaderboardEntity(Leaderboard leaderboard) {
        this.zzhku = leaderboard.getLeaderboardId();
        this.zzeby = leaderboard.getDisplayName();
        this.zzhbd = leaderboard.getIconImageUri();
        this.zzhbo = leaderboard.getIconImageUrl();
        this.zzhkv = leaderboard.getScoreOrder();
        Game game = leaderboard.getGame();
        this.zzhkx = game == null ? null : new GameEntity(game);
        ArrayList variants = leaderboard.getVariants();
        int size = variants.size();
        this.zzhkw = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            this.zzhkw.add((zzb) ((LeaderboardVariant) variants.get(i)).freeze());
        }
    }

    static int zza(Leaderboard leaderboard) {
        return Arrays.hashCode(new Object[]{leaderboard.getLeaderboardId(), leaderboard.getDisplayName(), leaderboard.getIconImageUri(), Integer.valueOf(leaderboard.getScoreOrder()), leaderboard.getVariants()});
    }

    static boolean zza(Leaderboard leaderboard, Object obj) {
        if (!(obj instanceof Leaderboard)) {
            return false;
        }
        if (leaderboard == obj) {
            return true;
        }
        Leaderboard leaderboard2 = (Leaderboard) obj;
        return zzbf.equal(leaderboard2.getLeaderboardId(), leaderboard.getLeaderboardId()) && zzbf.equal(leaderboard2.getDisplayName(), leaderboard.getDisplayName()) && zzbf.equal(leaderboard2.getIconImageUri(), leaderboard.getIconImageUri()) && zzbf.equal(Integer.valueOf(leaderboard2.getScoreOrder()), Integer.valueOf(leaderboard.getScoreOrder())) && zzbf.equal(leaderboard2.getVariants(), leaderboard.getVariants());
    }

    static String zzb(Leaderboard leaderboard) {
        return zzbf.zzt(leaderboard).zzg("LeaderboardId", leaderboard.getLeaderboardId()).zzg("DisplayName", leaderboard.getDisplayName()).zzg("IconImageUri", leaderboard.getIconImageUri()).zzg("IconImageUrl", leaderboard.getIconImageUrl()).zzg("ScoreOrder", Integer.valueOf(leaderboard.getScoreOrder())).zzg("Variants", leaderboard.getVariants()).toString();
    }

    public final boolean equals(Object obj) {
        return zza(this, obj);
    }

    public final /* bridge */ /* synthetic */ Object freeze() {
        if (this != null) {
            return this;
        }
        throw null;
    }

    public final String getDisplayName() {
        return this.zzeby;
    }

    public final void getDisplayName(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzeby, charArrayBuffer);
    }

    public final Game getGame() {
        return this.zzhkx;
    }

    public final Uri getIconImageUri() {
        return this.zzhbd;
    }

    public final String getIconImageUrl() {
        return this.zzhbo;
    }

    public final String getLeaderboardId() {
        return this.zzhku;
    }

    public final int getScoreOrder() {
        return this.zzhkv;
    }

    public final ArrayList<LeaderboardVariant> getVariants() {
        return new ArrayList(this.zzhkw);
    }

    public final int hashCode() {
        return zza(this);
    }

    public final boolean isDataValid() {
        return true;
    }

    public final String toString() {
        return zzb(this);
    }
}
