package com.google.android.gms.common.internal;

import com.google.android.gms.common.api.Response;
import com.google.android.gms.common.api.Result;

final class zzbl implements zzbn<R, T> {
    private /* synthetic */ Response zzfvq;

    zzbl(Response response) {
        this.zzfvq = response;
    }

    public final /* synthetic */ Object zzb(Result result) {
        this.zzfvq.setResult(result);
        return this.zzfvq;
    }
}
