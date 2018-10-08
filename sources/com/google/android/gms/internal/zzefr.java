package com.google.android.gms.internal;

import java.lang.reflect.Field;
import java.security.AccessController;
import java.util.logging.Level;
import java.util.logging.Logger;
import sun.misc.Unsafe;

final class zzefr {
    private static final Logger logger = Logger.getLogger(zzefr.class.getName());
    private static final boolean zzmyg = zzcdl();
    private static final Unsafe zznal = zzcdk();
    private static final Class<?> zznam = zzre("libcore.io.Memory");
    private static final boolean zznan = (zzre("org.robolectric.Robolectric") != null);
    private static final boolean zznao = zzi(Long.TYPE);
    private static final boolean zznap = zzi(Integer.TYPE);
    private static final zzd zznaq;
    private static final boolean zznar = zzcdn();
    private static final boolean zznas = zzcdm();
    private static final long zznat = ((long) zzg(byte[].class));
    private static final long zznau = ((long) zzg(boolean[].class));
    private static final long zznav = ((long) zzh(boolean[].class));
    private static final long zznaw = ((long) zzg(int[].class));
    private static final long zznax = ((long) zzh(int[].class));
    private static final long zznay = ((long) zzg(long[].class));
    private static final long zznaz = ((long) zzh(long[].class));
    private static final long zznba = ((long) zzg(float[].class));
    private static final long zznbb = ((long) zzh(float[].class));
    private static final long zznbc = ((long) zzg(double[].class));
    private static final long zznbd = ((long) zzh(double[].class));
    private static final long zznbe = ((long) zzg(Object[].class));
    private static final long zznbf = ((long) zzh(Object[].class));
    private static final long zznbg;
    private static final boolean zznbh;

    static abstract class zzd {
        Unsafe zznbi;

        zzd(Unsafe unsafe) {
            this.zznbi = unsafe;
        }

        public abstract void zze(Object obj, long j, byte b);

        public abstract byte zzf(Object obj, long j);
    }

    static final class zza extends zzd {
        zza(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zze(Object obj, long j, byte b) {
            if (zzefr.zznbh) {
                zzefr.zza(obj, j, b);
            } else {
                zzefr.zzb(obj, j, b);
            }
        }

        public final byte zzf(Object obj, long j) {
            return zzefr.zznbh ? zzefr.zzb(obj, j) : zzefr.zzc(obj, j);
        }
    }

    static final class zzb extends zzd {
        zzb(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zze(Object obj, long j, byte b) {
            if (zzefr.zznbh) {
                zzefr.zza(obj, j, b);
            } else {
                zzefr.zzb(obj, j, b);
            }
        }

        public final byte zzf(Object obj, long j) {
            return zzefr.zznbh ? zzefr.zzb(obj, j) : zzefr.zzc(obj, j);
        }
    }

    static final class zzc extends zzd {
        zzc(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zze(Object obj, long j, byte b) {
            this.zznbi.putByte(obj, j, b);
        }

        public final byte zzf(Object obj, long j) {
            return this.zznbi.getByte(obj, j);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static {
        /*
        r3 = 0;
        r1 = 1;
        r2 = 0;
        r0 = com.google.android.gms.internal.zzefr.class;
        r0 = r0.getName();
        r0 = java.util.logging.Logger.getLogger(r0);
        logger = r0;
        r0 = zzcdk();
        zznal = r0;
        r0 = "libcore.io.Memory";
        r0 = zzre(r0);
        zznam = r0;
        r0 = "org.robolectric.Robolectric";
        r0 = zzre(r0);
        if (r0 == 0) goto L_0x00eb;
    L_0x0025:
        r0 = r1;
    L_0x0026:
        zznan = r0;
        r0 = java.lang.Long.TYPE;
        r0 = zzi(r0);
        zznao = r0;
        r0 = java.lang.Integer.TYPE;
        r0 = zzi(r0);
        zznap = r0;
        r0 = zznal;
        if (r0 != 0) goto L_0x00ee;
    L_0x003c:
        r0 = r3;
    L_0x003d:
        zznaq = r0;
        r0 = zzcdn();
        zznar = r0;
        r0 = zzcdl();
        zzmyg = r0;
        r0 = zzcdm();
        zznas = r0;
        r0 = byte[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznat = r4;
        r0 = boolean[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznau = r4;
        r0 = boolean[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznav = r4;
        r0 = int[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznaw = r4;
        r0 = int[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznax = r4;
        r0 = long[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznay = r4;
        r0 = long[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznaz = r4;
        r0 = float[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznba = r4;
        r0 = float[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznbb = r4;
        r0 = double[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznbc = r4;
        r0 = double[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznbd = r4;
        r0 = java.lang.Object[].class;
        r0 = zzg(r0);
        r4 = (long) r0;
        zznbe = r4;
        r0 = java.lang.Object[].class;
        r0 = zzh(r0);
        r4 = (long) r0;
        zznbf = r4;
        r0 = zzcdo();
        if (r0 == 0) goto L_0x011a;
    L_0x00cc:
        r0 = java.nio.Buffer.class;
        r3 = "effectiveDirectAddress";
        r0 = zza(r0, r3);
        if (r0 == 0) goto L_0x011a;
    L_0x00d6:
        if (r0 == 0) goto L_0x00dc;
    L_0x00d8:
        r3 = zznaq;
        if (r3 != 0) goto L_0x0123;
    L_0x00dc:
        r4 = -1;
    L_0x00de:
        zznbg = r4;
        r0 = java.nio.ByteOrder.nativeOrder();
        r3 = java.nio.ByteOrder.BIG_ENDIAN;
        if (r0 != r3) goto L_0x012c;
    L_0x00e8:
        zznbh = r1;
        return;
    L_0x00eb:
        r0 = r2;
        goto L_0x0026;
    L_0x00ee:
        r0 = zzcdo();
        if (r0 == 0) goto L_0x0111;
    L_0x00f4:
        r0 = zznao;
        if (r0 == 0) goto L_0x0101;
    L_0x00f8:
        r0 = new com.google.android.gms.internal.zzefr$zzb;
        r3 = zznal;
        r0.<init>(r3);
        goto L_0x003d;
    L_0x0101:
        r0 = zznap;
        if (r0 == 0) goto L_0x010e;
    L_0x0105:
        r0 = new com.google.android.gms.internal.zzefr$zza;
        r3 = zznal;
        r0.<init>(r3);
        goto L_0x003d;
    L_0x010e:
        r0 = r3;
        goto L_0x003d;
    L_0x0111:
        r0 = new com.google.android.gms.internal.zzefr$zzc;
        r3 = zznal;
        r0.<init>(r3);
        goto L_0x003d;
    L_0x011a:
        r0 = java.nio.Buffer.class;
        r3 = "address";
        r0 = zza(r0, r3);
        goto L_0x00d6;
    L_0x0123:
        r3 = zznaq;
        r3 = r3.zznbi;
        r4 = r3.objectFieldOffset(r0);
        goto L_0x00de;
    L_0x012c:
        r1 = r2;
        goto L_0x00e8;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzefr.<clinit>():void");
    }

    private zzefr() {
    }

    private static int zza(Object obj, long j) {
        return zznaq.zznbi.getInt(obj, j);
    }

    private static Field zza(Class<?> cls, String str) {
        try {
            Field declaredField = cls.getDeclaredField(str);
            declaredField.setAccessible(true);
            return declaredField;
        } catch (Throwable th) {
            return null;
        }
    }

    private static void zza(Object obj, long j, byte b) {
        int i = ((((int) j) ^ -1) & 3) << 3;
        zza(obj, -4 & j, (zza(obj, j & -4) & ((255 << i) ^ -1)) | ((b & 255) << i));
    }

    private static void zza(Object obj, long j, int i) {
        zznaq.zznbi.putInt(obj, j, i);
    }

    static void zza(byte[] bArr, long j, byte b) {
        zznaq.zze(bArr, zznat + j, b);
    }

    private static byte zzb(Object obj, long j) {
        return (byte) (zza(obj, -4 & j) >>> ((int) (((-1 ^ j) & 3) << 3)));
    }

    static byte zzb(byte[] bArr, long j) {
        return zznaq.zzf(bArr, zznat + j);
    }

    private static void zzb(Object obj, long j, byte b) {
        int i = (((int) j) & 3) << 3;
        zza(obj, -4 & j, (zza(obj, j & -4) & ((255 << i) ^ -1)) | ((b & 255) << i));
    }

    private static byte zzc(Object obj, long j) {
        return (byte) (zza(obj, -4 & j) >>> ((int) ((3 & j) << 3)));
    }

    static boolean zzcdi() {
        return zzmyg;
    }

    static boolean zzcdj() {
        return zznar;
    }

    private static Unsafe zzcdk() {
        try {
            return (Unsafe) AccessController.doPrivileged(new zzefs());
        } catch (Throwable th) {
            return null;
        }
    }

    private static boolean zzcdl() {
        if (zznal == null) {
            return false;
        }
        try {
            Class cls = zznal.getClass();
            cls.getMethod("objectFieldOffset", new Class[]{Field.class});
            cls.getMethod("arrayBaseOffset", new Class[]{Class.class});
            cls.getMethod("arrayIndexScale", new Class[]{Class.class});
            cls.getMethod("getInt", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putInt", new Class[]{Object.class, Long.TYPE, Integer.TYPE});
            cls.getMethod("getLong", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putLong", new Class[]{Object.class, Long.TYPE, Long.TYPE});
            cls.getMethod("getObject", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putObject", new Class[]{Object.class, Long.TYPE, Object.class});
            if (zzcdo()) {
                return true;
            }
            cls.getMethod("getByte", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putByte", new Class[]{Object.class, Long.TYPE, Byte.TYPE});
            cls.getMethod("getBoolean", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putBoolean", new Class[]{Object.class, Long.TYPE, Boolean.TYPE});
            cls.getMethod("getFloat", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putFloat", new Class[]{Object.class, Long.TYPE, Float.TYPE});
            cls.getMethod("getDouble", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putDouble", new Class[]{Object.class, Long.TYPE, Double.TYPE});
            return true;
        } catch (Throwable th) {
            Logger logger = logger;
            Level level = Level.WARNING;
            String valueOf = String.valueOf(th);
            logger.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeArrayOperations", new StringBuilder(String.valueOf(valueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(valueOf).toString());
            return false;
        }
    }

    private static boolean zzcdm() {
        if (zznal == null) {
            return false;
        }
        try {
            zznal.getClass().getMethod("copyMemory", new Class[]{Object.class, Long.TYPE, Object.class, Long.TYPE, Long.TYPE});
            return true;
        } catch (Throwable th) {
            logger.logp(Level.WARNING, "com.google.protobuf.UnsafeUtil", "supportsUnsafeCopyMemory", "copyMemory is missing from platform - proto runtime falling back to safer methods.");
            return false;
        }
    }

    private static boolean zzcdn() {
        if (zznal == null) {
            return false;
        }
        try {
            Class cls = zznal.getClass();
            cls.getMethod("objectFieldOffset", new Class[]{Field.class});
            cls.getMethod("getLong", new Class[]{Object.class, Long.TYPE});
            if (zzcdo()) {
                return true;
            }
            cls.getMethod("getByte", new Class[]{Long.TYPE});
            cls.getMethod("putByte", new Class[]{Long.TYPE, Byte.TYPE});
            cls.getMethod("getInt", new Class[]{Long.TYPE});
            cls.getMethod("putInt", new Class[]{Long.TYPE, Integer.TYPE});
            cls.getMethod("getLong", new Class[]{Long.TYPE});
            cls.getMethod("putLong", new Class[]{Long.TYPE, Long.TYPE});
            cls.getMethod("copyMemory", new Class[]{Long.TYPE, Long.TYPE, Long.TYPE});
            return true;
        } catch (Throwable th) {
            Logger logger = logger;
            Level level = Level.WARNING;
            String valueOf = String.valueOf(th);
            logger.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeByteBufferOperations", new StringBuilder(String.valueOf(valueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(valueOf).toString());
            return false;
        }
    }

    private static boolean zzcdo() {
        return (zznam == null || zznan) ? false : true;
    }

    private static int zzg(Class<?> cls) {
        return zzmyg ? zznaq.zznbi.arrayBaseOffset(cls) : -1;
    }

    private static int zzh(Class<?> cls) {
        return zzmyg ? zznaq.zznbi.arrayIndexScale(cls) : -1;
    }

    private static boolean zzi(Class<?> cls) {
        if (!zzcdo()) {
            return false;
        }
        try {
            Class cls2 = zznam;
            cls2.getMethod("peekLong", new Class[]{cls, Boolean.TYPE});
            cls2.getMethod("pokeLong", new Class[]{cls, Long.TYPE, Boolean.TYPE});
            cls2.getMethod("pokeInt", new Class[]{cls, Integer.TYPE, Boolean.TYPE});
            cls2.getMethod("peekInt", new Class[]{cls, Boolean.TYPE});
            cls2.getMethod("pokeByte", new Class[]{cls, Byte.TYPE});
            cls2.getMethod("peekByte", new Class[]{cls});
            cls2.getMethod("pokeByteArray", new Class[]{cls, byte[].class, Integer.TYPE, Integer.TYPE});
            cls2.getMethod("peekByteArray", new Class[]{cls, byte[].class, Integer.TYPE, Integer.TYPE});
            return true;
        } catch (Throwable th) {
            return false;
        }
    }

    private static <T> Class<T> zzre(String str) {
        try {
            return Class.forName(str);
        } catch (Throwable th) {
            return null;
        }
    }
}
