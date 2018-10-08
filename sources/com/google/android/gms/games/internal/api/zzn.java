package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.achievement.Achievements.UpdateAchievementResult;

final class zzn implements UpdateAchievementResult {
    private /* synthetic */ Status zzeik;
    private /* synthetic */ zzm zzhha;

    zzn(zzm zzm, Status status) {
        this.zzhha = zzm;
        this.zzeik = status;
    }

    public final String getAchievementId() {
        return this.zzhha.zzbsx;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
