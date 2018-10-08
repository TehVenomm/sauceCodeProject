package com.google.android.gms.drive.events;

import android.os.Looper;
import com.google.android.gms.internal.zzbjv;
import java.util.concurrent.CountDownLatch;

final class zzh extends Thread {
    private /* synthetic */ CountDownLatch zzgfk;
    private /* synthetic */ DriveEventService zzgfl;

    zzh(DriveEventService driveEventService, CountDownLatch countDownLatch) {
        this.zzgfl = driveEventService;
        this.zzgfk = countDownLatch;
    }

    public final void run() {
        try {
            Looper.prepare();
            this.zzgfl.zzgfh = new zza(this.zzgfl);
            this.zzgfl.zzgfi = false;
            this.zzgfk.countDown();
            zzbjv.zzx("DriveEventService", "Bound and starting loop");
            Looper.loop();
            zzbjv.zzx("DriveEventService", "Finished loop");
        } finally {
            if (this.zzgfl.zzgfg != null) {
                this.zzgfl.zzgfg.countDown();
            }
        }
    }
}
