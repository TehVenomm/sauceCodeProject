package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.video.Videos.CaptureAvailableResult;

final class zzdw implements CaptureAvailableResult {
    private /* synthetic */ Status zzeik;

    zzdw(zzdv zzdv, Status status) {
        this.zzeik = status;
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final boolean isAvailable() {
        return false;
    }
}
