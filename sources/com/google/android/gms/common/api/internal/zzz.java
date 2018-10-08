package com.google.android.gms.common.api.internal;

final class zzz implements Runnable {
    private /* synthetic */ zzy zzfka;

    zzz(zzy zzy) {
        this.zzfka = zzy;
    }

    public final void run() {
        this.zzfka.zzfjy.lock();
        try {
            this.zzfka.zzagi();
        } finally {
            this.zzfka.zzfjy.unlock();
        }
    }
}
