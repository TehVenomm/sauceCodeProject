package com.google.android.gms.internal;

public final class zzegk implements Cloneable {
    private static final zzegl zzncw = new zzegl();
    private int mSize;
    private boolean zzncx;
    private int[] zzncy;
    private zzegl[] zzncz;

    zzegk() {
        this(10);
    }

    private zzegk(int i) {
        this.zzncx = false;
        int idealIntArraySize = idealIntArraySize(i);
        this.zzncy = new int[idealIntArraySize];
        this.zzncz = new zzegl[idealIntArraySize];
        this.mSize = 0;
    }

    private static int idealIntArraySize(int i) {
        int i2 = i << 2;
        for (int i3 = 4; i3 < 32; i3++) {
            if (i2 <= (1 << i3) - 12) {
                i2 = (1 << i3) - 12;
                break;
            }
        }
        return i2 / 4;
    }

    private final int zzhh(int i) {
        int i2 = 0;
        int i3 = this.mSize - 1;
        while (i2 <= i3) {
            int i4 = (i2 + i3) >>> 1;
            int i5 = this.zzncy[i4];
            if (i5 < i) {
                i2 = i4 + 1;
            } else if (i5 <= i) {
                return i4;
            } else {
                i3 = i4 - 1;
            }
        }
        return i2 ^ -1;
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        int i = this.mSize;
        zzegk zzegk = new zzegk(i);
        System.arraycopy(this.zzncy, 0, zzegk.zzncy, 0, i);
        for (int i2 = 0; i2 < i; i2++) {
            if (this.zzncz[i2] != null) {
                zzegk.zzncz[i2] = (zzegl) this.zzncz[i2].clone();
            }
        }
        zzegk.mSize = i;
        return zzegk;
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof zzegk)) {
                return false;
            }
            zzegk zzegk = (zzegk) obj;
            if (this.mSize != zzegk.mSize) {
                return false;
            }
            int i;
            boolean z;
            int[] iArr = this.zzncy;
            int[] iArr2 = zzegk.zzncy;
            int i2 = this.mSize;
            for (i = 0; i < i2; i++) {
                if (iArr[i] != iArr2[i]) {
                    z = false;
                    break;
                }
            }
            z = true;
            if (!z) {
                return false;
            }
            zzegl[] zzeglArr = this.zzncz;
            zzegl[] zzeglArr2 = zzegk.zzncz;
            i2 = this.mSize;
            for (i = 0; i < i2; i++) {
                if (!zzeglArr[i].equals(zzeglArr2[i])) {
                    z = false;
                    break;
                }
            }
            z = true;
            if (!z) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        int i = 17;
        for (int i2 = 0; i2 < this.mSize; i2++) {
            i = (((i * 31) + this.zzncy[i2]) * 31) + this.zzncz[i2].hashCode();
        }
        return i;
    }

    public final boolean isEmpty() {
        return this.mSize == 0;
    }

    final int size() {
        return this.mSize;
    }

    final void zza(int i, zzegl zzegl) {
        int zzhh = zzhh(i);
        if (zzhh >= 0) {
            this.zzncz[zzhh] = zzegl;
            return;
        }
        zzhh ^= -1;
        if (zzhh >= this.mSize || this.zzncz[zzhh] != zzncw) {
            if (this.mSize >= this.zzncy.length) {
                int idealIntArraySize = idealIntArraySize(this.mSize + 1);
                Object obj = new int[idealIntArraySize];
                Object obj2 = new zzegl[idealIntArraySize];
                System.arraycopy(this.zzncy, 0, obj, 0, this.zzncy.length);
                System.arraycopy(this.zzncz, 0, obj2, 0, this.zzncz.length);
                this.zzncy = obj;
                this.zzncz = obj2;
            }
            if (this.mSize - zzhh != 0) {
                System.arraycopy(this.zzncy, zzhh, this.zzncy, zzhh + 1, this.mSize - zzhh);
                System.arraycopy(this.zzncz, zzhh, this.zzncz, zzhh + 1, this.mSize - zzhh);
            }
            this.zzncy[zzhh] = i;
            this.zzncz[zzhh] = zzegl;
            this.mSize++;
            return;
        }
        this.zzncy[zzhh] = i;
        this.zzncz[zzhh] = zzegl;
    }

    final zzegl zzhf(int i) {
        int zzhh = zzhh(i);
        return (zzhh < 0 || this.zzncz[zzhh] == zzncw) ? null : this.zzncz[zzhh];
    }

    final zzegl zzhg(int i) {
        return this.zzncz[i];
    }
}
