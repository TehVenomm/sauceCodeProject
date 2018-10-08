package com.google.android.gms.common.api.internal;

final class zzci implements Runnable {
    private /* synthetic */ String zzao;
    private /* synthetic */ LifecycleCallback zzfom;
    private /* synthetic */ zzch zzfon;

    zzci(zzch zzch, LifecycleCallback lifecycleCallback, String str) {
        this.zzfon = zzch;
        this.zzfom = lifecycleCallback;
        this.zzao = str;
    }

    public final void run() {
        if (this.zzfon.zzbyx > 0) {
            this.zzfom.onCreate(this.zzfon.zzfol != null ? this.zzfon.zzfol.getBundle(this.zzao) : null);
        }
        if (this.zzfon.zzbyx >= 2) {
            this.zzfom.onStart();
        }
        if (this.zzfon.zzbyx >= 3) {
            this.zzfom.onResume();
        }
        if (this.zzfon.zzbyx >= 4) {
            this.zzfom.onStop();
        }
        if (this.zzfon.zzbyx >= 5) {
            this.zzfom.onDestroy();
        }
    }
}
