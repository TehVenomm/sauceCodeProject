package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

final class zzc<TResult, TContinuationResult> implements OnFailureListener, OnSuccessListener<TContinuationResult>, zzk<TResult> {
    private final Executor zzjqg;
    private final Continuation<TResult, Task<TContinuationResult>> zzkfj;
    private final zzn<TContinuationResult> zzkfk;

    public zzc(@NonNull Executor executor, @NonNull Continuation<TResult, Task<TContinuationResult>> continuation, @NonNull zzn<TContinuationResult> zzn) {
        this.zzjqg = executor;
        this.zzkfj = continuation;
        this.zzkfk = zzn;
    }

    public final void cancel() {
        throw new UnsupportedOperationException();
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        this.zzjqg.execute(new zzd(this, task));
    }

    public final void onFailure(@NonNull Exception exception) {
        this.zzkfk.setException(exception);
    }

    public final void onSuccess(TContinuationResult tContinuationResult) {
        this.zzkfk.setResult(tContinuationResult);
    }
}
