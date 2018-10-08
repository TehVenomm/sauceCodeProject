package com.google.android.gms.tasks;

final class zzj implements Runnable {
    private /* synthetic */ Task zzkfl;
    private /* synthetic */ zzi zzkft;

    zzj(zzi zzi, Task task) {
        this.zzkft = zzi;
        this.zzkfl = task;
    }

    public final void run() {
        synchronized (this.zzkft.mLock) {
            if (this.zzkft.zzkfs != null) {
                this.zzkft.zzkfs.onSuccess(this.zzkfl.getResult());
            }
        }
    }
}
