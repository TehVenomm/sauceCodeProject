package com.google.android.gms.internal;

import java.util.concurrent.Callable;
import java.util.concurrent.TimeoutException;

final class zzcdz implements Callable<String> {
    private /* synthetic */ zzcdo zziup;

    zzcdz(zzcdo zzcdo) {
        this.zziup = zzcdo;
    }

    public final /* synthetic */ Object call() throws Exception {
        Object zzaym = this.zziup.zzaul().zzaym();
        if (zzaym == null) {
            zzcdl zzaty = this.zziup.zzaty();
            if (zzaty.zzauj().zzayr()) {
                zzaty.zzauk().zzayc().log("Cannot retrieve app instance id from analytics worker thread");
                zzaym = null;
            } else {
                zzaty.zzauj();
                if (zzccj.zzaq()) {
                    zzaty.zzauk().zzayc().log("Cannot retrieve app instance id from main thread");
                    zzaym = null;
                } else {
                    long elapsedRealtime = zzaty.zzvu().elapsedRealtime();
                    zzaym = zzaty.zzbc(120000);
                    elapsedRealtime = zzaty.zzvu().elapsedRealtime() - elapsedRealtime;
                    if (zzaym == null && elapsedRealtime < 120000) {
                        zzaym = zzaty.zzbc(120000 - elapsedRealtime);
                    }
                }
            }
            if (zzaym == null) {
                throw new TimeoutException();
            }
            this.zziup.zzaul().zzjk(zzaym);
        }
        return zzaym;
    }
}
