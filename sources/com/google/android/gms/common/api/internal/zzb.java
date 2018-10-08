package com.google.android.gms.common.api.internal;

import android.os.DeadObjectException;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

abstract class zzb extends zza {
    protected final TaskCompletionSource<Void> zzdzd;

    public zzb(int i, TaskCompletionSource<Void> taskCompletionSource) {
        super(i);
        this.zzdzd = taskCompletionSource;
    }

    public void zza(@NonNull zzah zzah, boolean z) {
    }

    public final void zza(zzbr<?> zzbr) throws DeadObjectException {
        try {
            zzb(zzbr);
        } catch (RemoteException e) {
            zzq(zza.zza(e));
            throw e;
        } catch (RemoteException e2) {
            zzq(zza.zza(e2));
        }
    }

    protected abstract void zzb(zzbr<?> zzbr) throws RemoteException;

    public void zzq(@NonNull Status status) {
        this.zzdzd.trySetException(new ApiException(status));
    }
}
