package com.google.android.gms.internal;

import android.os.RemoteException;

final class zzcem implements Runnable {
    private /* synthetic */ zzceg zzivw;

    zzcem(zzceg zzceg) {
        this.zzivw = zzceg;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Failed to send measurementEnabled to service");
            return;
        }
        try {
            zzd.zzb(this.zzivw.zzatz().zzjb(this.zzivw.zzauk().zzayj()));
            this.zzivw.zzwt();
        } catch (RemoteException e) {
            this.zzivw.zzauk().zzayc().zzj("Failed to send measurementEnabled to the service", e);
        }
    }
}
