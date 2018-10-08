package com.google.android.gms.common.internal;

import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.PendingResult.zza;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.concurrent.TimeUnit;

final class zzbk implements zza {
    private /* synthetic */ PendingResult zzfvm;
    private /* synthetic */ TaskCompletionSource zzfvn;
    private /* synthetic */ zzbn zzfvo;
    private /* synthetic */ zzbo zzfvp;

    zzbk(PendingResult pendingResult, TaskCompletionSource taskCompletionSource, zzbn zzbn, zzbo zzbo) {
        this.zzfvm = pendingResult;
        this.zzfvn = taskCompletionSource;
        this.zzfvo = zzbn;
        this.zzfvp = zzbo;
    }

    public final void zzp(Status status) {
        if (status.isSuccess()) {
            this.zzfvn.setResult(this.zzfvo.zzb(this.zzfvm.await(0, TimeUnit.MILLISECONDS)));
            return;
        }
        this.zzfvn.setException(this.zzfvp.zzy(status));
    }
}
