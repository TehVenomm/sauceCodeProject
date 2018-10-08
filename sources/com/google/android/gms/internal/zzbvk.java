package com.google.android.gms.internal;

import java.util.concurrent.atomic.AtomicReference;

public abstract class zzbvk {
    private final AtomicReference<zzbvi> zzhiu = new AtomicReference();

    public final void flush() {
        zzbvi zzbvi = (zzbvi) this.zzhiu.get();
        if (zzbvi != null) {
            zzbvi.flush();
        }
    }

    protected abstract zzbvi zzaqt();

    public final void zzp(String str, int i) {
        zzbvi zzbvi = (zzbvi) this.zzhiu.get();
        if (zzbvi == null) {
            zzbvi = zzaqt();
            if (!this.zzhiu.compareAndSet(null, zzbvi)) {
                zzbvi = (zzbvi) this.zzhiu.get();
            }
        }
        zzbvi.zzt(str, i);
    }
}
