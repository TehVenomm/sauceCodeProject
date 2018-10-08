package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;

public final class zzcbf<V> {
    private final String zzbfl;
    private final V zzdsu;
    private final zzbbo<V> zzdsv;

    private zzcbf(String str, zzbbo<V> zzbbo, V v) {
        zzbp.zzu(zzbbo);
        this.zzdsv = zzbbo;
        this.zzdsu = v;
        this.zzbfl = str;
    }

    static zzcbf<Long> zzb(String str, long j, long j2) {
        return new zzcbf(str, zzbbo.zza(str, Long.valueOf(j2)), Long.valueOf(j));
    }

    static zzcbf<Boolean> zzb(String str, boolean z, boolean z2) {
        return new zzcbf(str, zzbbo.zzf(str, z2), Boolean.valueOf(z));
    }

    static zzcbf<String> zzi(String str, String str2, String str3) {
        return new zzcbf(str, zzbbo.zzv(str, str3), str2);
    }

    static zzcbf<Integer> zzm(String str, int i, int i2) {
        return new zzcbf(str, zzbbo.zza(str, Integer.valueOf(i2)), Integer.valueOf(i));
    }

    public final V get() {
        return this.zzdsu;
    }

    public final V get(V v) {
        return v != null ? v : this.zzdsu;
    }

    public final String getKey() {
        return this.zzbfl;
    }
}
