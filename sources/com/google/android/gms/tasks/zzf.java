package com.google.android.gms.tasks;

final class zzf implements Runnable {
    private /* synthetic */ Task zzkfl;
    private /* synthetic */ zze zzkfp;

    zzf(zze zze, Task task) {
        this.zzkfp = zze;
        this.zzkfl = task;
    }

    public final void run() {
        synchronized (this.zzkfp.mLock) {
            if (this.zzkfp.zzkfo != null) {
                this.zzkfp.zzkfo.onComplete(this.zzkfl);
            }
        }
    }
}
