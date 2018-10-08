package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;
import java.util.concurrent.Executors;
import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;

public final class zzbdl implements ThreadFactory {
    private final int mPriority;
    private final String zzfzh;
    private final AtomicInteger zzfzi;
    private final ThreadFactory zzfzj;

    public zzbdl(String str) {
        this(str, 0);
    }

    private zzbdl(String str, int i) {
        this.zzfzi = new AtomicInteger();
        this.zzfzj = Executors.defaultThreadFactory();
        this.zzfzh = (String) zzbp.zzb((Object) str, (Object) "Name must not be null");
        this.mPriority = 0;
    }

    public final Thread newThread(Runnable runnable) {
        Thread newThread = this.zzfzj.newThread(new zzbdm(runnable, 0));
        String str = this.zzfzh;
        newThread.setName(new StringBuilder(String.valueOf(str).length() + 13).append(str).append("[").append(this.zzfzi.getAndIncrement()).append("]").toString());
        return newThread;
    }
}
