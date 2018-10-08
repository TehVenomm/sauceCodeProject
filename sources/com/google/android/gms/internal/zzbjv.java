package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.common.internal.zzak;

public final class zzbjv {
    private static final zzak zzgib = new zzak("GmsDrive");

    public static void zza(String str, Throwable th, String str2) {
        zzgib.zzd(str, str2, th);
    }

    public static void zzm(Context context, String str, String str2) {
        zzgib.zze(str, str2, new Throwable());
    }

    public static void zzx(String str, String str2) {
        zzgib.zzx(str, str2);
    }

    public static void zzy(String str, String str2) {
        zzgib.zzy(str, str2);
    }

    public static void zzz(String str, String str2) {
        zzgib.zzz(str, str2);
    }
}
