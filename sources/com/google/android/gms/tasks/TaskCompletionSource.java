package com.google.android.gms.tasks;

import android.support.annotation.NonNull;

public class TaskCompletionSource<TResult> {
    private final zzn<TResult> zzkfw = new zzn();

    @NonNull
    public Task<TResult> getTask() {
        return this.zzkfw;
    }

    public void setException(@NonNull Exception exception) {
        this.zzkfw.setException(exception);
    }

    public void setResult(TResult tResult) {
        this.zzkfw.setResult(tResult);
    }

    public boolean trySetException(@NonNull Exception exception) {
        return this.zzkfw.trySetException(exception);
    }

    public boolean trySetResult(TResult tResult) {
        return this.zzkfw.trySetResult(tResult);
    }
}
