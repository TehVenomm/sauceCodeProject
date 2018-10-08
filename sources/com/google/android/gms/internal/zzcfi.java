package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;

final class zzcfi {
    private long mStartTime;
    private final zzd zzasl;

    public zzcfi(zzd zzd) {
        zzbp.zzu(zzd);
        this.zzasl = zzd;
    }

    public final void clear() {
        this.mStartTime = 0;
    }

    public final void start() {
        this.mStartTime = this.zzasl.elapsedRealtime();
    }

    public final boolean zzu(long j) {
        return this.mStartTime == 0 || this.zzasl.elapsedRealtime() - this.mStartTime >= j;
    }
}
