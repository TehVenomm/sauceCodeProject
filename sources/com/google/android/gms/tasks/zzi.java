package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

final class zzi<TResult> implements zzk<TResult> {
    private final Object mLock = new Object();
    private final Executor zzjqg;
    private OnSuccessListener<? super TResult> zzkfs;

    public zzi(@NonNull Executor executor, @NonNull OnSuccessListener<? super TResult> onSuccessListener) {
        this.zzjqg = executor;
        this.zzkfs = onSuccessListener;
    }

    public final void cancel() {
        synchronized (this.mLock) {
            this.zzkfs = null;
        }
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        if (task.isSuccessful()) {
            synchronized (this.mLock) {
                if (this.zzkfs == null) {
                    return;
                }
                this.zzjqg.execute(new zzj(this, task));
            }
        }
    }
}
