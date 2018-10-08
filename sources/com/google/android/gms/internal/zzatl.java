package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzdd;
import com.google.android.gms.common.api.internal.zzde;
import com.google.android.gms.tasks.TaskCompletionSource;

abstract class zzatl extends zzdd<zzath, Void> {
    private TaskCompletionSource<Void> zzdzd;

    private zzatl() {
    }

    protected final /* synthetic */ void zza(zzb zzb, TaskCompletionSource taskCompletionSource) throws RemoteException {
        zzath zzath = (zzath) zzb;
        this.zzdzd = taskCompletionSource;
        zza((zzatd) zzath.zzajj());
    }

    protected abstract void zza(zzatd zzatd) throws RemoteException;

    protected final void zzh(Status status) {
        zzde.zza(status, null, this.zzdzd);
    }
}
