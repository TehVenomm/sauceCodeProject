package com.google.android.gms.tasks;

final class zzd implements Runnable {
    private /* synthetic */ Task zzkfl;
    private /* synthetic */ zzc zzkfn;

    zzd(zzc zzc, Task task) {
        this.zzkfn = zzc;
        this.zzkfl = task;
    }

    public final void run() {
        try {
            Task task = (Task) this.zzkfn.zzkfj.then(this.zzkfl);
            if (task == null) {
                this.zzkfn.onFailure(new NullPointerException("Continuation returned null"));
                return;
            }
            task.addOnSuccessListener(TaskExecutors.zzkfx, this.zzkfn);
            task.addOnFailureListener(TaskExecutors.zzkfx, this.zzkfn);
        } catch (Exception e) {
            if (e.getCause() instanceof Exception) {
                this.zzkfn.zzkfk.setException((Exception) e.getCause());
            } else {
                this.zzkfn.zzkfk.setException(e);
            }
        } catch (Exception e2) {
            this.zzkfn.zzkfk.setException(e2);
        }
    }
}
