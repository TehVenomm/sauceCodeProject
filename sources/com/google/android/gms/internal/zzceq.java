package com.google.android.gms.internal;

import android.os.RemoteException;
import android.text.TextUtils;
import java.util.Collections;
import java.util.concurrent.atomic.AtomicReference;

final class zzceq implements Runnable {
    private /* synthetic */ String zziab;
    private /* synthetic */ String zziud;
    private /* synthetic */ String zziue;
    private /* synthetic */ boolean zzius;
    private /* synthetic */ zzceg zzivw;
    private /* synthetic */ AtomicReference zzivx;

    zzceq(zzceg zzceg, AtomicReference atomicReference, String str, String str2, String str3, boolean z) {
        this.zzivw = zzceg;
        this.zzivx = atomicReference;
        this.zziab = str;
        this.zziud = str2;
        this.zziue = str3;
        this.zzius = z;
    }

    public final void run() {
        synchronized (this.zzivx) {
            try {
                zzcbg zzd = this.zzivw.zzivq;
                if (zzd == null) {
                    this.zzivw.zzauk().zzayc().zzd("Failed to get user properties", zzcbo.zzjf(this.zziab), this.zziud, this.zziue);
                    this.zzivx.set(Collections.emptyList());
                    this.zzivx.notify();
                    return;
                }
                if (TextUtils.isEmpty(this.zziab)) {
                    this.zzivx.set(zzd.zza(this.zziud, this.zziue, this.zzius, this.zzivw.zzatz().zzjb(this.zzivw.zzauk().zzayj())));
                } else {
                    this.zzivx.set(zzd.zza(this.zziab, this.zziud, this.zziue, this.zzius));
                }
                this.zzivw.zzwt();
                this.zzivx.notify();
            } catch (RemoteException e) {
                this.zzivw.zzauk().zzayc().zzd("Failed to get user properties", zzcbo.zzjf(this.zziab), this.zziud, e);
                this.zzivx.set(Collections.emptyList());
                this.zzivx.notify();
            } catch (Throwable th) {
                this.zzivx.notify();
            }
        }
    }
}
