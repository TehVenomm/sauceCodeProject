package com.google.android.gms.internal;

final class zzcfh implements Runnable {
    private /* synthetic */ long zzikq;
    private /* synthetic */ zzcfd zziwq;

    zzcfh(zzcfd zzcfd, long j) {
        this.zziwq = zzcfd;
        this.zzikq = j;
    }

    public final void run() {
        this.zziwq.zzbe(this.zzikq);
    }
}
