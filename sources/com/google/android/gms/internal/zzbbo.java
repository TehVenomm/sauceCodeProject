package com.google.android.gms.internal;

public class zzbbo<T> {
    private static String READ_PERMISSION = "com.google.android.providers.gsf.permission.READ_GSERVICES";
    private static final Object zzaqm = new Object();
    private static zzbbu zzfps = null;
    private static int zzfpt = 0;
    private String zzbfl;
    private T zzbfm;
    private T zzfpu = null;

    protected zzbbo(String str, T t) {
        this.zzbfl = str;
        this.zzbfm = t;
    }

    public static zzbbo<Float> zza(String str, Float f) {
        return new zzbbs(str, f);
    }

    public static zzbbo<Integer> zza(String str, Integer num) {
        return new zzbbr(str, num);
    }

    public static zzbbo<Long> zza(String str, Long l) {
        return new zzbbq(str, l);
    }

    public static zzbbo<Boolean> zzf(String str, boolean z) {
        return new zzbbp(str, Boolean.valueOf(z));
    }

    public static zzbbo<String> zzv(String str, String str2) {
        return new zzbbt(str, str2);
    }
}
