package com.google.android.gms.internal;

final class zzeeg implements zzeem {
    static final zzeeg zzmyw = new zzeeg();
    private static zzeeh zzmyx = new zzeeh();

    private zzeeg() {
    }

    public final int zza(boolean z, int i, boolean z2, int i2) {
        if (z == z2 && i == i2) {
            return i;
        }
        throw zzmyx;
    }

    public final zzedk zza(boolean z, zzedk zzedk, boolean z2, zzedk zzedk2) {
        if (z == z2 && zzedk.equals(zzedk2)) {
            return zzedk;
        }
        throw zzmyx;
    }

    public final <T> zzeeq<T> zza(zzeeq<T> zzeeq, zzeeq<T> zzeeq2) {
        if (zzeeq.equals(zzeeq2)) {
            return zzeeq;
        }
        throw zzmyx;
    }

    public final <T extends zzeey> T zza(T t, T t2) {
        if (t == null && t2 == null) {
            return null;
        }
        if (t == null || t2 == null) {
            throw zzmyx;
        }
        T t3 = (zzeed) t;
        if (t3 == t2 || !((zzeed) t3.zza(zzeel.zzmzg, null, null)).getClass().isInstance(t2)) {
            return t;
        }
        Object obj = (zzeed) t2;
        t3.zza(zzeel.zzmzb, (Object) this, obj);
        t3.zzmyr = zza(t3.zzmyr, obj.zzmyr);
        return t;
    }

    public final zzefq zza(zzefq zzefq, zzefq zzefq2) {
        if (zzefq.equals(zzefq2)) {
            return zzefq;
        }
        throw zzmyx;
    }

    public final String zza(boolean z, String str, boolean z2, String str2) {
        if (z == z2 && str.equals(str2)) {
            return str;
        }
        throw zzmyx;
    }
}
