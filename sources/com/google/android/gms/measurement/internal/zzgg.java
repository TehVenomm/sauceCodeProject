package com.google.android.gms.measurement.internal;

import android.os.Bundle;
import android.support.annotation.NonNull;

public final class zzgg {
    public static <T> T zza(@NonNull Bundle bundle, String str, Class<T> cls, T t) {
        Object obj = bundle.get(str);
        if (obj == null) {
            return t;
        }
        if (cls.isAssignableFrom(obj.getClass())) {
            return obj;
        }
        throw new IllegalStateException(String.format("Invalid conditional user property field type. '%s' expected [%s] but was [%s]", new Object[]{str, cls.getCanonicalName(), obj.getClass().getCanonicalName()}));
    }

    public static void zza(@NonNull Bundle bundle, @NonNull Object obj) {
        if (obj instanceof Double) {
            bundle.putDouble("value", ((Double) obj).doubleValue());
        } else if (obj instanceof Long) {
            bundle.putLong("value", ((Long) obj).longValue());
        } else {
            bundle.putString("value", obj.toString());
        }
    }
}
