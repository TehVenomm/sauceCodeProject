package com.google.android.gms.internal.measurement;

final class zzej {
    private static final Class<?> zzaeq = zztl();

    private static final zzel zzdu(String str) throws Exception {
        return (zzel) zzaeq.getDeclaredMethod(str, new Class[0]).invoke(null, new Object[0]);
    }

    private static Class<?> zztl() {
        try {
            return Class.forName("com.google.protobuf.ExtensionRegistry");
        } catch (ClassNotFoundException e) {
            return null;
        }
    }

    public static zzel zztm() {
        if (zzaeq != null) {
            try {
                return zzdu("getEmptyRegistry");
            } catch (Exception e) {
            }
        }
        return zzel.zzaev;
    }

    static zzel zztn() {
        zzel zzel = null;
        if (zzaeq != null) {
            try {
                zzel = zzdu("loadGeneratedRegistry");
            } catch (Exception e) {
            }
        }
        if (zzel == null) {
            zzel = zzel.zztn();
        }
        return zzel == null ? zztm() : zzel;
    }
}
