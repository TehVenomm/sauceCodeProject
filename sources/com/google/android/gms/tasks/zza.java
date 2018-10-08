package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

final class zza<TResult, TContinuationResult> implements zzk<TResult> {
    private final Executor zzjqg;
    private final Continuation<TResult, TContinuationResult> zzkfj;
    private final zzn<TContinuationResult> zzkfk;

    public zza(@NonNull Executor executor, @NonNull Continuation<TResult, TContinuationResult> continuation, @NonNull zzn<TContinuationResult> zzn) {
        this.zzjqg = executor;
        this.zzkfj = continuation;
        this.zzkfk = zzn;
    }

    public final void cancel() {
        throw new UnsupportedOperationException();
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        this.zzjqg.execute(new zzb(this, task));
    }
}
