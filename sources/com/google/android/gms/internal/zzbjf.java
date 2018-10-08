package com.google.android.gms.internal;

import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;

final class zzbjf implements ResultCallback<Status> {
    zzbjf(zzbjc zzbjc) {
    }

    public final /* synthetic */ void onResult(Result result) {
        if (((Status) result).isSuccess()) {
            zzbjv.zzx("DriveContentsImpl", "Contents discarded");
        } else {
            zzbjv.zzz("DriveContentsImpl", "Error discarding contents");
        }
    }
}
