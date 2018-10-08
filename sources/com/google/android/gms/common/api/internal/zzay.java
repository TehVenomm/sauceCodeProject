package com.google.android.gms.common.api.internal;

import android.support.annotation.BinderThread;
import com.google.android.gms.internal.zzcpr;
import com.google.android.gms.internal.zzcpz;
import java.lang.ref.WeakReference;

final class zzay extends zzcpr {
    private final WeakReference<zzar> zzfls;

    zzay(zzar zzar) {
        this.zzfls = new WeakReference(zzar);
    }

    @BinderThread
    public final void zzb(zzcpz zzcpz) {
        zzar zzar = (zzar) this.zzfls.get();
        if (zzar != null) {
            zzar.zzflb.zza(new zzaz(this, zzar, zzar, zzcpz));
        }
    }
}
