package com.google.android.gms.internal;

public final class zzeek implements zzeem {
    public static final zzeek zzmyz = new zzeek();

    private zzeek() {
    }

    public final int zza(boolean z, int i, boolean z2, int i2) {
        return z2 ? i2 : i;
    }

    public final zzedk zza(boolean z, zzedk zzedk, boolean z2, zzedk zzedk2) {
        return z2 ? zzedk2 : zzedk;
    }

    public final <T> zzeeq<T> zza(zzeeq<T> zzeeq, zzeeq<T> zzeeq2) {
        int size = zzeeq.size();
        int size2 = zzeeq2.size();
        if (size > 0 && size2 > 0) {
            if (!zzeeq.zzcbk()) {
                zzeeq = zzeeq.zzgu(size2 + size);
            }
            zzeeq.addAll(zzeeq2);
        }
        return size > 0 ? zzeeq : zzeeq2;
    }

    public final <T extends zzeey> T zza(T t, T t2) {
        return (t == null || t2 == null) ? t != null ? t : t2 : t.zzccn().zzc(t2).zzccs();
    }

    public final zzefq zza(zzefq zzefq, zzefq zzefq2) {
        return zzefq2 == zzefq.zzcdh() ? zzefq : zzefq.zzb(zzefq, zzefq2);
    }

    public final String zza(boolean z, String str, boolean z2, String str2) {
        return z2 ? str2 : str;
    }
}
