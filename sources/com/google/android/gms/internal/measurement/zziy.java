package com.google.android.gms.internal.measurement;

import java.util.Arrays;

final class zziy {
    final int tag;
    final byte[] zzado;

    zziy(int i, byte[] bArr) {
        this.tag = i;
        this.zzado = bArr;
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof zziy)) {
                return false;
            }
            zziy zziy = (zziy) obj;
            if (this.tag != zziy.tag || !Arrays.equals(this.zzado, zziy.zzado)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return ((this.tag + 527) * 31) + Arrays.hashCode(this.zzado);
    }
}
