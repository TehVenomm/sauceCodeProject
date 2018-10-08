package com.google.android.gms.common.api.internal;

import android.os.RemoteException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.tasks.TaskCompletionSource;

public final class zzd extends zzb {
    private zzcr<zzb, ?> zzfhx;
    private zzdm<zzb, ?> zzfhy;

    public zzd(zzcs zzcs, TaskCompletionSource<Void> taskCompletionSource) {
        super(3, taskCompletionSource);
        this.zzfhx = zzcs.zzfhx;
        this.zzfhy = zzcs.zzfhy;
    }

    public final /* bridge */ /* synthetic */ void zza(@NonNull zzah zzah, boolean z) {
    }

    public final void zzb(zzbr<?> zzbr) throws RemoteException {
        this.zzfhx.zzb(zzbr.zzagm(), this.zzdzd);
        if (this.zzfhx.zzaik() != null) {
            zzbr.zzahv().put(this.zzfhx.zzaik(), new zzcs(this.zzfhx, this.zzfhy));
        }
    }

    public final /* bridge */ /* synthetic */ void zzq(@NonNull Status status) {
        super.zzq(status);
    }
}
