package com.google.android.gms.internal;

import android.os.RemoteException;

final class zzcej implements Runnable {
    private /* synthetic */ zzceg zzivw;

    zzcej(zzceg zzceg) {
        this.zzivw = zzceg;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Discarding data. Failed to send app launch");
            return;
        }
        try {
            zzd.zza(this.zzivw.zzatz().zzjb(this.zzivw.zzauk().zzayj()));
            this.zzivw.zza(zzd, null);
            this.zzivw.zzwt();
        } catch (RemoteException e) {
            this.zzivw.zzauk().zzayc().zzj("Failed to send app launch to the service", e);
        }
    }
}
