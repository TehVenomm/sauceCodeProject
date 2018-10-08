package com.google.android.gms.common.api.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.tasks.TaskCompletionSource;

public abstract class zzdm<A extends zzb, L> {
    private final zzcl<L> zzfop;

    protected zzdm(zzcl<L> zzcl) {
        this.zzfop = zzcl;
    }

    public final zzcl<L> zzaik() {
        return this.zzfop;
    }

    protected abstract void zzc(A a, TaskCompletionSource<Void> taskCompletionSource) throws RemoteException;
}
