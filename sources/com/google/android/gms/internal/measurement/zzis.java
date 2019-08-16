package com.google.android.gms.internal.measurement;

public final class zzis implements Cloneable {
    private static final zzir zzaor = new zzir();
    private int mSize;
    private boolean zzaos;
    private int[] zzaot;
    private zzir[] zzaou;

    zzis() {
        this(10);
    }

    private zzis(int i) {
        this.zzaos = false;
        int idealIntArraySize = idealIntArraySize(i);
        this.zzaot = new int[idealIntArraySize];
        this.zzaou = new zzir[idealIntArraySize];
        this.mSize = 0;
    }

    private static int idealIntArraySize(int i) {
        int i2 = i << 2;
        int i3 = 4;
        while (true) {
            if (i3 >= 32) {
                break;
            } else if (i2 <= (1 << i3) - 12) {
                i2 = (1 << i3) - 12;
                break;
            } else {
                i3++;
            }
        }
        return i2 / 4;
    }

    private final int zzcn(int i) {
        int i2 = 0;
        int i3 = this.mSize - 1;
        while (i2 <= i3) {
            int i4 = (i2 + i3) >>> 1;
            int i5 = this.zzaot[i4];
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
        zzis zzis = new zzis(i);
        System.arraycopy(this.zzaot, 0, zzis.zzaot, 0, i);
        for (int i2 = 0; i2 < i; i2++) {
            if (this.zzaou[i2] != null) {
                zzis.zzaou[i2] = (zzir) this.zzaou[i2].clone();
            }
        }
        zzis.mSize = i;
        return zzis;
    }

    public final boolean equals(Object obj) {
        boolean z;
        boolean z2;
        if (obj != this) {
            if (!(obj instanceof zzis)) {
                return false;
            }
            zzis zzis = (zzis) obj;
            if (this.mSize != zzis.mSize) {
                return false;
            }
            int[] iArr = this.zzaot;
            int[] iArr2 = zzis.zzaot;
            int i = this.mSize;
            int i2 = 0;
            while (true) {
                if (i2 >= i) {
                    z = true;
                    break;
                } else if (iArr[i2] != iArr2[i2]) {
                    z = false;
                    break;
                } else {
                    i2++;
                }
            }
            if (!z) {
                return false;
            }
            zzir[] zzirArr = this.zzaou;
            zzir[] zzirArr2 = zzis.zzaou;
            int i3 = this.mSize;
            int i4 = 0;
            while (true) {
                if (i4 >= i3) {
                    z2 = true;
                    break;
                } else if (!zzirArr[i4].equals(zzirArr2[i4])) {
                    z2 = false;
                    break;
                } else {
                    i4++;
                }
            }
            if (!z2) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        int i = 17;
        for (int i2 = 0; i2 < this.mSize; i2++) {
            i = (((i * 31) + this.zzaot[i2]) * 31) + this.zzaou[i2].hashCode();
        }
        return i;
    }

    public final boolean isEmpty() {
        return this.mSize == 0;
    }

    /* access modifiers changed from: 0000 */
    public final int size() {
        return this.mSize;
    }

    /* access modifiers changed from: 0000 */
    public final void zza(int i, zzir zzir) {
        int zzcn = zzcn(i);
        if (zzcn >= 0) {
            this.zzaou[zzcn] = zzir;
            return;
        }
        int i2 = zzcn ^ -1;
        if (i2 >= this.mSize || this.zzaou[i2] != zzaor) {
            if (this.mSize >= this.zzaot.length) {
                int idealIntArraySize = idealIntArraySize(this.mSize + 1);
                int[] iArr = new int[idealIntArraySize];
                zzir[] zzirArr = new zzir[idealIntArraySize];
                System.arraycopy(this.zzaot, 0, iArr, 0, this.zzaot.length);
                System.arraycopy(this.zzaou, 0, zzirArr, 0, this.zzaou.length);
                this.zzaot = iArr;
                this.zzaou = zzirArr;
            }
            if (this.mSize - i2 != 0) {
                System.arraycopy(this.zzaot, i2, this.zzaot, i2 + 1, this.mSize - i2);
                System.arraycopy(this.zzaou, i2, this.zzaou, i2 + 1, this.mSize - i2);
            }
            this.zzaot[i2] = i;
            this.zzaou[i2] = zzir;
            this.mSize++;
            return;
        }
        this.zzaot[i2] = i;
        this.zzaou[i2] = zzir;
    }

    /* access modifiers changed from: 0000 */
    public final zzir zzcl(int i) {
        int zzcn = zzcn(i);
        if (zzcn < 0 || this.zzaou[zzcn] == zzaor) {
            return null;
        }
        return this.zzaou[zzcn];
    }

    /* access modifiers changed from: 0000 */
    public final zzir zzcm(int i) {
        return this.zzaou[i];
    }
}
