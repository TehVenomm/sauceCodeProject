package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzey.zzc;
import com.google.android.gms.internal.measurement.zzey.zzd;

public final class zzbq {

    public static final class zza extends zzey<zza, C1832zza> implements zzgk {
        private static volatile zzgr<zza> zzuo;
        /* access modifiers changed from: private */
        public static final zza zzwa = new zza();
        private int zzue;
        private String zzvy = "";
        private String zzvz = "";

        /* renamed from: com.google.android.gms.internal.measurement.zzbq$zza$zza reason: collision with other inner class name */
        public static final class C1832zza extends com.google.android.gms.internal.measurement.zzey.zza<zza, C1832zza> implements zzgk {
            private C1832zza() {
                super(zza.zzwa);
            }

            /* synthetic */ C1832zza(zzbp zzbp) {
                this();
            }
        }

        static {
            zzey.zza(zza.class, zzwa);
        }

        private zza() {
        }

        public static zzgr<zza> zzkj() {
            return (zzgr) zzwa.zza(zzd.zzaij, (Object) null, (Object) null);
        }

        public final String getKey() {
            return this.zzvy;
        }

        public final String getValue() {
            return this.zzvz;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbp.zzud[i - 1]) {
                case 1:
                    return new zza();
                case 2:
                    return new C1832zza(null);
                case 3:
                    return zza((zzgi) zzwa, "\u0001\u0002\u0000\u0001\u0001\u0002\u0002\u0000\u0000\u0000\u0001\b\u0000\u0002\b\u0001", new Object[]{"zzue", "zzvy", "zzvz"});
                case 4:
                    return zzwa;
                case 5:
                    zzgr<zza> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zza.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new zzc(zzwa);
                                zzuo = zzgr;
                            }
                        } finally {
                            Class<zza> cls = zza.class;
                        }
                    }
                    return zzgr;
                case 6:
                    return Byte.valueOf(1);
                case 7:
                    return null;
                default:
                    throw new UnsupportedOperationException();
            }
        }
    }
}
