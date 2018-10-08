package com.google.android.gms.internal;

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public class zzedz {
    private static volatile boolean zzmyi = false;
    private static final Class<?> zzmyj = zzcch();
    static final zzedz zzmyk = new zzedz(true);
    private final Map<Object, Object> zzmyl;

    zzedz() {
        this.zzmyl = new HashMap();
    }

    private zzedz(boolean z) {
        this.zzmyl = Collections.emptyMap();
    }

    private static Class<?> zzcch() {
        try {
            return Class.forName("com.google.protobuf.Extension");
        } catch (ClassNotFoundException e) {
            return null;
        }
    }

    public static zzedz zzcci() {
        return zzedy.zzccg();
    }
}
