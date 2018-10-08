package com.google.android.gms.internal;

import java.util.Arrays;

public final class zzefq {
    private static final zzefq zznai = new zzefq(0, new int[0], new Object[0], false);
    private int count;
    private boolean zzmxq;
    private int zzmys;
    private int[] zznaj;
    private Object[] zznak;

    private zzefq() {
        this(0, new int[8], new Object[8], true);
    }

    private zzefq(int i, int[] iArr, Object[] objArr, boolean z) {
        this.zzmys = -1;
        this.count = i;
        this.zznaj = iArr;
        this.zznak = objArr;
        this.zzmxq = z;
    }

    static zzefq zzb(zzefq zzefq, zzefq zzefq2) {
        int i = zzefq.count + zzefq2.count;
        Object copyOf = Arrays.copyOf(zzefq.zznaj, i);
        System.arraycopy(zzefq2.zznaj, 0, copyOf, zzefq.count, zzefq2.count);
        Object copyOf2 = Arrays.copyOf(zzefq.zznak, i);
        System.arraycopy(zzefq2.zznak, 0, copyOf2, zzefq.count, zzefq2.count);
        return new zzefq(i, copyOf, copyOf2, true);
    }

    public static zzefq zzcdh() {
        return zznai;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || !(obj instanceof zzefq)) {
                return false;
            }
            zzefq zzefq = (zzefq) obj;
            if (this.count != zzefq.count) {
                return false;
            }
            int i;
            boolean z;
            int[] iArr = this.zznaj;
            int[] iArr2 = zzefq.zznaj;
            int i2 = this.count;
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
            Object[] objArr = this.zznak;
            Object[] objArr2 = zzefq.zznak;
            i2 = this.count;
            for (i = 0; i < i2; i++) {
                if (!objArr[i].equals(objArr2[i])) {
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
        return ((((this.count + 527) * 31) + Arrays.hashCode(this.zznaj)) * 31) + Arrays.deepHashCode(this.zznak);
    }

    public final void zzbhq() {
        this.zzmxq = false;
    }

    final void zzd(StringBuilder stringBuilder, int i) {
        for (int i2 = 0; i2 < this.count; i2++) {
            zzefb.zzb(stringBuilder, i, String.valueOf(this.zznaj[i2] >>> 3), this.zznak[i2]);
        }
    }
}
