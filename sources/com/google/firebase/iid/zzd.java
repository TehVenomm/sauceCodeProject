package com.google.firebase.iid;

import android.content.BroadcastReceiver.PendingResult;
import android.content.Intent;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;

final class zzd {
    final Intent intent;
    private final PendingResult zzmij;
    private boolean zzmik = false;
    private final ScheduledFuture<?> zzmil;

    zzd(Intent intent, PendingResult pendingResult, ScheduledExecutorService scheduledExecutorService) {
        this.intent = intent;
        this.zzmij = pendingResult;
        this.zzmil = scheduledExecutorService.schedule(new zze(this, intent), 9500, TimeUnit.MILLISECONDS);
    }

    final void finish() {
        synchronized (this) {
            if (!this.zzmik) {
                this.zzmij.finish();
                this.zzmil.cancel(false);
                this.zzmik = true;
            }
        }
    }
}
