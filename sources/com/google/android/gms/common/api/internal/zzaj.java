package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;

final class zzaj implements OnCompleteListener<TResult> {
    private /* synthetic */ TaskCompletionSource zzejh;
    private /* synthetic */ zzah zzfkx;

    zzaj(zzah zzah, TaskCompletionSource taskCompletionSource) {
        this.zzfkx = zzah;
        this.zzejh = taskCompletionSource;
    }

    public final void onComplete(@NonNull Task<TResult> task) {
        this.zzfkx.zzfkv.remove(this.zzejh);
    }
}
