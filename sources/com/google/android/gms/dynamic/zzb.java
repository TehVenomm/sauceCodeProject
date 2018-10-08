package com.google.android.gms.dynamic;

import java.util.Iterator;

final class zzb implements zzo<T> {
    private /* synthetic */ zza zzgop;

    zzb(zza zza) {
        this.zzgop = zza;
    }

    public final void zza(T t) {
        this.zzgop.zzgol = t;
        Iterator it = this.zzgop.zzgon.iterator();
        while (it.hasNext()) {
            ((zzi) it.next()).zzb(this.zzgop.zzgol);
        }
        this.zzgop.zzgon.clear();
        this.zzgop.zzgom = null;
    }
}
