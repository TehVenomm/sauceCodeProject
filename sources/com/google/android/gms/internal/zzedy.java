package com.google.android.gms.internal;

final class zzedy {
    private static Class<?> zzmyh = zzccf();

    private static Class<?> zzccf() {
        try {
            return Class.forName("com.google.protobuf.ExtensionRegistry");
        } catch (ClassNotFoundException e) {
            return null;
        }
    }

    public static zzedz zzccg() {
        if (zzmyh != null) {
            try {
                return (zzedz) zzmyh.getMethod("getEmptyRegistry", new Class[0]).invoke(null, new Object[0]);
            } catch (Exception e) {
            }
        }
        return zzedz.zzmyk;
    }
}
