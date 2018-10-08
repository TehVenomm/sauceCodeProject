package com.google.android.gms.common.api.internal;

import com.google.android.gms.internal.zzbdl;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

public final class zzct {
    private static final ExecutorService zzfnd = new ThreadPoolExecutor(0, 4, 60, TimeUnit.SECONDS, new LinkedBlockingQueue(), new zzbdl("GAC_Transform"));

    public static ExecutorService zzahm() {
        return zzfnd;
    }
}
