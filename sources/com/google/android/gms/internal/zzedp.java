package com.google.android.gms.internal;

final class zzedp {
    private final byte[] buffer;
    private final zzedw zzmxx;

    private zzedp(int i) {
        this.buffer = new byte[i];
        this.zzmxx = zzedw.zzat(this.buffer);
    }

    public final zzedk zzcbp() {
        this.zzmxx.zzccd();
        return new zzedr(this.buffer);
    }

    public final zzedw zzcbq() {
        return this.zzmxx;
    }
}
