package com.google.android.gms.common.api.internal;

import android.os.DeadObjectException;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

public final class zze<TResult> extends zza {
    private final TaskCompletionSource<TResult> zzdzd;
    private final zzdd<zzb, TResult> zzfhz;
    private final zzcz zzfia;

    public zze(int i, zzdd<zzb, TResult> zzdd, TaskCompletionSource<TResult> taskCompletionSource, zzcz zzcz) {
        super(i);
        this.zzdzd = taskCompletionSource;
        this.zzfhz = zzdd;
        this.zzfia = zzcz;
    }

    public final void zza(@NonNull zzah zzah, boolean z) {
        zzah.zza(this.zzdzd, z);
    }

    public final void zza(zzbr<?> zzbr) throws DeadObjectException {
        try {
            this.zzfhz.zza(zzbr.zzagm(), this.zzdzd);
        } catch (DeadObjectException e) {
            throw e;
        } catch (RemoteException e2) {
            zzq(zza.zza(e2));
        }
    }

    public final void zzq(@NonNull Status status) {
        this.zzdzd.trySetException(this.zzfia.zzr(status));
    }
}
