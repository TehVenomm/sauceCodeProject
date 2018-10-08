package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.GameBuffer;
import com.google.android.gms.games.GamesMetadata.LoadGamesResult;

final class zzaa implements LoadGamesResult {
    private /* synthetic */ Status zzeik;

    zzaa(zzz zzz, Status status) {
        this.zzeik = status;
    }

    public final GameBuffer getGames() {
        return new GameBuffer(DataHolder.zzbx(14));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
