package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.request.GameRequestBuffer;
import com.google.android.gms.games.request.Requests.LoadRequestsResult;

final class zzby implements LoadRequestsResult {
    private /* synthetic */ Status zzeik;

    zzby(zzbx zzbx, Status status) {
        this.zzeik = status;
    }

    public final GameRequestBuffer getRequests(int i) {
        return new GameRequestBuffer(DataHolder.zzbx(this.zzeik.getStatusCode()));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
