package com.google.android.gms.common.api.internal;

import com.google.android.gms.internal.zzbdl;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public final class zzbo {
    private static final ExecutorService zzfnd = Executors.newFixedThreadPool(2, new zzbdl("GAC_Executor"));

    public static ExecutorService zzahm() {
        return zzfnd;
    }
}
