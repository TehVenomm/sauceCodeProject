package com.google.android.gms.internal.measurement;

import java.util.List;

public final class zzbk {

    public static final class zza extends zzey<zza, C1831zza> implements zzgk {
        /* access modifiers changed from: private */
        public static final zza zzun = new zza();
        private static volatile zzgr<zza> zzuo;
        private int zzue;
        private int zzuf;
        private String zzug = "";
        private zzff<zzb> zzuh = zzun();
        private boolean zzui;
        private zzc zzuj;
        private boolean zzuk;
        private boolean zzul;
        private boolean zzum;

        /* renamed from: com.google.android.gms.internal.measurement.zzbk$zza$zza reason: collision with other inner class name */
        public static final class C1831zza extends com.google.android.gms.internal.measurement.zzey.zza<zza, C1831zza> implements zzgk {
            private C1831zza() {
                super(zza.zzun);
            }

            /* synthetic */ C1831zza(zzbj zzbj) {
                this();
            }

            public final C1831zza zza(int i, zzb zzb) {
                zzuc();
                ((zza) this.zzahx).zzb(i, zzb);
                return this;
            }

            public final C1831zza zzbs(String str) {
                zzuc();
                ((zza) this.zzahx).zzbt(str);
                return this;
            }

            public final zzb zze(int i) {
                return ((zza) this.zzahx).zze(i);
            }

            public final String zzjz() {
                return ((zza) this.zzahx).zzjz();
            }

            public final int zzka() {
                return ((zza) this.zzahx).zzka();
            }
        }

        static {
            zzey.zza(zza.class, zzun);
        }

        private zza() {
        }

        public static zza zza(byte[] bArr, zzel zzel) throws zzfi {
            return (zza) zzey.zza(zzun, bArr, zzel);
        }

        /* access modifiers changed from: private */
        public final void zzb(int i, zzb zzb) {
            if (zzb == null) {
                throw new NullPointerException();
            }
            if (!this.zzuh.zzrx()) {
                this.zzuh = zzey.zza(this.zzuh);
            }
            this.zzuh.set(i, zzb);
        }

        /* access modifiers changed from: private */
        public final void zzbt(String str) {
            if (str == null) {
                throw new NullPointerException();
            }
            this.zzue |= 2;
            this.zzug = str;
        }

        public static zzgr<zza> zzkj() {
            return (zzgr) zzun.zza(com.google.android.gms.internal.measurement.zzey.zzd.zzaij, (Object) null, (Object) null);
        }

        public final int getId() {
            return this.zzuf;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbj.zzud[i - 1]) {
                case 1:
                    return new zza();
                case 2:
                    return new C1831zza(null);
                case 3:
                    return zza((zzgi) zzun, "\u0001\b\u0000\u0001\u0001\b\b\u0000\u0001\u0000\u0001\u0004\u0000\u0002\b\u0001\u0003\u001b\u0004\u0007\u0002\u0005\t\u0003\u0006\u0007\u0004\u0007\u0007\u0005\b\u0007\u0006", new Object[]{"zzue", "zzuf", "zzug", "zzuh", zzb.class, "zzui", "zzuj", "zzuk", "zzul", "zzum"});
                case 4:
                    return zzun;
                case 5:
                    zzgr<zza> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zza.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new com.google.android.gms.internal.measurement.zzey.zzc(zzun);
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

        public final zzb zze(int i) {
            return (zzb) this.zzuh.get(i);
        }

        public final String zzjz() {
            return this.zzug;
        }

        public final int zzka() {
            return this.zzuh.size();
        }

        public final boolean zzkb() {
            return (this.zzue & 1) != 0;
        }

        public final List<zzb> zzkc() {
            return this.zzuh;
        }

        public final boolean zzkd() {
            return (this.zzue & 8) != 0;
        }

        public final zzc zzke() {
            return this.zzuj == null ? zzc.zzle() : this.zzuj;
        }

        public final boolean zzkf() {
            return this.zzuk;
        }

        public final boolean zzkg() {
            return this.zzul;
        }

        public final boolean zzkh() {
            return (this.zzue & 64) != 0;
        }

        public final boolean zzki() {
            return this.zzum;
        }
    }

    public static final class zzb extends zzey<zzb, zza> implements zzgk {
        private static volatile zzgr<zzb> zzuo;
        /* access modifiers changed from: private */
        public static final zzb zzut = new zzb();
        private int zzue;
        private zze zzup;
        private zzc zzuq;
        private boolean zzur;
        private String zzus = "";

        public static final class zza extends com.google.android.gms.internal.measurement.zzey.zza<zzb, zza> implements zzgk {
            private zza() {
                super(zzb.zzut);
            }

            /* synthetic */ zza(zzbj zzbj) {
                this();
            }

            public final zza zzbu(String str) {
                zzuc();
                ((zzb) this.zzahx).zzbv(str);
                return this;
            }
        }

        static {
            zzey.zza(zzb.class, zzut);
        }

        private zzb() {
        }

        /* access modifiers changed from: private */
        public final void zzbv(String str) {
            if (str == null) {
                throw new NullPointerException();
            }
            this.zzue |= 8;
            this.zzus = str;
        }

        public static zzb zzks() {
            return zzut;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbj.zzud[i - 1]) {
                case 1:
                    return new zzb();
                case 2:
                    return new zza(null);
                case 3:
                    return zza((zzgi) zzut, "\u0001\u0004\u0000\u0001\u0001\u0004\u0004\u0000\u0000\u0000\u0001\t\u0000\u0002\t\u0001\u0003\u0007\u0002\u0004\b\u0003", new Object[]{"zzue", "zzup", "zzuq", "zzur", "zzus"});
                case 4:
                    return zzut;
                case 5:
                    zzgr<zzb> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zzb.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new com.google.android.gms.internal.measurement.zzey.zzc(zzut);
                                zzuo = zzgr;
                            }
                        } finally {
                            Class<zzb> cls = zzb.class;
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

        public final boolean zzkl() {
            return (this.zzue & 1) != 0;
        }

        public final zze zzkm() {
            return this.zzup == null ? zze.zzls() : this.zzup;
        }

        public final boolean zzkn() {
            return (this.zzue & 2) != 0;
        }

        public final zzc zzko() {
            return this.zzuq == null ? zzc.zzle() : this.zzuq;
        }

        public final boolean zzkp() {
            return (this.zzue & 4) != 0;
        }

        public final boolean zzkq() {
            return this.zzur;
        }

        public final String zzkr() {
            return this.zzus;
        }
    }

    public static final class zzc extends zzey<zzc, zza> implements zzgk {
        private static volatile zzgr<zzc> zzuo;
        /* access modifiers changed from: private */
        public static final zzc zzuz = new zzc();
        private int zzue;
        private int zzuu;
        private boolean zzuv;
        private String zzuw = "";
        private String zzux = "";
        private String zzuy = "";

        public static final class zza extends com.google.android.gms.internal.measurement.zzey.zza<zzc, zza> implements zzgk {
            private zza() {
                super(zzc.zzuz);
            }

            /* synthetic */ zza(zzbj zzbj) {
                this();
            }
        }

        public enum zzb implements zzfc {
            UNKNOWN_COMPARISON_TYPE(0),
            LESS_THAN(1),
            GREATER_THAN(2),
            EQUAL(3),
            BETWEEN(4);
            
            private static final zzfb<zzb> zzvf = null;
            private final int value;

            static {
                zzvf = new zzbl();
            }

            private zzb(int i) {
                this.value = i;
            }

            public static zzb zzf(int i) {
                switch (i) {
                    case 0:
                        return UNKNOWN_COMPARISON_TYPE;
                    case 1:
                        return LESS_THAN;
                    case 2:
                        return GREATER_THAN;
                    case 3:
                        return EQUAL;
                    case 4:
                        return BETWEEN;
                    default:
                        return null;
                }
            }

            public static zzfe zzlh() {
                return zzbm.zzvk;
            }

            public final int zzlg() {
                return this.value;
            }
        }

        static {
            zzey.zza(zzc.class, zzuz);
        }

        private zzc() {
        }

        public static zzc zzle() {
            return zzuz;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbj.zzud[i - 1]) {
                case 1:
                    return new zzc();
                case 2:
                    return new zza(null);
                case 3:
                    return zza((zzgi) zzuz, "\u0001\u0005\u0000\u0001\u0001\u0005\u0005\u0000\u0000\u0000\u0001\f\u0000\u0002\u0007\u0001\u0003\b\u0002\u0004\b\u0003\u0005\b\u0004", new Object[]{"zzue", "zzuu", zzb.zzlh(), "zzuv", "zzuw", "zzux", "zzuy"});
                case 4:
                    return zzuz;
                case 5:
                    zzgr<zzc> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zzc.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new com.google.android.gms.internal.measurement.zzey.zzc(zzuz);
                                zzuo = zzgr;
                            }
                        } finally {
                            Class<zzc> cls = zzc.class;
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

        public final boolean zzku() {
            return (this.zzue & 1) != 0;
        }

        public final zzb zzkv() {
            zzb zzf = zzb.zzf(this.zzuu);
            return zzf == null ? zzb.UNKNOWN_COMPARISON_TYPE : zzf;
        }

        public final boolean zzkw() {
            return (this.zzue & 2) != 0;
        }

        public final boolean zzkx() {
            return this.zzuv;
        }

        public final boolean zzky() {
            return (this.zzue & 4) != 0;
        }

        public final String zzkz() {
            return this.zzuw;
        }

        public final boolean zzla() {
            return (this.zzue & 8) != 0;
        }

        public final String zzlb() {
            return this.zzux;
        }

        public final boolean zzlc() {
            return (this.zzue & 16) != 0;
        }

        public final String zzld() {
            return this.zzuy;
        }
    }

    public static final class zzd extends zzey<zzd, zza> implements zzgk {
        private static volatile zzgr<zzd> zzuo;
        /* access modifiers changed from: private */
        public static final zzd zzvj = new zzd();
        private int zzue;
        private int zzuf;
        private boolean zzuk;
        private boolean zzul;
        private boolean zzum;
        private String zzvh = "";
        private zzb zzvi;

        public static final class zza extends com.google.android.gms.internal.measurement.zzey.zza<zzd, zza> implements zzgk {
            private zza() {
                super(zzd.zzvj);
            }

            /* synthetic */ zza(zzbj zzbj) {
                this();
            }

            public final zza zzbw(String str) {
                zzuc();
                ((zzd) this.zzahx).setPropertyName(str);
                return this;
            }
        }

        static {
            zzey.zza(zzd.class, zzvj);
        }

        private zzd() {
        }

        /* access modifiers changed from: private */
        public final void setPropertyName(String str) {
            if (str == null) {
                throw new NullPointerException();
            }
            this.zzue |= 2;
            this.zzvh = str;
        }

        public static zzd zzb(byte[] bArr, zzel zzel) throws zzfi {
            return (zzd) zzey.zza(zzvj, bArr, zzel);
        }

        public static zzgr<zzd> zzkj() {
            return (zzgr) zzvj.zza(com.google.android.gms.internal.measurement.zzey.zzd.zzaij, (Object) null, (Object) null);
        }

        public final int getId() {
            return this.zzuf;
        }

        public final String getPropertyName() {
            return this.zzvh;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbj.zzud[i - 1]) {
                case 1:
                    return new zzd();
                case 2:
                    return new zza(null);
                case 3:
                    return zza((zzgi) zzvj, "\u0001\u0006\u0000\u0001\u0001\u0006\u0006\u0000\u0000\u0000\u0001\u0004\u0000\u0002\b\u0001\u0003\t\u0002\u0004\u0007\u0003\u0005\u0007\u0004\u0006\u0007\u0005", new Object[]{"zzue", "zzuf", "zzvh", "zzvi", "zzuk", "zzul", "zzum"});
                case 4:
                    return zzvj;
                case 5:
                    zzgr<zzd> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zzd.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new com.google.android.gms.internal.measurement.zzey.zzc(zzvj);
                                zzuo = zzgr;
                            }
                        } finally {
                            Class<zzd> cls = zzd.class;
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

        public final boolean zzkb() {
            return (this.zzue & 1) != 0;
        }

        public final boolean zzkf() {
            return this.zzuk;
        }

        public final boolean zzkg() {
            return this.zzul;
        }

        public final boolean zzkh() {
            return (this.zzue & 32) != 0;
        }

        public final boolean zzki() {
            return this.zzum;
        }

        public final zzb zzli() {
            return this.zzvi == null ? zzb.zzks() : this.zzvi;
        }
    }

    public static final class zze extends zzey<zze, zzb> implements zzgk {
        private static volatile zzgr<zze> zzuo;
        /* access modifiers changed from: private */
        public static final zze zzvp = new zze();
        private int zzue;
        private int zzvl;
        private String zzvm = "";
        private boolean zzvn;
        private zzff<String> zzvo = zzey.zzun();

        public enum zza implements zzfc {
            UNKNOWN_MATCH_TYPE(0),
            REGEXP(1),
            BEGINS_WITH(2),
            ENDS_WITH(3),
            PARTIAL(4),
            EXACT(5),
            IN_LIST(6);
            
            private static final zzfb<zza> zzvf = null;
            private final int value;

            static {
                zzvf = new zzbo();
            }

            private zza(int i) {
                this.value = i;
            }

            public static zza zzh(int i) {
                switch (i) {
                    case 0:
                        return UNKNOWN_MATCH_TYPE;
                    case 1:
                        return REGEXP;
                    case 2:
                        return BEGINS_WITH;
                    case 3:
                        return ENDS_WITH;
                    case 4:
                        return PARTIAL;
                    case 5:
                        return EXACT;
                    case 6:
                        return IN_LIST;
                    default:
                        return null;
                }
            }

            public static zzfe zzlh() {
                return zzbn.zzvk;
            }

            public final int zzlg() {
                return this.value;
            }
        }

        public static final class zzb extends com.google.android.gms.internal.measurement.zzey.zza<zze, zzb> implements zzgk {
            private zzb() {
                super(zze.zzvp);
            }

            /* synthetic */ zzb(zzbj zzbj) {
                this();
            }
        }

        static {
            zzey.zza(zze.class, zzvp);
        }

        private zze() {
        }

        public static zze zzls() {
            return zzvp;
        }

        /* access modifiers changed from: protected */
        public final Object zza(int i, Object obj, Object obj2) {
            zzgr zzgr;
            switch (zzbj.zzud[i - 1]) {
                case 1:
                    return new zze();
                case 2:
                    return new zzb(null);
                case 3:
                    return zza((zzgi) zzvp, "\u0001\u0004\u0000\u0001\u0001\u0004\u0004\u0000\u0001\u0000\u0001\f\u0000\u0002\b\u0001\u0003\u0007\u0002\u0004\u001a", new Object[]{"zzue", "zzvl", zza.zzlh(), "zzvm", "zzvn", "zzvo"});
                case 4:
                    return zzvp;
                case 5:
                    zzgr<zze> zzgr2 = zzuo;
                    if (zzgr2 != null) {
                        return zzgr2;
                    }
                    synchronized (zze.class) {
                        try {
                            zzgr = zzuo;
                            if (zzgr == null) {
                                zzgr = new com.google.android.gms.internal.measurement.zzey.zzc(zzvp);
                                zzuo = zzgr;
                            }
                        } finally {
                            Class<zze> cls = zze.class;
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

        public final boolean zzlk() {
            return (this.zzue & 1) != 0;
        }

        public final zza zzll() {
            zza zzh = zza.zzh(this.zzvl);
            return zzh == null ? zza.UNKNOWN_MATCH_TYPE : zzh;
        }

        public final boolean zzlm() {
            return (this.zzue & 2) != 0;
        }

        public final String zzln() {
            return this.zzvm;
        }

        public final boolean zzlo() {
            return (this.zzue & 4) != 0;
        }

        public final boolean zzlp() {
            return this.zzvn;
        }

        public final List<String> zzlq() {
            return this.zzvo;
        }

        public final int zzlr() {
            return this.zzvo.size();
        }
    }
}
