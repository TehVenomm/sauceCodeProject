package com.google.android.gms.internal;

final class zzedn extends zzedr {
    private final int zzmxv;
    private final int zzmxw;

    zzedn(byte[] bArr, int i, int i2) {
        super(bArr);
        zzedk.zzg(i, i + i2, bArr.length);
        this.zzmxv = i;
        this.zzmxw = i2;
    }

    public final int size() {
        return this.zzmxw;
    }

    protected final void zza(byte[] bArr, int i, int i2, int i3) {
        System.arraycopy(this.zzjao, zzcbo(), bArr, 0, i3);
    }

    protected final int zzcbo() {
        return this.zzmxv;
    }

    public final byte zzgi(int i) {
        int size = size();
        if (((size - (i + 1)) | i) >= 0) {
            return this.zzjao[this.zzmxv + i];
        }
        if (i < 0) {
            throw new ArrayIndexOutOfBoundsException("Index < 0: " + i);
        }
        throw new ArrayIndexOutOfBoundsException("Index > length: " + i + ", " + size);
    }
}
