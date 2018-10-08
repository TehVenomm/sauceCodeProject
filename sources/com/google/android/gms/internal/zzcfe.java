package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.WorkerThread;

final class zzcfe extends zzcau {
    private /* synthetic */ zzcfd zziwq;

    zzcfe(zzcfd zzcfd, zzcco zzcco) {
        this.zziwq = zzcfd;
        super(zzcco);
    }

    @WorkerThread
    public final void run() {
        zzcdl zzcdl = this.zziwq;
        zzcdl.zzug();
        zzcdl.zzauk().zzayi().zzj("Session started, time", Long.valueOf(zzcdl.zzvu().elapsedRealtime()));
        zzcdl.zzaul().zziqv.set(false);
        zzcdl.zzaty().zzc("auto", "_s", new Bundle());
        zzcdl.zzaul().zziqw.set(zzcdl.zzvu().currentTimeMillis());
    }
}
