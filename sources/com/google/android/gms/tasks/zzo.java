package com.google.android.gms.tasks;

import java.util.concurrent.Callable;

final class zzo implements Runnable {
    private /* synthetic */ Callable zzdbl;
    private /* synthetic */ zzn zzkgc;

    zzo(zzn zzn, Callable callable) {
        this.zzkgc = zzn;
        this.zzdbl = callable;
    }

    public final void run() {
        try {
            this.zzkgc.setResult(this.zzdbl.call());
        } catch (Exception e) {
            this.zzkgc.setException(e);
        }
    }
}
