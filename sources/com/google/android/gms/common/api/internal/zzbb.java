package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;

abstract class zzbb implements Runnable {
    private /* synthetic */ zzar zzflr;

    private zzbb(zzar zzar) {
        this.zzflr = zzar;
    }

    @WorkerThread
    public void run() {
        this.zzflr.zzfjy.lock();
        try {
            if (!Thread.interrupted()) {
                zzagy();
                this.zzflr.zzfjy.unlock();
            }
        } catch (RuntimeException e) {
            this.zzflr.zzflb.zza(e);
        } finally {
            this.zzflr.zzfjy.unlock();
        }
    }

    @WorkerThread
    protected abstract void zzagy();
}
