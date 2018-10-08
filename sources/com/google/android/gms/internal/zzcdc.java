package com.google.android.gms.internal;

import java.util.List;
import java.util.concurrent.Callable;

final class zzcdc implements Callable<List<zzcan>> {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ String zziud;
    private /* synthetic */ String zziue;

    zzcdc(zzcct zzcct, String str, String str2, String str3) {
        this.zziub = zzcct;
        this.zziab = str;
        this.zziud = str2;
        this.zziue = str3;
    }

    public final /* synthetic */ Object call() throws Exception {
        this.zziub.zzikb.zzazj();
        return this.zziub.zzikb.zzaue().zzh(this.zziab, this.zziud, this.zziue);
    }
}
