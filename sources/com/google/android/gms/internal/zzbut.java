package com.google.android.gms.internal;

public abstract class zzbut<T> {
    private final int zzbfk;
    private final String zzbfl;
    private final T zzbfm;

    private zzbut(int i, String str, T t) {
        this.zzbfk = i;
        this.zzbfl = str;
        this.zzbfm = t;
        zzbve.zzape().zza(this);
    }

    public static zzbuv zzb(int i, String str, Boolean bool) {
        return new zzbuv(0, str, bool);
    }

    public static zzbuw zzb(int i, String str, int i2) {
        return new zzbuw(0, str, Integer.valueOf(i2));
    }

    public static zzbux zzb(int i, String str, long j) {
        return new zzbux(0, str, Long.valueOf(j));
    }

    public final String getKey() {
        return this.zzbfl;
    }

    public final int getSource() {
        return this.zzbfk;
    }

    protected abstract T zza(zzbvb zzbvb);

    public final T zzik() {
        return this.zzbfm;
    }
}
