package com.google.android.gms.internal.drive;

import java.util.Arrays;

final class zziz {
    final int tag;
    final byte[] zzng;

    zziz(int i, byte[] bArr) {
        this.tag = i;
        this.zzng = bArr;
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof zziz)) {
                return false;
            }
            zziz zziz = (zziz) obj;
            if (this.tag != zziz.tag || !Arrays.equals(this.zzng, zziz.zzng)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return ((this.tag + 527) * 31) + Arrays.hashCode(this.zzng);
    }
}
