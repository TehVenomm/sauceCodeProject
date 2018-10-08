package com.google.android.gms.internal;

final class zzcdp implements Runnable {
    private /* synthetic */ boolean val$enabled;
    private /* synthetic */ zzcdo zziup;

    zzcdp(zzcdo zzcdo, boolean z) {
        this.zziup = zzcdo;
        this.val$enabled = z;
    }

    public final void run() {
        this.zziup.zzbp(this.val$enabled);
    }
}
