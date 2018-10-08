package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

final class zzw implements Result {
    private /* synthetic */ Status zzeik;

    zzw(zzv zzv, Status status) {
        this.zzeik = status;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
