package com.google.android.gms.internal;

import java.util.concurrent.Callable;

final class zzcdf implements Callable<byte[]> {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ zzcbc zziuf;

    zzcdf(zzcct zzcct, zzcbc zzcbc, String str) {
        this.zziub = zzcct;
        this.zziuf = zzcbc;
        this.zziab = str;
    }

    public final /* synthetic */ Object call() throws Exception {
        this.zziub.zzikb.zzazj();
        return this.zziub.zzikb.zza(this.zziuf, this.zziab);
    }
}
