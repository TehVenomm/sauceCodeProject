package com.google.android.gms.internal;

import android.content.ComponentName;

final class zzcev implements Runnable {
    private /* synthetic */ ComponentName val$name;
    private /* synthetic */ zzcet zziwg;

    zzcev(zzcet zzcet, ComponentName componentName) {
        this.zziwg = zzcet;
        this.val$name = componentName;
    }

    public final void run() {
        this.zziwg.zzivw.onServiceDisconnected(this.val$name);
    }
}
