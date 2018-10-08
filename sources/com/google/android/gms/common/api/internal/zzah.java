package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.WeakHashMap;

public final class zzah {
    private final Map<zzs<?>, Boolean> zzfku = Collections.synchronizedMap(new WeakHashMap());
    private final Map<TaskCompletionSource<?>, Boolean> zzfkv = Collections.synchronizedMap(new WeakHashMap());

    private final void zza(boolean z, Status status) {
        synchronized (this.zzfku) {
            Map hashMap = new HashMap(this.zzfku);
        }
        synchronized (this.zzfkv) {
            Map hashMap2 = new HashMap(this.zzfkv);
        }
        for (Entry entry : hashMap.entrySet()) {
            if (z || ((Boolean) entry.getValue()).booleanValue()) {
                ((zzs) entry.getKey()).zzt(status);
            }
        }
        for (Entry entry2 : hashMap2.entrySet()) {
            if (z || ((Boolean) entry2.getValue()).booleanValue()) {
                ((TaskCompletionSource) entry2.getKey()).trySetException(new ApiException(status));
            }
        }
    }

    final void zza(zzs<? extends Result> zzs, boolean z) {
        this.zzfku.put(zzs, Boolean.valueOf(z));
        zzs.zza(new zzai(this, zzs));
    }

    final <TResult> void zza(TaskCompletionSource<TResult> taskCompletionSource, boolean z) {
        this.zzfkv.put(taskCompletionSource, Boolean.valueOf(z));
        taskCompletionSource.getTask().addOnCompleteListener(new zzaj(this, taskCompletionSource));
    }

    final boolean zzagr() {
        return (this.zzfku.isEmpty() && this.zzfkv.isEmpty()) ? false : true;
    }

    public final void zzags() {
        zza(false, zzbp.zzfne);
    }

    public final void zzagt() {
        zza(true, zzdi.zzfpk);
    }
}
