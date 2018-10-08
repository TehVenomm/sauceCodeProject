package com.google.android.gms.internal;

final class zzcfk extends zzcau {
    private /* synthetic */ zzcfj zziwt;

    zzcfk(zzcfj zzcfj, zzcco zzcco) {
        this.zziwt = zzcfj;
        super(zzcco);
    }

    public final void run() {
        this.zziwt.zzauk().zzayi().log("Sending upload intent from DelayedRunnable");
        this.zziwt.zzazv();
    }
}
