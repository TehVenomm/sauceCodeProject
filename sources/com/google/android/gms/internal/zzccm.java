package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;

final class zzccm<V> extends FutureTask<V> implements Comparable<zzccm> {
    private final String zzisc;
    private /* synthetic */ zzccj zzisd;
    private final long zzise = zzccj.zzisb.getAndIncrement();
    private final boolean zzisf;

    zzccm(zzccj zzccj, Runnable runnable, boolean z, String str) {
        this.zzisd = zzccj;
        super(runnable, null);
        zzbp.zzu(str);
        this.zzisc = str;
        this.zzisf = false;
        if (this.zzise == Long.MAX_VALUE) {
            zzccj.zzauk().zzayc().log("Tasks index overflow");
        }
    }

    zzccm(zzccj zzccj, Callable<V> callable, boolean z, String str) {
        this.zzisd = zzccj;
        super(callable);
        zzbp.zzu(str);
        this.zzisc = str;
        this.zzisf = z;
        if (this.zzise == Long.MAX_VALUE) {
            zzccj.zzauk().zzayc().log("Tasks index overflow");
        }
    }

    public final /* synthetic */ int compareTo(@NonNull Object obj) {
        zzccm zzccm = (zzccm) obj;
        if (this.zzisf != zzccm.zzisf) {
            if (!this.zzisf) {
                return 1;
            }
        } else if (this.zzise >= zzccm.zzise) {
            if (this.zzise > zzccm.zzise) {
                return 1;
            }
            this.zzisd.zzauk().zzayd().zzj("Two tasks share the same index. index", Long.valueOf(this.zzise));
            return 0;
        }
        return -1;
    }

    protected final void setException(Throwable th) {
        this.zzisd.zzauk().zzayc().zzj(this.zzisc, th);
        if (th instanceof zzcck) {
            Thread.getDefaultUncaughtExceptionHandler().uncaughtException(Thread.currentThread(), th);
        }
        super.setException(th);
    }
}
