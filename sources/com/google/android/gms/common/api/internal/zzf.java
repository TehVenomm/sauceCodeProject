package com.google.android.gms.common.api.internal;

import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

public final class zzf extends zzb {
    private zzcl<?> zzfib;

    public zzf(zzcl<?> zzcl, TaskCompletionSource<Void> taskCompletionSource) {
        super(4, taskCompletionSource);
        this.zzfib = zzcl;
    }

    public final /* bridge */ /* synthetic */ void zza(@NonNull zzah zzah, boolean z) {
    }

    public final void zzb(zzbr<?> zzbr) throws RemoteException {
        zzcs zzcs = (zzcs) zzbr.zzahv().remove(this.zzfib);
        if (zzcs != null) {
            zzcs.zzfhy.zzc(zzbr.zzagm(), this.zzdzd);
            zzcs.zzfhx.zzail();
            return;
        }
        this.zzdzd.trySetException(new ApiException(new Status(13, "listener already unregistered")));
    }

    public final /* bridge */ /* synthetic */ void zzq(@NonNull Status status) {
        super.zzq(status);
    }
}
