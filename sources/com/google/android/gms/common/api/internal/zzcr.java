package com.google.android.gms.common.api.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.tasks.TaskCompletionSource;

public abstract class zzcr<A extends zzb, L> {
    private final zzcj<L> zzfow;

    protected zzcr(zzcj<L> zzcj) {
        this.zzfow = zzcj;
    }

    public final zzcl<L> zzaik() {
        return this.zzfow.zzaik();
    }

    public final void zzail() {
        this.zzfow.clear();
    }

    protected abstract void zzb(A a, TaskCompletionSource<Void> taskCompletionSource) throws RemoteException;
}
