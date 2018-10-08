package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.PlayerBuffer;
import com.google.android.gms.games.Players.LoadPlayersResult;

final class zzbg implements LoadPlayersResult {
    private /* synthetic */ Status zzeik;

    zzbg(zzbf zzbf, Status status) {
        this.zzeik = status;
    }

    public final PlayerBuffer getPlayers() {
        return new PlayerBuffer(DataHolder.zzbx(14));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
