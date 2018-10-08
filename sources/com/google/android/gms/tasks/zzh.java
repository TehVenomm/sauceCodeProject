package com.google.android.gms.tasks;

final class zzh implements Runnable {
    private /* synthetic */ Task zzkfl;
    private /* synthetic */ zzg zzkfr;

    zzh(zzg zzg, Task task) {
        this.zzkfr = zzg;
        this.zzkfl = task;
    }

    public final void run() {
        synchronized (this.zzkfr.mLock) {
            if (this.zzkfr.zzkfq != null) {
                this.zzkfr.zzkfq.onFailure(this.zzkfl.getException());
            }
        }
    }
}
