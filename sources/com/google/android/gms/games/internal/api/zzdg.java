package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.CancelMatchResult;

final class zzdg implements CancelMatchResult {
    private /* synthetic */ Status zzeik;
    private /* synthetic */ zzdf zzhim;

    zzdg(zzdf zzdf, Status status) {
        this.zzhim = zzdf;
        this.zzeik = status;
    }

    public final String getMatchId() {
        return this.zzhim.zzbsx;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
