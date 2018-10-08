package com.google.android.gms.internal;

import java.util.List;
import java.util.concurrent.Callable;

final class zzcdi implements Callable<List<zzcfn>> {
    private /* synthetic */ zzcak zziua;
    private /* synthetic */ zzcct zziub;

    zzcdi(zzcct zzcct, zzcak zzcak) {
        this.zziub = zzcct;
        this.zziua = zzcak;
    }

    public final /* synthetic */ Object call() throws Exception {
        this.zziub.zzikb.zzazj();
        return this.zziub.zzikb.zzaue().zziv(this.zziua.packageName);
    }
}
