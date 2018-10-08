package com.google.android.gms.internal;

import java.util.Arrays;

final class zzegq {
    final int tag;
    final byte[] zzjao;

    zzegq(int i, byte[] bArr) {
        this.tag = i;
        this.zzjao = bArr;
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof zzegq)) {
                return false;
            }
            zzegq zzegq = (zzegq) obj;
            if (this.tag != zzegq.tag) {
                return false;
            }
            if (!Arrays.equals(this.zzjao, zzegq.zzjao)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return ((this.tag + 527) * 31) + Arrays.hashCode(this.zzjao);
    }
}
