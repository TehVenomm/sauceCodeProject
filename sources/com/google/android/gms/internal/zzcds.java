package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

final class zzcds implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ String zziud;
    private /* synthetic */ String zziue;
    private /* synthetic */ zzcdo zziup;
    private /* synthetic */ AtomicReference zziur;

    zzcds(zzcdo zzcdo, AtomicReference atomicReference, String str, String str2, String str3) {
        this.zziup = zzcdo;
        this.zziur = atomicReference;
        this.zziab = str;
        this.zziud = str2;
        this.zziue = str3;
    }

    public final void run() {
        this.zziup.zzikb.zzaub().zza(this.zziur, this.zziab, this.zziud, this.zziue);
    }
}
