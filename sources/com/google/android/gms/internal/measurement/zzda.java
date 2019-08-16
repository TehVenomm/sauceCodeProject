package com.google.android.gms.internal.measurement;

import java.io.Serializable;
import org.checkerframework.checker.nullness.compatqual.NullableDecl;

public final class zzda {
    public static <T> zzdb<T> zza(zzdb<T> zzdb) {
        return ((zzdb instanceof zzdc) || (zzdb instanceof zzdd)) ? zzdb : zzdb instanceof Serializable ? new zzdd(zzdb) : new zzdc(zzdb);
    }

    public static <T> zzdb<T> zzg(@NullableDecl T t) {
        return new zzde(t);
    }
}
