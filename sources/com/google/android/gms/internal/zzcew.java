package com.google.android.gms.internal;

final class zzcew implements Runnable {
    private /* synthetic */ zzcet zziwg;
    private /* synthetic */ zzcbg zziwh;

    zzcew(zzcet zzcet, zzcbg zzcbg) {
        this.zziwg = zzcet;
        this.zziwh = zzcbg;
    }

    public final void run() {
        synchronized (this.zziwg) {
            this.zziwg.zziwd = false;
            if (!this.zziwg.zzivw.isConnected()) {
                this.zziwg.zzivw.zzauk().zzayh().log("Connected to remote service");
                this.zziwg.zzivw.zza(this.zziwh);
            }
        }
    }
}
