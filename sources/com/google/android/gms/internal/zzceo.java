package com.google.android.gms.internal;

import android.os.RemoteException;
import android.text.TextUtils;

final class zzceo implements Runnable {
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ boolean zzivz = true;
    private /* synthetic */ boolean zziwa;
    private /* synthetic */ zzcan zziwb;
    private /* synthetic */ zzcan zziwc;

    zzceo(zzceg zzceg, boolean z, boolean z2, zzcan zzcan, zzcan zzcan2) {
        this.zzivw = zzceg;
        this.zziwa = z2;
        this.zziwb = zzcan;
        this.zziwc = zzcan2;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Discarding data. Failed to send conditional user property to service");
            return;
        }
        if (this.zzivz) {
            this.zzivw.zza(zzd, this.zziwa ? null : this.zziwb);
        } else {
            try {
                if (TextUtils.isEmpty(this.zziwc.packageName)) {
                    zzd.zza(this.zziwb, this.zzivw.zzatz().zzjb(this.zzivw.zzauk().zzayj()));
                } else {
                    zzd.zzb(this.zziwb);
                }
            } catch (RemoteException e) {
                this.zzivw.zzauk().zzayc().zzj("Failed to send conditional user property to the service", e);
            }
        }
        this.zzivw.zzwt();
    }
}
