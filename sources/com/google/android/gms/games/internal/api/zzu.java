package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.games.event.EventBuffer;
import com.google.android.gms.games.event.Events.LoadEventsResult;

final class zzu implements LoadEventsResult {
    private /* synthetic */ Status zzeik;

    zzu(zzt zzt, Status status) {
        this.zzeik = status;
    }

    public final EventBuffer getEvents() {
        return new EventBuffer(DataHolder.zzbx(14));
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
