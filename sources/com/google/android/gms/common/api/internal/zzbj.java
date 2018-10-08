package com.google.android.gms.common.api.internal;

import java.lang.ref.WeakReference;

final class zzbj extends zzbz {
    private WeakReference<zzbd> zzfmt;

    zzbj(zzbd zzbd) {
        this.zzfmt = new WeakReference(zzbd);
    }

    public final void zzagd() {
        zzbd zzbd = (zzbd) this.zzfmt.get();
        if (zzbd != null) {
            zzbd.resume();
        }
    }
}
