package com.google.android.gms.internal;

import java.util.concurrent.Callable;

final class zzccq implements Callable<String> {
    private /* synthetic */ String zziab;
    private /* synthetic */ zzcco zzitu;

    zzccq(zzcco zzcco, String str) {
        this.zzitu = zzcco;
        this.zziab = str;
    }

    public final /* synthetic */ Object call() throws Exception {
        zzcaj zziw = this.zzitu.zzaue().zziw(this.zziab);
        if (zziw != null) {
            return zziw.getAppInstanceId();
        }
        this.zzitu.zzauk().zzaye().log("App info was null when attempting to get app instance id");
        return null;
    }
}
