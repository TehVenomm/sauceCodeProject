package com.google.android.gms.internal;

public final class zzt<T> {
    public final T result;
    public final zzc zzbe;
    public final zzaa zzbf;
    public boolean zzbg;

    private zzt(zzaa zzaa) {
        this.zzbg = false;
        this.result = null;
        this.zzbe = null;
        this.zzbf = zzaa;
    }

    private zzt(T t, zzc zzc) {
        this.zzbg = false;
        this.result = t;
        this.zzbe = zzc;
        this.zzbf = null;
    }

    public static <T> zzt<T> zza(T t, zzc zzc) {
        return new zzt(t, zzc);
    }

    public static <T> zzt<T> zzc(zzaa zzaa) {
        return new zzt(zzaa);
    }
}
