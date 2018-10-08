package com.google.android.gms.internal;

import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;

public abstract class zzedt {
    int zzmxy;
    int zzmxz;
    private int zzmya;

    private zzedt() {
        this.zzmxz = 100;
        this.zzmya = Strategy.TTL_SECONDS_INFINITE;
    }

    public static zzedt zzas(byte[] bArr) {
        return zzb(bArr, 0, bArr.length, false);
    }

    static zzedt zzb(byte[] bArr, int i, int i2, boolean z) {
        zzedt zzedv = new zzedv(bArr, i, i2, z, null);
        try {
            zzedv.zzgm(i2);
            return zzedv;
        } catch (Throwable e) {
            throw new IllegalArgumentException(e);
        }
    }

    public abstract <T extends zzeed<T, ?>> T zza(T t, zzedz zzedz) throws IOException;

    public abstract int zzcbr() throws IOException;

    public abstract int zzcbs() throws IOException;

    public abstract String zzcbt() throws IOException;

    public abstract zzedk zzcbu() throws IOException;

    public abstract int zzcbv() throws IOException;

    abstract long zzcbw() throws IOException;

    public abstract boolean zzcbx() throws IOException;

    public abstract int zzcby();

    public abstract void zzgk(int i) throws zzeer;

    public abstract boolean zzgl(int i) throws IOException;

    public abstract int zzgm(int i) throws zzeer;

    public abstract void zzgn(int i);

    public abstract void zzgo(int i) throws IOException;
}
