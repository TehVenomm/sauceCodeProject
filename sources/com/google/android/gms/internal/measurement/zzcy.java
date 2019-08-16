package com.google.android.gms.internal.measurement;

import org.checkerframework.checker.nullness.compatqual.NullableDecl;

final class zzcy<T> extends zzcw<T> {
    private final T zzabr;

    zzcy(T t) {
        this.zzabr = t;
    }

    public final boolean equals(@NullableDecl Object obj) {
        if (!(obj instanceof zzcy)) {
            return false;
        }
        return this.zzabr.equals(((zzcy) obj).zzabr);
    }

    public final T get() {
        return this.zzabr;
    }

    public final int hashCode() {
        return 1502476572 + this.zzabr.hashCode();
    }

    public final boolean isPresent() {
        return true;
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zzabr);
        return new StringBuilder(String.valueOf(valueOf).length() + 13).append("Optional.of(").append(valueOf).append(")").toString();
    }
}
