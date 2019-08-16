package com.google.android.gms.internal.measurement;

import org.checkerframework.checker.nullness.compatqual.NullableDecl;

final class zzdc<T> implements zzdb<T> {
    @NullableDecl
    private T value;
    private volatile zzdb<T> zzabs;
    private volatile boolean zzdh;

    zzdc(zzdb<T> zzdb) {
        this.zzabs = (zzdb) zzcz.checkNotNull(zzdb);
    }

    public final T get() {
        if (!this.zzdh) {
            synchronized (this) {
                if (!this.zzdh) {
                    T t = this.zzabs.get();
                    this.value = t;
                    this.zzdh = true;
                    this.zzabs = null;
                    return t;
                }
            }
        }
        return this.value;
    }

    public final String toString() {
        Object obj = this.zzabs;
        if (obj == null) {
            String valueOf = String.valueOf(this.value);
            obj = new StringBuilder(String.valueOf(valueOf).length() + 25).append("<supplier that returned ").append(valueOf).append(">").toString();
        }
        String valueOf2 = String.valueOf(obj);
        return new StringBuilder(String.valueOf(valueOf2).length() + 19).append("Suppliers.memoize(").append(valueOf2).append(")").toString();
    }
}
