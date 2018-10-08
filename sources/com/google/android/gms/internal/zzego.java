package com.google.android.gms.internal;

import java.io.IOException;

public abstract class zzego {
    protected volatile int zzndd = -1;

    public static final <T extends zzego> T zza(T t, byte[] bArr) throws zzegn {
        return zza(t, bArr, 0, bArr.length);
    }

    private static <T extends zzego> T zza(T t, byte[] bArr, int i, int i2) throws zzegn {
        try {
            zzegf zzh = zzegf.zzh(bArr, 0, i2);
            t.zza(zzh);
            zzh.zzgk(0);
            return t;
        } catch (zzegn e) {
            throw e;
        } catch (Throwable e2) {
            throw new RuntimeException("Reading from a byte array threw an IOException (should never happen).", e2);
        }
    }

    public static final byte[] zzc(zzego zzego) {
        byte[] bArr = new byte[zzego.zzbjo()];
        try {
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzego.zza(zzi);
            zzi.zzccd();
            return bArr;
        } catch (Throwable e) {
            throw new RuntimeException("Serializing to a byte array threw an IOException (should never happen).", e);
        }
    }

    public /* synthetic */ Object clone() throws CloneNotSupportedException {
        return zzcdz();
    }

    public String toString() {
        return zzegp.zzd(this);
    }

    public abstract zzego zza(zzegf zzegf) throws IOException;

    public void zza(zzegg zzegg) throws IOException {
    }

    public final int zzbjo() {
        int zzn = zzn();
        this.zzndd = zzn;
        return zzn;
    }

    public zzego zzcdz() throws CloneNotSupportedException {
        return (zzego) super.clone();
    }

    public final int zzcef() {
        if (this.zzndd < 0) {
            zzbjo();
        }
        return this.zzndd;
    }

    protected int zzn() {
        return 0;
    }
}
