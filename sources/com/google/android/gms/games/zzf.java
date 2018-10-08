package com.google.android.gms.games;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.Games.GetServerAuthCodeResult;

final class zzf implements GetServerAuthCodeResult {
    private /* synthetic */ Status zzeik;

    zzf(zzc zzc, Status status) {
        this.zzeik = status;
    }

    public final String getCode() {
        return null;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
