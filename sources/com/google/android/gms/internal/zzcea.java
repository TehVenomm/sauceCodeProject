package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

final class zzcea implements Runnable {
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ AtomicReference zziur;

    zzcea(zzcdo zzcdo, AtomicReference atomicReference) {
        this.zziup = zzcdo;
        this.zziur = atomicReference;
    }

    public final void run() {
        this.zziup.zzaub().zza(this.zziur);
    }
}
