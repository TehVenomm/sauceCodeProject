package com.google.android.gms.internal;

import android.os.RemoteException;
import android.text.TextUtils;

final class zzcen implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcbc zziuf;
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ boolean zzivz = true;
    private /* synthetic */ boolean zziwa;

    zzcen(zzceg zzceg, boolean z, boolean z2, zzcbc zzcbc, String str) {
        this.zzivw = zzceg;
        this.zziwa = z2;
        this.zziuf = zzcbc;
        this.zziab = str;
    }

    public final void run() {
        zzcbg zzd = this.zzivw.zzivq;
        if (zzd == null) {
            this.zzivw.zzauk().zzayc().log("Discarding data. Failed to send event to service");
            return;
        }
        if (this.zzivz) {
            this.zzivw.zza(zzd, this.zziwa ? null : this.zziuf);
        } else {
            try {
                if (TextUtils.isEmpty(this.zziab)) {
                    zzd.zza(this.zziuf, this.zzivw.zzatz().zzjb(this.zzivw.zzauk().zzayj()));
                } else {
                    zzd.zza(this.zziuf, this.zziab, this.zzivw.zzauk().zzayj());
                }
            } catch (RemoteException e) {
                this.zzivw.zzauk().zzayc().zzj("Failed to send event to the service", e);
            }
        }
        this.zzivw.zzwt();
    }
}
