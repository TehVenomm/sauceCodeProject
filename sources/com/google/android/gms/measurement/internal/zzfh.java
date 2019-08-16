package com.google.android.gms.measurement.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.Preconditions;
import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;

final class zzfh<V> extends FutureTask<V> implements Comparable<zzfh> {
    private final String zzns;
    private final /* synthetic */ zzfc zznt;
    private final long zznw = zzfc.zznj.getAndIncrement();
    final boolean zznx;

    zzfh(zzfc zzfc, Runnable runnable, boolean z, String str) {
        this.zznt = zzfc;
        super(runnable, null);
        Preconditions.checkNotNull(str);
        this.zzns = str;
        this.zznx = false;
        if (this.zznw == Long.MAX_VALUE) {
            zzfc.zzab().zzgk().zzao("Tasks index overflow");
        }
    }

    zzfh(zzfc zzfc, Callable<V> callable, boolean z, String str) {
        this.zznt = zzfc;
        super(callable);
        Preconditions.checkNotNull(str);
        this.zzns = str;
        this.zznx = z;
        if (this.zznw == Long.MAX_VALUE) {
            zzfc.zzab().zzgk().zzao("Tasks index overflow");
        }
    }

    public final /* synthetic */ int compareTo(@NonNull Object obj) {
        zzfh zzfh = (zzfh) obj;
        if (this.zznx != zzfh.zznx) {
            if (!this.zznx) {
                return 1;
            }
        } else if (this.zznw >= zzfh.zznw) {
            if (this.zznw > zzfh.zznw) {
                return 1;
            }
            this.zznt.zzab().zzgl().zza("Two tasks share the same index. index", Long.valueOf(this.zznw));
            return 0;
        }
        return -1;
    }

    /* access modifiers changed from: protected */
    public final void setException(Throwable th) {
        this.zznt.zzab().zzgk().zza(this.zzns, th);
        if (th instanceof zzff) {
            Thread.getDefaultUncaughtExceptionHandler().uncaughtException(Thread.currentThread(), th);
        }
        super.setException(th);
    }
}
