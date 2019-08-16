package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzey.zze;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public class zzel {
    private static volatile boolean zzaer = false;
    private static final Class<?> zzaes = zzto();
    private static volatile zzel zzaet;
    private static volatile zzel zzaeu;
    static final zzel zzaev = new zzel(true);
    private final Map<zza, zze<?, ?>> zzaew;

    static final class zza {
        private final int number;
        private final Object object;

        zza(Object obj, int i) {
            this.object = obj;
            this.number = i;
        }

        public final boolean equals(Object obj) {
            if (!(obj instanceof zza)) {
                return false;
            }
            zza zza = (zza) obj;
            return this.object == zza.object && this.number == zza.number;
        }

        public final int hashCode() {
            return (System.identityHashCode(this.object) * 65535) + this.number;
        }
    }

    zzel() {
        this.zzaew = new HashMap();
    }

    private zzel(boolean z) {
        this.zzaew = Collections.emptyMap();
    }

    static zzel zztn() {
        return zzex.zzc(zzel.class);
    }

    private static Class<?> zzto() {
        try {
            return Class.forName("com.google.protobuf.Extension");
        } catch (ClassNotFoundException e) {
            return null;
        }
    }

    public static zzel zztp() {
        zzel zzel = zzaet;
        if (zzel == null) {
            synchronized (zzel.class) {
                try {
                    zzel = zzaet;
                    if (zzel == null) {
                        zzel = zzej.zztm();
                        zzaet = zzel;
                    }
                } finally {
                    Class<zzel> cls = zzel.class;
                }
            }
        }
        return zzel;
    }

    public static zzel zztq() {
        zzel zzel = zzaeu;
        if (zzel == null) {
            synchronized (zzel.class) {
                try {
                    zzel = zzaeu;
                    if (zzel == null) {
                        zzel = zzej.zztn();
                        zzaeu = zzel;
                    }
                } finally {
                    Class<zzel> cls = zzel.class;
                }
            }
        }
        return zzel;
    }

    public final <ContainingType extends zzgi> zze<ContainingType, ?> zza(ContainingType containingtype, int i) {
        return (zze) this.zzaew.get(new zza(containingtype, i));
    }
}
