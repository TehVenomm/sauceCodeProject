package com.google.android.gms.common;

import java.lang.ref.WeakReference;

abstract class zzi extends zzg {
    private static final WeakReference<byte[]> zzffl = new WeakReference(null);
    private WeakReference<byte[]> zzffk = zzffl;

    zzi(byte[] bArr) {
        super(bArr);
    }

    final byte[] getBytes() {
        byte[] bArr;
        synchronized (this) {
            bArr = (byte[]) this.zzffk.get();
            if (bArr == null) {
                bArr = zzafa();
                this.zzffk = new WeakReference(bArr);
            }
        }
        return bArr;
    }

    protected abstract byte[] zzafa();
}
