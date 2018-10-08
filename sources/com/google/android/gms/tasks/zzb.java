package com.google.android.gms.tasks;

final class zzb implements Runnable {
    private /* synthetic */ Task zzkfl;
    private /* synthetic */ zza zzkfm;

    zzb(zza zza, Task task) {
        this.zzkfm = zza;
        this.zzkfl = task;
    }

    public final void run() {
        try {
            this.zzkfm.zzkfk.setResult(this.zzkfm.zzkfj.then(this.zzkfl));
        } catch (Exception e) {
            if (e.getCause() instanceof Exception) {
                this.zzkfm.zzkfk.setException((Exception) e.getCause());
            } else {
                this.zzkfm.zzkfk.setException(e);
            }
        } catch (Exception e2) {
            this.zzkfm.zzkfk.setException(e2);
        }
    }
}
