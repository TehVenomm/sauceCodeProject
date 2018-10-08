package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;
import java.lang.Thread.UncaughtExceptionHandler;

final class zzccl implements UncaughtExceptionHandler {
    private final String zzisc;
    private /* synthetic */ zzccj zzisd;

    public zzccl(zzccj zzccj, String str) {
        this.zzisd = zzccj;
        zzbp.zzu(str);
        this.zzisc = str;
    }

    public final void uncaughtException(Thread thread, Throwable th) {
        synchronized (this) {
            this.zzisd.zzauk().zzayc().zzj(this.zzisc, th);
        }
    }
}
