package com.google.android.gms.common.internal;

import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Response;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;

public final class zzbi {
    private static final zzbo zzfvl = new zzbj();

    public static <R extends Result, T extends Response<R>> Task<T> zza(PendingResult<R> pendingResult, T t) {
        return zza((PendingResult) pendingResult, new zzbl(t));
    }

    public static <R extends Result, T> Task<T> zza(PendingResult<R> pendingResult, zzbn<R, T> zzbn) {
        zzbo zzbo = zzfvl;
        TaskCompletionSource taskCompletionSource = new TaskCompletionSource();
        pendingResult.zza(new zzbk(pendingResult, taskCompletionSource, zzbn, zzbo));
        return taskCompletionSource.getTask();
    }

    public static <R extends Result> Task<Void> zzb(PendingResult<R> pendingResult) {
        return zza((PendingResult) pendingResult, new zzbm());
    }
}
