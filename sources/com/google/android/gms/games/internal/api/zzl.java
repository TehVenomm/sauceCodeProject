package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.achievement.AchievementBuffer;
import com.google.android.gms.games.achievement.Achievements.LoadAchievementsResult;

final class zzl implements LoadAchievementsResult {
    private /* synthetic */ Status zzeik;

    zzl(zzk zzk, Status status) {
        this.zzeik = status;
    }

    public final AchievementBuffer getAchievements() {
        return new AchievementBuffer(DataHolder.zzbx(14));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
