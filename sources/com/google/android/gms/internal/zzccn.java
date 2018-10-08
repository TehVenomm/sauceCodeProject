package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.FutureTask;

final class zzccn extends Thread {
    private /* synthetic */ zzccj zzisd;
    private final Object zzisg = new Object();
    private final BlockingQueue<FutureTask<?>> zzish;

    public zzccn(zzccj zzccj, String str, BlockingQueue<FutureTask<?>> blockingQueue) {
        this.zzisd = zzccj;
        zzbp.zzu(str);
        zzbp.zzu(blockingQueue);
        this.zzish = blockingQueue;
        setName(str);
    }

    private final void zza(InterruptedException interruptedException) {
        this.zzisd.zzauk().zzaye().zzj(String.valueOf(getName()).concat(" was interrupted"), interruptedException);
    }

    public final void run() {
        Object obj = null;
        while (obj == null) {
            try {
                this.zzisd.zzirz.acquire();
                obj = 1;
            } catch (InterruptedException e) {
                zza(e);
            }
        }
        while (true) {
            try {
                FutureTask futureTask = (FutureTask) this.zzish.poll();
                if (futureTask != null) {
                    futureTask.run();
                } else {
                    synchronized (this.zzisg) {
                        if (this.zzish.peek() == null && !this.zzisd.zzisa) {
                            try {
                                this.zzisg.wait(30000);
                            } catch (InterruptedException e2) {
                                zza(e2);
                            }
                        }
                    }
                    synchronized (this.zzisd.zziry) {
                        if (this.zzish.peek() == null) {
                            break;
                        }
                    }
                }
            } catch (Throwable th) {
                synchronized (this.zzisd.zziry) {
                    this.zzisd.zzirz.release();
                    this.zzisd.zziry.notifyAll();
                    if (this == this.zzisd.zzirs) {
                        this.zzisd.zzirs = null;
                    } else if (this == this.zzisd.zzirt) {
                        this.zzisd.zzirt = null;
                    } else {
                        this.zzisd.zzauk().zzayc().log("Current scheduler thread is neither worker nor network");
                    }
                }
            }
        }
        synchronized (this.zzisd.zziry) {
            this.zzisd.zzirz.release();
            this.zzisd.zziry.notifyAll();
            if (this == this.zzisd.zzirs) {
                this.zzisd.zzirs = null;
            } else if (this == this.zzisd.zzirt) {
                this.zzisd.zzirt = null;
            } else {
                this.zzisd.zzauk().zzayc().log("Current scheduler thread is neither worker nor network");
            }
        }
    }

    public final void zzmi() {
        synchronized (this.zzisg) {
            this.zzisg.notifyAll();
        }
    }
}
