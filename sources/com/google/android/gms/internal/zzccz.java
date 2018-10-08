package com.google.android.gms.internal;

import java.util.List;
import java.util.concurrent.Callable;

final class zzccz implements Callable<List<zzcfn>> {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;
    private /* synthetic */ String zziud;
    private /* synthetic */ String zziue;

    zzccz(zzcct zzcct, zzcak zzcak, String str, String str2) {
        this.zziub = zzcct;
        this.zziua = zzcak;
        this.zziud = str;
        this.zziue = str2;
    }

    public final /* synthetic */ Object call() throws Exception {
        this.zziub.zzikb.zzazj();
        return this.zziub.zzikb.zzaue().zzg(this.zziua.packageName, this.zziud, this.zziue);
    }
}
