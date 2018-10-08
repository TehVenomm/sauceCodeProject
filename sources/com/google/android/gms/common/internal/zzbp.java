package com.google.android.gms.common.internal;

import android.os.Handler;
import android.os.Looper;
import android.text.TextUtils;
import com.google.android.gms.common.util.zzu;

public final class zzbp {
    public static long zza(long j, Object obj) {
        if (j != 0) {
            return j;
        }
        throw new IllegalArgumentException(String.valueOf(obj));
    }

    public static void zza(Handler handler) {
        if (Looper.myLooper() != handler.getLooper()) {
            throw new IllegalStateException("Must be called on the handler thread");
        }
    }

    public static void zza(boolean z, Object obj) {
        if (!z) {
            throw new IllegalStateException(String.valueOf(obj));
        }
    }

    public static void zza(boolean z, String str, Object... objArr) {
        if (!z) {
            throw new IllegalStateException(String.format(str, objArr));
        }
    }

    public static <T> T zzb(T t, Object obj) {
        if (t != null) {
            return t;
        }
        throw new NullPointerException(String.valueOf(obj));
    }

    public static void zzb(boolean z, Object obj) {
        if (!z) {
            throw new IllegalArgumentException(String.valueOf(obj));
        }
    }

    public static void zzb(boolean z, String str, Object... objArr) {
        if (!z) {
            throw new IllegalArgumentException(String.format(str, objArr));
        }
    }

    public static void zzbg(boolean z) {
        if (!z) {
            throw new IllegalStateException();
        }
    }

    public static void zzbh(boolean z) {
        if (!z) {
            throw new IllegalArgumentException();
        }
    }

    public static void zzfx(String str) {
        if (!zzu.zzaq()) {
            throw new IllegalStateException(str);
        }
    }

    public static String zzgf(String str) {
        if (!TextUtils.isEmpty(str)) {
            return str;
        }
        throw new IllegalArgumentException("Given String is empty or null");
    }

    public static void zzgg(String str) {
        if (zzu.zzaq()) {
            throw new IllegalStateException(str);
        }
    }

    public static String zzh(String str, Object obj) {
        if (!TextUtils.isEmpty(str)) {
            return str;
        }
        throw new IllegalArgumentException(String.valueOf(obj));
    }

    public static <T> T zzu(T t) {
        if (t != null) {
            return t;
        }
        throw new NullPointerException("null reference");
    }
}
