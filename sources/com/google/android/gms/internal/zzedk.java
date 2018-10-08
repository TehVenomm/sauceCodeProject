package com.google.android.gms.internal;

import java.io.IOException;
import java.io.Serializable;
import java.util.Iterator;

public abstract class zzedk implements Serializable, Iterable<Byte> {
    public static final zzedk zzmxr = new zzedr(zzeen.EMPTY_BYTE_ARRAY);
    private static final zzedo zzmxs;
    private int zzmxt = 0;

    static {
        Object obj = 1;
        try {
            Class.forName("android.content.Context");
        } catch (ClassNotFoundException e) {
            obj = null;
        }
        zzmxs = obj != null ? new zzeds() : new zzedm();
    }

    zzedk() {
    }

    public static zzedk zzaq(byte[] bArr) {
        return zzc(bArr, 0, bArr.length);
    }

    static zzedk zzar(byte[] bArr) {
        return new zzedr(bArr);
    }

    public static zzedk zzc(byte[] bArr, int i, int i2) {
        return new zzedr(zzmxs.zzd(bArr, i, i2));
    }

    static int zzg(int i, int i2, int i3) {
        int i4 = i2 - i;
        if ((((i | i2) | i4) | (i3 - i2)) >= 0) {
            return i4;
        }
        if (i < 0) {
            throw new IndexOutOfBoundsException("Beginning index: " + i + " < 0");
        } else if (i2 < i) {
            throw new IndexOutOfBoundsException("Beginning index larger than ending index: " + i + ", " + i2);
        } else {
            throw new IndexOutOfBoundsException("End index: " + i2 + " >= " + i3);
        }
    }

    static zzedp zzgj(int i) {
        return new zzedp(i);
    }

    public static zzedk zzra(String str) {
        return new zzedr(str.getBytes(zzeen.UTF_8));
    }

    public abstract boolean equals(Object obj);

    public final int hashCode() {
        int i = this.zzmxt;
        if (i == 0) {
            i = size();
            i = zzf(i, 0, i);
            if (i == 0) {
                i = 1;
            }
            this.zzmxt = i;
        }
        return i;
    }

    public final boolean isEmpty() {
        return size() == 0;
    }

    public /* synthetic */ Iterator iterator() {
        return new zzedl(this);
    }

    public abstract int size();

    public final byte[] toByteArray() {
        int size = size();
        if (size == 0) {
            return zzeen.EMPTY_BYTE_ARRAY;
        }
        byte[] bArr = new byte[size];
        zza(bArr, 0, 0, size);
        return bArr;
    }

    public final String toString() {
        return String.format("<ByteString@%s size=%d>", new Object[]{Integer.toHexString(System.identityHashCode(this)), Integer.valueOf(size())});
    }

    abstract void zza(zzedj zzedj) throws IOException;

    protected abstract void zza(byte[] bArr, int i, int i2, int i3);

    public abstract zzedt zzcbm();

    protected final int zzcbn() {
        return this.zzmxt;
    }

    protected abstract int zzf(int i, int i2, int i3);

    public abstract byte zzgi(int i);

    public abstract zzedk zzs(int i, int i2);
}
