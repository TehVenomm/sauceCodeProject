package com.google.android.gms.tasks;

import android.support.annotation.NonNull;
import java.util.concurrent.Executor;

final class zze<TResult> implements zzk<TResult> {
    private final Object mLock = new Object();
    private final Executor zzjqg;
    private OnCompleteListener<TResult> zzkfo;

    public zze(@NonNull Executor executor, @NonNull OnCompleteListener<TResult> onCompleteListener) {
        this.zzjqg = executor;
        this.zzkfo = onCompleteListener;
    }

    public final void cancel() {
        synchronized (this.mLock) {
            this.zzkfo = null;
        }
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        synchronized (this.mLock) {
            if (this.zzkfo == null) {
                return;
            }
            this.zzjqg.execute(new zzf(this, task));
        }
    }
}
