package com.google.android.gms.common.api.internal;

final class zzbq implements zzl {
    private /* synthetic */ zzbp zzfno;

    zzbq(zzbp zzbp) {
        this.zzfno = zzbp;
    }

    public final void zzbe(boolean z) {
        this.zzfno.mHandler.sendMessage(this.zzfno.mHandler.obtainMessage(1, Boolean.valueOf(z)));
    }
}
