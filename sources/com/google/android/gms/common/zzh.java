package com.google.android.gms.common;

import java.util.Arrays;

final class zzh extends zzg {
    private final byte[] zzffj;

    zzh(byte[] bArr) {
        super(Arrays.copyOfRange(bArr, 0, 25));
        this.zzffj = bArr;
    }

    final byte[] getBytes() {
        return this.zzffj;
    }
}
