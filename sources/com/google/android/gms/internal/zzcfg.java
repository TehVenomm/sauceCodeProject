package com.google.android.gms.internal;

final class zzcfg implements Runnable {
    private /* synthetic */ long zzikq;
    private /* synthetic */ zzcfd zziwq;

    zzcfg(zzcfd zzcfd, long j) {
        this.zziwq = zzcfd;
        this.zzikq = j;
    }

    public final void run() {
        this.zziwq.zzbd(this.zzikq);
    }
}
