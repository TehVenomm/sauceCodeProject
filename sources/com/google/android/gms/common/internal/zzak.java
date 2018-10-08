package com.google.android.gms.common.internal;

import android.util.Log;

public final class zzak {
    private static int zzfvc = 15;
    private static final String zzfvd = null;
    private final String zzfve;
    private final String zzfvf;

    public zzak(String str) {
        this(str, null);
    }

    private zzak(String str, String str2) {
        zzbp.zzb((Object) str, (Object) "log tag cannot be null");
        zzbp.zzb(str.length() <= 23, "tag \"%s\" is longer than the %d character maximum", str, Integer.valueOf(23));
        this.zzfve = str;
        this.zzfvf = null;
    }

    private final boolean zzce(int i) {
        return Log.isLoggable(this.zzfve, i);
    }

    private final String zzge(String str) {
        return this.zzfvf == null ? str : this.zzfvf.concat(str);
    }

    public final void zzb(String str, String str2, Throwable th) {
        if (zzce(4)) {
            Log.i(str, zzge(str2), th);
        }
    }

    public final void zzc(String str, String str2, Throwable th) {
        if (zzce(5)) {
            Log.w(str, zzge(str2), th);
        }
    }

    public final void zzd(String str, String str2, Throwable th) {
        if (zzce(6)) {
            Log.e(str, zzge(str2), th);
        }
    }

    public final void zze(String str, String str2, Throwable th) {
        if (zzce(7)) {
            Log.e(str, zzge(str2), th);
            Log.wtf(str, zzge(str2), th);
        }
    }

    public final void zzx(String str, String str2) {
        if (zzce(3)) {
            Log.d(str, zzge(str2));
        }
    }

    public final void zzy(String str, String str2) {
        if (zzce(5)) {
            Log.w(str, zzge(str2));
        }
    }

    public final void zzz(String str, String str2) {
        if (zzce(6)) {
            Log.e(str, zzge(str2));
        }
    }
}
