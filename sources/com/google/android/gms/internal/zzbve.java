package com.google.android.gms.internal;

public final class zzbve {
    private static zzbve zzhaq;
    private final zzbuz zzhar = new zzbuz();
    private final zzbva zzhas = new zzbva();

    static {
        zzbve zzbve = new zzbve();
        synchronized (zzbve.class) {
            try {
                zzhaq = zzbve;
            } catch (Throwable th) {
                Class cls = zzbve.class;
            }
        }
    }

    private zzbve() {
    }

    private static zzbve zzapd() {
        zzbve zzbve;
        synchronized (zzbve.class) {
            try {
                zzbve = zzhaq;
            } catch (Throwable th) {
                Class cls = zzbve.class;
            }
        }
        return zzbve;
    }

    public static zzbuz zzape() {
        return zzapd().zzhar;
    }

    public static zzbva zzapf() {
        return zzapd().zzhas;
    }
}
