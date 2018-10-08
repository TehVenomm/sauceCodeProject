package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

final class zzg<TResult> implements zzk<TResult> {
    private final Object mLock = new Object();
    private final Executor zzjqg;
    private OnFailureListener zzkfq;

    public zzg(@NonNull Executor executor, @NonNull OnFailureListener onFailureListener) {
        this.zzjqg = executor;
        this.zzkfq = onFailureListener;
    }

    public final void cancel() {
        synchronized (this.mLock) {
            this.zzkfq = null;
        }
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        if (!task.isSuccessful()) {
            synchronized (this.mLock) {
                if (this.zzkfq == null) {
                    return;
                }
                this.zzjqg.execute(new zzh(this, task));
            }
        }
    }
}
