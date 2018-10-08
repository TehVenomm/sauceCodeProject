package com.google.android.gms.common.api;

import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.TimeUnit;

public final class BatchResult implements Result {
    private final Status mStatus;
    private final PendingResult<?>[] zzfgh;

    BatchResult(Status status, PendingResult<?>[] pendingResultArr) {
        this.mStatus = status;
        this.zzfgh = pendingResultArr;
    }

    public final Status getStatus() {
        return this.mStatus;
    }

    public final <R extends Result> R take(BatchResultToken<R> batchResultToken) {
        zzbp.zzb(batchResultToken.mId < this.zzfgh.length, (Object) "The result token does not belong to this batch");
        return this.zzfgh[batchResultToken.mId].await(0, TimeUnit.MILLISECONDS);
    }
}
