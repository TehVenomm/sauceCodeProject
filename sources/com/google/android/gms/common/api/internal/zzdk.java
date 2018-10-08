package com.google.android.gms.common.api.internal;

import android.os.IBinder;
import android.os.IBinder.DeathRecipient;
import com.google.android.gms.common.api.zzf;
import java.lang.ref.WeakReference;

final class zzdk implements DeathRecipient, zzdl {
    private final WeakReference<zzs<?>> zzfpp;
    private final WeakReference<zzf> zzfpq;
    private final WeakReference<IBinder> zzfpr;

    private zzdk(zzs<?> zzs, zzf zzf, IBinder iBinder) {
        this.zzfpq = new WeakReference(zzf);
        this.zzfpp = new WeakReference(zzs);
        this.zzfpr = new WeakReference(iBinder);
    }

    private final void zzair() {
        zzs zzs = (zzs) this.zzfpp.get();
        zzf zzf = (zzf) this.zzfpq.get();
        if (!(zzf == null || zzs == null)) {
            zzf.remove(zzs.zzafr().intValue());
        }
        IBinder iBinder = (IBinder) this.zzfpr.get();
        if (iBinder != null) {
            iBinder.unlinkToDeath(this, 0);
        }
    }

    public final void binderDied() {
        zzair();
    }

    public final void zzc(zzs<?> zzs) {
        zzair();
    }
}
