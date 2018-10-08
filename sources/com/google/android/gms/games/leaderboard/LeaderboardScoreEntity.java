package com.google.android.gms.games.leaderboard;

import android.database.CharArrayBuffer;
import android.net.Uri;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzg;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerEntity;
import java.util.Arrays;

public final class LeaderboardScoreEntity implements LeaderboardScore {
    private final long zzhla;
    private final String zzhlb;
    private final String zzhlc;
    private final long zzhld;
    private final long zzhle;
    private final String zzhlf;
    private final Uri zzhlg;
    private final Uri zzhlh;
    private final PlayerEntity zzhli;
    private final String zzhlj;
    private final String zzhlk;
    private final String zzhll;

    public LeaderboardScoreEntity(LeaderboardScore leaderboardScore) {
        this.zzhla = leaderboardScore.getRank();
        this.zzhlb = (String) zzbp.zzu(leaderboardScore.getDisplayRank());
        this.zzhlc = (String) zzbp.zzu(leaderboardScore.getDisplayScore());
        this.zzhld = leaderboardScore.getRawScore();
        this.zzhle = leaderboardScore.getTimestampMillis();
        this.zzhlf = leaderboardScore.getScoreHolderDisplayName();
        this.zzhlg = leaderboardScore.getScoreHolderIconImageUri();
        this.zzhlh = leaderboardScore.getScoreHolderHiResImageUri();
        Player scoreHolder = leaderboardScore.getScoreHolder();
        this.zzhli = scoreHolder == null ? null : (PlayerEntity) scoreHolder.freeze();
        this.zzhlj = leaderboardScore.getScoreTag();
        this.zzhlk = leaderboardScore.getScoreHolderIconImageUrl();
        this.zzhll = leaderboardScore.getScoreHolderHiResImageUrl();
    }

    static int zza(LeaderboardScore leaderboardScore) {
        return Arrays.hashCode(new Object[]{Long.valueOf(leaderboardScore.getRank()), leaderboardScore.getDisplayRank(), Long.valueOf(leaderboardScore.getRawScore()), leaderboardScore.getDisplayScore(), Long.valueOf(leaderboardScore.getTimestampMillis()), leaderboardScore.getScoreHolderDisplayName(), leaderboardScore.getScoreHolderIconImageUri(), leaderboardScore.getScoreHolderHiResImageUri(), leaderboardScore.getScoreHolder()});
    }

    static boolean zza(LeaderboardScore leaderboardScore, Object obj) {
        if (!(obj instanceof LeaderboardScore)) {
            return false;
        }
        if (leaderboardScore == obj) {
            return true;
        }
        LeaderboardScore leaderboardScore2 = (LeaderboardScore) obj;
        return zzbf.equal(Long.valueOf(leaderboardScore2.getRank()), Long.valueOf(leaderboardScore.getRank())) && zzbf.equal(leaderboardScore2.getDisplayRank(), leaderboardScore.getDisplayRank()) && zzbf.equal(Long.valueOf(leaderboardScore2.getRawScore()), Long.valueOf(leaderboardScore.getRawScore())) && zzbf.equal(leaderboardScore2.getDisplayScore(), leaderboardScore.getDisplayScore()) && zzbf.equal(Long.valueOf(leaderboardScore2.getTimestampMillis()), Long.valueOf(leaderboardScore.getTimestampMillis())) && zzbf.equal(leaderboardScore2.getScoreHolderDisplayName(), leaderboardScore.getScoreHolderDisplayName()) && zzbf.equal(leaderboardScore2.getScoreHolderIconImageUri(), leaderboardScore.getScoreHolderIconImageUri()) && zzbf.equal(leaderboardScore2.getScoreHolderHiResImageUri(), leaderboardScore.getScoreHolderHiResImageUri()) && zzbf.equal(leaderboardScore2.getScoreHolder(), leaderboardScore.getScoreHolder()) && zzbf.equal(leaderboardScore2.getScoreTag(), leaderboardScore.getScoreTag());
    }

    static String zzb(LeaderboardScore leaderboardScore) {
        return zzbf.zzt(leaderboardScore).zzg("Rank", Long.valueOf(leaderboardScore.getRank())).zzg("DisplayRank", leaderboardScore.getDisplayRank()).zzg("Score", Long.valueOf(leaderboardScore.getRawScore())).zzg("DisplayScore", leaderboardScore.getDisplayScore()).zzg("Timestamp", Long.valueOf(leaderboardScore.getTimestampMillis())).zzg("DisplayName", leaderboardScore.getScoreHolderDisplayName()).zzg("IconImageUri", leaderboardScore.getScoreHolderIconImageUri()).zzg("IconImageUrl", leaderboardScore.getScoreHolderIconImageUrl()).zzg("HiResImageUri", leaderboardScore.getScoreHolderHiResImageUri()).zzg("HiResImageUrl", leaderboardScore.getScoreHolderHiResImageUrl()).zzg("Player", leaderboardScore.getScoreHolder() == null ? null : leaderboardScore.getScoreHolder()).zzg("ScoreTag", leaderboardScore.getScoreTag()).toString();
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

    public final String getDisplayRank() {
        return this.zzhlb;
    }

    public final void getDisplayRank(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzhlb, charArrayBuffer);
    }

    public final String getDisplayScore() {
        return this.zzhlc;
    }

    public final void getDisplayScore(CharArrayBuffer charArrayBuffer) {
        zzg.zzb(this.zzhlc, charArrayBuffer);
    }

    public final long getRank() {
        return this.zzhla;
    }

    public final long getRawScore() {
        return this.zzhld;
    }

    public final Player getScoreHolder() {
        return this.zzhli;
    }

    public final String getScoreHolderDisplayName() {
        return this.zzhli == null ? this.zzhlf : this.zzhli.getDisplayName();
    }

    public final void getScoreHolderDisplayName(CharArrayBuffer charArrayBuffer) {
        if (this.zzhli == null) {
            zzg.zzb(this.zzhlf, charArrayBuffer);
        } else {
            this.zzhli.getDisplayName(charArrayBuffer);
        }
    }

    public final Uri getScoreHolderHiResImageUri() {
        return this.zzhli == null ? this.zzhlh : this.zzhli.getHiResImageUri();
    }

    public final String getScoreHolderHiResImageUrl() {
        return this.zzhli == null ? this.zzhll : this.zzhli.getHiResImageUrl();
    }

    public final Uri getScoreHolderIconImageUri() {
        return this.zzhli == null ? this.zzhlg : this.zzhli.getIconImageUri();
    }

    public final String getScoreHolderIconImageUrl() {
        return this.zzhli == null ? this.zzhlk : this.zzhli.getIconImageUrl();
    }

    public final String getScoreTag() {
        return this.zzhlj;
    }

    public final long getTimestampMillis() {
        return this.zzhle;
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
