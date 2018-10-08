package com.google.android.gms.games.leaderboard;

import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbh;
import com.google.android.gms.internal.zzbvh;
import java.util.Arrays;

public final class zzb implements LeaderboardVariant {
    private final int zzhln;
    private final int zzhlo;
    private final boolean zzhlp;
    private final long zzhlq;
    private final String zzhlr;
    private final long zzhls;
    private final String zzhlt;
    private final String zzhlu;
    private final long zzhlv;
    private final String zzhlw;
    private final String zzhlx;
    private final String zzhly;

    public zzb(LeaderboardVariant leaderboardVariant) {
        this.zzhln = leaderboardVariant.getTimeSpan();
        this.zzhlo = leaderboardVariant.getCollection();
        this.zzhlp = leaderboardVariant.hasPlayerInfo();
        this.zzhlq = leaderboardVariant.getRawPlayerScore();
        this.zzhlr = leaderboardVariant.getDisplayPlayerScore();
        this.zzhls = leaderboardVariant.getPlayerRank();
        this.zzhlt = leaderboardVariant.getDisplayPlayerRank();
        this.zzhlu = leaderboardVariant.getPlayerScoreTag();
        this.zzhlv = leaderboardVariant.getNumScores();
        this.zzhlw = leaderboardVariant.zzarr();
        this.zzhlx = leaderboardVariant.zzars();
        this.zzhly = leaderboardVariant.zzart();
    }

    static int zza(LeaderboardVariant leaderboardVariant) {
        return Arrays.hashCode(new Object[]{Integer.valueOf(leaderboardVariant.getTimeSpan()), Integer.valueOf(leaderboardVariant.getCollection()), Boolean.valueOf(leaderboardVariant.hasPlayerInfo()), Long.valueOf(leaderboardVariant.getRawPlayerScore()), leaderboardVariant.getDisplayPlayerScore(), Long.valueOf(leaderboardVariant.getPlayerRank()), leaderboardVariant.getDisplayPlayerRank(), Long.valueOf(leaderboardVariant.getNumScores()), leaderboardVariant.zzarr(), leaderboardVariant.zzart(), leaderboardVariant.zzars()});
    }

    static boolean zza(LeaderboardVariant leaderboardVariant, Object obj) {
        if (!(obj instanceof LeaderboardVariant)) {
            return false;
        }
        if (leaderboardVariant == obj) {
            return true;
        }
        LeaderboardVariant leaderboardVariant2 = (LeaderboardVariant) obj;
        return zzbf.equal(Integer.valueOf(leaderboardVariant2.getTimeSpan()), Integer.valueOf(leaderboardVariant.getTimeSpan())) && zzbf.equal(Integer.valueOf(leaderboardVariant2.getCollection()), Integer.valueOf(leaderboardVariant.getCollection())) && zzbf.equal(Boolean.valueOf(leaderboardVariant2.hasPlayerInfo()), Boolean.valueOf(leaderboardVariant.hasPlayerInfo())) && zzbf.equal(Long.valueOf(leaderboardVariant2.getRawPlayerScore()), Long.valueOf(leaderboardVariant.getRawPlayerScore())) && zzbf.equal(leaderboardVariant2.getDisplayPlayerScore(), leaderboardVariant.getDisplayPlayerScore()) && zzbf.equal(Long.valueOf(leaderboardVariant2.getPlayerRank()), Long.valueOf(leaderboardVariant.getPlayerRank())) && zzbf.equal(leaderboardVariant2.getDisplayPlayerRank(), leaderboardVariant.getDisplayPlayerRank()) && zzbf.equal(Long.valueOf(leaderboardVariant2.getNumScores()), Long.valueOf(leaderboardVariant.getNumScores())) && zzbf.equal(leaderboardVariant2.zzarr(), leaderboardVariant.zzarr()) && zzbf.equal(leaderboardVariant2.zzart(), leaderboardVariant.zzart()) && zzbf.equal(leaderboardVariant2.zzars(), leaderboardVariant.zzars());
    }

    static String zzb(LeaderboardVariant leaderboardVariant) {
        Object obj;
        zzbh zzg = zzbf.zzt(leaderboardVariant).zzg("TimeSpan", zzbvh.zzdf(leaderboardVariant.getTimeSpan()));
        int collection = leaderboardVariant.getCollection();
        switch (collection) {
            case -1:
                obj = "UNKNOWN";
                break;
            case 0:
                obj = "PUBLIC";
                break;
            case 1:
                obj = "SOCIAL";
                break;
            case 2:
                obj = "SOCIAL_1P";
                break;
            default:
                throw new IllegalArgumentException("Unknown leaderboard collection: " + collection);
        }
        return zzg.zzg("Collection", obj).zzg("RawPlayerScore", leaderboardVariant.hasPlayerInfo() ? Long.valueOf(leaderboardVariant.getRawPlayerScore()) : "none").zzg("DisplayPlayerScore", leaderboardVariant.hasPlayerInfo() ? leaderboardVariant.getDisplayPlayerScore() : "none").zzg("PlayerRank", leaderboardVariant.hasPlayerInfo() ? Long.valueOf(leaderboardVariant.getPlayerRank()) : "none").zzg("DisplayPlayerRank", leaderboardVariant.hasPlayerInfo() ? leaderboardVariant.getDisplayPlayerRank() : "none").zzg("NumScores", Long.valueOf(leaderboardVariant.getNumScores())).zzg("TopPageNextToken", leaderboardVariant.zzarr()).zzg("WindowPageNextToken", leaderboardVariant.zzart()).zzg("WindowPagePrevToken", leaderboardVariant.zzars()).toString();
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

    public final int getCollection() {
        return this.zzhlo;
    }

    public final String getDisplayPlayerRank() {
        return this.zzhlt;
    }

    public final String getDisplayPlayerScore() {
        return this.zzhlr;
    }

    public final long getNumScores() {
        return this.zzhlv;
    }

    public final long getPlayerRank() {
        return this.zzhls;
    }

    public final String getPlayerScoreTag() {
        return this.zzhlu;
    }

    public final long getRawPlayerScore() {
        return this.zzhlq;
    }

    public final int getTimeSpan() {
        return this.zzhln;
    }

    public final boolean hasPlayerInfo() {
        return this.zzhlp;
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

    public final String zzarr() {
        return this.zzhlw;
    }

    public final String zzars() {
        return this.zzhlx;
    }

    public final String zzart() {
        return this.zzhly;
    }
}
