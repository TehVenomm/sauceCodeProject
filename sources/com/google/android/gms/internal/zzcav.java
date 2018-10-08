package com.google.android.gms.internal;

import android.os.Looper;

final class zzcav implements Runnable {
    private /* synthetic */ zzcau zzimy;

    zzcav(zzcau zzcau) {
        this.zzimy = zzcau;
    }

    public final void run() {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            this.zzimy.zzikb.zzauj().zzg(this);
            return;
        }
        boolean zzdp = this.zzimy.zzdp();
        this.zzimy.zzdqy = 0;
        if (zzdp && this.zzimy.zzimx) {
            this.zzimy.run();
        }
    }
}
