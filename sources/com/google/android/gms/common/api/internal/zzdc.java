package com.google.android.gms.common.api.internal;

final class zzdc implements Runnable {
    private /* synthetic */ String zzao;
    private /* synthetic */ LifecycleCallback zzfom;
    private /* synthetic */ zzdb zzfpa;

    zzdc(zzdb zzdb, LifecycleCallback lifecycleCallback, String str) {
        this.zzfpa = zzdb;
        this.zzfom = lifecycleCallback;
        this.zzao = str;
    }

    public final void run() {
        if (this.zzfpa.zzbyx > 0) {
            this.zzfom.onCreate(this.zzfpa.zzfol != null ? this.zzfpa.zzfol.getBundle(this.zzao) : null);
        }
        if (this.zzfpa.zzbyx >= 2) {
            this.zzfom.onStart();
        }
        if (this.zzfpa.zzbyx >= 3) {
            this.zzfom.onResume();
        }
        if (this.zzfpa.zzbyx >= 4) {
            this.zzfom.onStop();
        }
        if (this.zzfpa.zzbyx >= 5) {
            this.zzfom.onDestroy();
        }
    }
}
