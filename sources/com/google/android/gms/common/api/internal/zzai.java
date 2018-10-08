package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.api.PendingResult.zza;
import com.google.android.gms.common.api.Status;

final class zzai implements zza {
    private /* synthetic */ zzs zzfkw;
    private /* synthetic */ zzah zzfkx;

    zzai(zzah zzah, zzs zzs) {
        this.zzfkx = zzah;
        this.zzfkw = zzs;
    }

    public final void zzp(Status status) {
        this.zzfkx.zzfku.remove(this.zzfkw);
    }
}
