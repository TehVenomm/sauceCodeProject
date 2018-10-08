package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

final class zzcdy implements Runnable {
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ AtomicReference zziur;
    private /* synthetic */ boolean zzius;

    zzcdy(zzcdo zzcdo, AtomicReference atomicReference, boolean z) {
        this.zziup = zzcdo;
        this.zziur = atomicReference;
        this.zzius = z;
    }

    public final void run() {
        this.zziup.zzaub().zza(this.zziur, this.zzius);
    }
}
