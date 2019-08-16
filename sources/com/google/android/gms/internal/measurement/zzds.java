package com.google.android.gms.internal.measurement;

final class zzds extends zzdz {
    private final int zzadl;
    private final int zzadm;

    zzds(byte[] bArr, int i, int i2) {
        super(bArr);
        zzb(i, i + i2, bArr.length);
        this.zzadl = i;
        this.zzadm = i2;
    }

    public final int size() {
        return this.zzadm;
    }

    public final byte zzaq(int i) {
        int size = size();
        if (((size - (i + 1)) | i) >= 0) {
            return this.zzado[this.zzadl + i];
        }
        if (i < 0) {
            throw new ArrayIndexOutOfBoundsException("Index < 0: " + i);
        }
        throw new ArrayIndexOutOfBoundsException("Index > length: " + i + ", " + size);
    }

    /* access modifiers changed from: 0000 */
    public final byte zzar(int i) {
        return this.zzado[this.zzadl + i];
    }

    /* access modifiers changed from: protected */
    public final int zzsd() {
        return this.zzadl;
    }
}
