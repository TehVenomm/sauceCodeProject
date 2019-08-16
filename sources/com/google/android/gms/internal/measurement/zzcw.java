package com.google.android.gms.internal.measurement;

import java.io.Serializable;

public abstract class zzcw<T> implements Serializable {
    zzcw() {
    }

    public static <T> zzcw<T> zzf(T t) {
        return new zzcy(zzcz.checkNotNull(t));
    }

    public static <T> zzcw<T> zzrp() {
        return zzcu.zzabp;
    }

    public abstract T get();

    public abstract boolean isPresent();
}
