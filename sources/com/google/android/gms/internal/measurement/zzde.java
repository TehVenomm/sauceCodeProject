package com.google.android.gms.internal.measurement;

import java.io.Serializable;
import java.util.Arrays;
import org.checkerframework.checker.nullness.compatqual.NullableDecl;

final class zzde<T> implements zzdb<T>, Serializable {
    @NullableDecl
    private final T zzaby;

    zzde(@NullableDecl T t) {
        this.zzaby = t;
    }

    public final boolean equals(@NullableDecl Object obj) {
        if (!(obj instanceof zzde)) {
            return false;
        }
        zzde zzde = (zzde) obj;
        T t = this.zzaby;
        T t2 = zzde.zzaby;
        return t == t2 || (t != null && t.equals(t2));
    }

    public final T get() {
        return this.zzaby;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzaby});
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zzaby);
        return new StringBuilder(String.valueOf(valueOf).length() + 22).append("Suppliers.ofInstance(").append(valueOf).append(")").toString();
    }
}
