package com.google.android.gms.games.leaderboard;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbh;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbvh;
import java.util.HashMap;

public final class ScoreSubmissionData {
    private static final String[] zzhks = new String[]{"leaderboardId", "playerId", "timeSpan", "hasResult", "rawScore", "formattedScore", "newBest", "scoreTag"};
    private String zzezq;
    private int zzezx;
    private String zzhku;
    private HashMap<Integer, Result> zzhlz = new HashMap();

    public static final class Result {
        public final String formattedScore;
        public final boolean newBest;
        public final long rawScore;
        public final String scoreTag;

        public Result(long j, String str, String str2, boolean z) {
            this.rawScore = j;
            this.formattedScore = str;
            this.scoreTag = str2;
            this.newBest = z;
        }

        public final String toString() {
            return zzbf.zzt(this).zzg("RawScore", Long.valueOf(this.rawScore)).zzg("FormattedScore", this.formattedScore).zzg("ScoreTag", this.scoreTag).zzg("NewBest", Boolean.valueOf(this.newBest)).toString();
        }
    }

    public ScoreSubmissionData(DataHolder dataHolder) {
        this.zzezx = dataHolder.getStatusCode();
        int count = dataHolder.getCount();
        zzbp.zzbh(count == 3);
        for (int i = 0; i < count; i++) {
            int zzbw = dataHolder.zzbw(i);
            if (i == 0) {
                this.zzhku = dataHolder.zzd("leaderboardId", i, zzbw);
                this.zzezq = dataHolder.zzd("playerId", i, zzbw);
            }
            if (dataHolder.zze("hasResult", i, zzbw)) {
                this.zzhlz.put(Integer.valueOf(dataHolder.zzc("timeSpan", i, zzbw)), new Result(dataHolder.zzb("rawScore", i, zzbw), dataHolder.zzd("formattedScore", i, zzbw), dataHolder.zzd("scoreTag", i, zzbw), dataHolder.zze("newBest", i, zzbw)));
            }
        }
    }

    public final String getLeaderboardId() {
        return this.zzhku;
    }

    public final String getPlayerId() {
        return this.zzezq;
    }

    public final Result getScoreResult(int i) {
        return (Result) this.zzhlz.get(Integer.valueOf(i));
    }

    public final String toString() {
        zzbh zzg = zzbf.zzt(this).zzg("PlayerId", this.zzezq).zzg("StatusCode", Integer.valueOf(this.zzezx));
        for (int i = 0; i < 3; i++) {
            Result result = (Result) this.zzhlz.get(Integer.valueOf(i));
            zzg.zzg("TimesSpan", zzbvh.zzdf(i));
            zzg.zzg("Result", result == null ? "null" : result.toString());
        }
        return zzg.toString();
    }
}
