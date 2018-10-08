package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.util.Log;
import com.google.android.gms.common.internal.zzak;

public final class zzbcq {
    private final String mTag;
    private final int zzdqw;
    private final String zzfvf;
    private final zzak zzfwd;

    private zzbcq(String str, String str2) {
        this.zzfvf = str2;
        this.mTag = str;
        this.zzfwd = new zzak(str);
        this.zzdqw = getLogLevel();
    }

    public zzbcq(String str, String... strArr) {
        this(str, zzb(strArr));
    }

    private final String format(String str, @Nullable Object... objArr) {
        if (objArr != null && objArr.length > 0) {
            str = String.format(str, objArr);
        }
        return this.zzfvf.concat(str);
    }

    private final int getLogLevel() {
        int i = 2;
        while (7 >= i && !Log.isLoggable(this.mTag, i)) {
            i++;
        }
        return i;
    }

    private final boolean zzad(int i) {
        return this.zzdqw <= i;
    }

    private static String zzb(String... strArr) {
        if (strArr.length == 0) {
            return "";
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append('[');
        for (String str : strArr) {
            if (stringBuilder.length() > 1) {
                stringBuilder.append(",");
            }
            stringBuilder.append(str);
        }
        stringBuilder.append(']').append(' ');
        return stringBuilder.toString();
    }

    public final void zza(String str, Throwable th, @Nullable Object... objArr) {
        Log.wtf(this.mTag, format(str, objArr), th);
    }

    public final void zza(String str, @Nullable Object... objArr) {
        if (zzad(2)) {
            Log.v(this.mTag, format(str, objArr));
        }
    }

    public final void zzb(String str, @Nullable Object... objArr) {
        if (zzad(3)) {
            Log.d(this.mTag, format(str, objArr));
        }
    }

    public final void zzc(Throwable th) {
        Log.wtf(this.mTag, th);
    }

    public final void zze(String str, @Nullable Object... objArr) {
        Log.i(this.mTag, format(str, objArr));
    }

    public final void zzf(String str, @Nullable Object... objArr) {
        Log.w(this.mTag, format(str, objArr));
    }
}
