package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.internal.zzbp;
import java.util.List;
import java.util.Map;

@WorkerThread
final class zzcbv implements Runnable {
    private final String mPackageName;
    private final int zzbyx;
    private final Throwable zzdfd;
    private final zzcbu zzipw;
    private final byte[] zzipx;
    private final Map<String, List<String>> zzipy;

    private zzcbv(String str, zzcbu zzcbu, int i, Throwable th, byte[] bArr, Map<String, List<String>> map) {
        zzbp.zzu(zzcbu);
        this.zzipw = zzcbu;
        this.zzbyx = i;
        this.zzdfd = th;
        this.zzipx = bArr;
        this.mPackageName = str;
        this.zzipy = map;
    }

    public final void run() {
        this.zzipw.zza(this.mPackageName, this.zzbyx, this.zzdfd, this.zzipx, this.zzipy);
    }
}
