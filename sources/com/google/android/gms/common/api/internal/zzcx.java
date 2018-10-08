package com.google.android.gms.common.api.internal;

import com.google.android.gms.internal.zzcpz;

final class zzcx implements Runnable {
    private /* synthetic */ zzcpz zzflz;
    private /* synthetic */ zzcw zzfoz;

    zzcx(zzcw zzcw, zzcpz zzcpz) {
        this.zzfoz = zzcw;
        this.zzflz = zzcpz;
    }

    public final void run() {
        this.zzfoz.zzc(this.zzflz);
    }
}
