package com.google.android.gms.internal.measurement;

import java.lang.reflect.Field;
import java.nio.Buffer;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.security.AccessController;
import java.util.logging.Level;
import java.util.logging.Logger;
import libcore.io.Memory;
import sun.misc.Unsafe;

final class zzhv {
    private static final Logger logger = Logger.getLogger(zzhv.class.getName());
    private static final Class<?> zzacx = zzdi.zzrw();
    private static final boolean zzaec = zzww();
    private static final Unsafe zzaki = zzwv();
    private static final boolean zzame = zzk(Long.TYPE);
    private static final boolean zzamf = zzk(Integer.TYPE);
    private static final zzd zzamg;
    private static final boolean zzamh = zzwx();
    static final long zzami = ((long) zzi(byte[].class));
    private static final long zzamj = ((long) zzi(boolean[].class));
    private static final long zzamk = ((long) zzj(boolean[].class));
    private static final long zzaml = ((long) zzi(int[].class));
    private static final long zzamm = ((long) zzj(int[].class));
    private static final long zzamn = ((long) zzi(long[].class));
    private static final long zzamo = ((long) zzj(long[].class));
    private static final long zzamp = ((long) zzi(float[].class));
    private static final long zzamq = ((long) zzj(float[].class));
    private static final long zzamr = ((long) zzi(double[].class));
    private static final long zzams = ((long) zzj(double[].class));
    private static final long zzamt = ((long) zzi(Object[].class));
    private static final long zzamu = ((long) zzj(Object[].class));
    private static final long zzamv;
    private static final int zzamw = ((int) (zzami & 7));
    static final boolean zzamx = (ByteOrder.nativeOrder() == ByteOrder.BIG_ENDIAN);

    static final class zza extends zzd {
        zza(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zza(long j, byte b) {
            Memory.pokeByte((int) (-1 & j), b);
        }

        public final void zza(Object obj, long j, double d) {
            zza(obj, j, Double.doubleToLongBits(d));
        }

        public final void zza(Object obj, long j, float f) {
            zzb(obj, j, Float.floatToIntBits(f));
        }

        public final void zza(Object obj, long j, boolean z) {
            if (zzhv.zzamx) {
                zzhv.zzb(obj, j, z);
            } else {
                zzhv.zzc(obj, j, z);
            }
        }

        public final void zza(byte[] bArr, long j, long j2, long j3) {
            Memory.pokeByteArray((int) (-1 & j2), bArr, (int) j, (int) j3);
        }

        public final void zze(Object obj, long j, byte b) {
            if (zzhv.zzamx) {
                zzhv.zza(obj, j, b);
            } else {
                zzhv.zzb(obj, j, b);
            }
        }

        public final boolean zzm(Object obj, long j) {
            return zzhv.zzamx ? zzhv.zzs(obj, j) : zzhv.zzt(obj, j);
        }

        public final float zzn(Object obj, long j) {
            return Float.intBitsToFloat(zzk(obj, j));
        }

        public final double zzo(Object obj, long j) {
            return Double.longBitsToDouble(zzl(obj, j));
        }

        public final byte zzy(Object obj, long j) {
            return zzhv.zzamx ? zzhv.zzq(obj, j) : zzhv.zzr(obj, j);
        }
    }

    static final class zzb extends zzd {
        zzb(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zza(long j, byte b) {
            this.zzana.putByte(j, b);
        }

        public final void zza(Object obj, long j, double d) {
            this.zzana.putDouble(obj, j, d);
        }

        public final void zza(Object obj, long j, float f) {
            this.zzana.putFloat(obj, j, f);
        }

        public final void zza(Object obj, long j, boolean z) {
            this.zzana.putBoolean(obj, j, z);
        }

        public final void zza(byte[] bArr, long j, long j2, long j3) {
            this.zzana.copyMemory(bArr, zzhv.zzami + j, null, j2, j3);
        }

        public final void zze(Object obj, long j, byte b) {
            this.zzana.putByte(obj, j, b);
        }

        public final boolean zzm(Object obj, long j) {
            return this.zzana.getBoolean(obj, j);
        }

        public final float zzn(Object obj, long j) {
            return this.zzana.getFloat(obj, j);
        }

        public final double zzo(Object obj, long j) {
            return this.zzana.getDouble(obj, j);
        }

        public final byte zzy(Object obj, long j) {
            return this.zzana.getByte(obj, j);
        }
    }

    static final class zzc extends zzd {
        zzc(Unsafe unsafe) {
            super(unsafe);
        }

        public final void zza(long j, byte b) {
            Memory.pokeByte(j, b);
        }

        public final void zza(Object obj, long j, double d) {
            zza(obj, j, Double.doubleToLongBits(d));
        }

        public final void zza(Object obj, long j, float f) {
            zzb(obj, j, Float.floatToIntBits(f));
        }

        public final void zza(Object obj, long j, boolean z) {
            if (zzhv.zzamx) {
                zzhv.zzb(obj, j, z);
            } else {
                zzhv.zzc(obj, j, z);
            }
        }

        public final void zza(byte[] bArr, long j, long j2, long j3) {
            Memory.pokeByteArray(j2, bArr, (int) j, (int) j3);
        }

        public final void zze(Object obj, long j, byte b) {
            if (zzhv.zzamx) {
                zzhv.zza(obj, j, b);
            } else {
                zzhv.zzb(obj, j, b);
            }
        }

        public final boolean zzm(Object obj, long j) {
            return zzhv.zzamx ? zzhv.zzs(obj, j) : zzhv.zzt(obj, j);
        }

        public final float zzn(Object obj, long j) {
            return Float.intBitsToFloat(zzk(obj, j));
        }

        public final double zzo(Object obj, long j) {
            return Double.longBitsToDouble(zzl(obj, j));
        }

        public final byte zzy(Object obj, long j) {
            return zzhv.zzamx ? zzhv.zzq(obj, j) : zzhv.zzr(obj, j);
        }
    }

    static abstract class zzd {
        Unsafe zzana;

        zzd(Unsafe unsafe) {
            this.zzana = unsafe;
        }

        public abstract void zza(long j, byte b);

        public abstract void zza(Object obj, long j, double d);

        public abstract void zza(Object obj, long j, float f);

        public final void zza(Object obj, long j, long j2) {
            this.zzana.putLong(obj, j, j2);
        }

        public abstract void zza(Object obj, long j, boolean z);

        public abstract void zza(byte[] bArr, long j, long j2, long j3);

        public final void zzb(Object obj, long j, int i) {
            this.zzana.putInt(obj, j, i);
        }

        public abstract void zze(Object obj, long j, byte b);

        public final int zzk(Object obj, long j) {
            return this.zzana.getInt(obj, j);
        }

        public final long zzl(Object obj, long j) {
            return this.zzana.getLong(obj, j);
        }

        public abstract boolean zzm(Object obj, long j);

        public abstract float zzn(Object obj, long j);

        public abstract double zzo(Object obj, long j);

        public abstract byte zzy(Object obj, long j);
    }

    static {
        zzd zzd2 = null;
        if (zzaki != null) {
            if (!zzdi.zzrv()) {
                zzd2 = new zzb(zzaki);
            } else if (zzame) {
                zzd2 = new zzc(zzaki);
            } else if (zzamf) {
                zzd2 = new zza(zzaki);
            }
        }
        zzamg = zzd2;
        Field zzwy = zzwy();
        zzamv = (zzwy == null || zzamg == null) ? -1 : zzamg.zzana.objectFieldOffset(zzwy);
    }

    private zzhv() {
    }

    static byte zza(byte[] bArr, long j) {
        return zzamg.zzy(bArr, zzami + j);
    }

    static void zza(long j, byte b) {
        zzamg.zza(j, b);
    }

    /* access modifiers changed from: private */
    public static void zza(Object obj, long j, byte b) {
        int i = ((((int) j) ^ -1) & 3) << 3;
        zzb(obj, -4 & j, (zzk(obj, j & -4) & ((255 << i) ^ -1)) | ((b & 255) << i));
    }

    static void zza(Object obj, long j, double d) {
        zzamg.zza(obj, j, d);
    }

    static void zza(Object obj, long j, float f) {
        zzamg.zza(obj, j, f);
    }

    static void zza(Object obj, long j, long j2) {
        zzamg.zza(obj, j, j2);
    }

    static void zza(Object obj, long j, Object obj2) {
        zzamg.zzana.putObject(obj, j, obj2);
    }

    static void zza(Object obj, long j, boolean z) {
        zzamg.zza(obj, j, z);
    }

    static void zza(byte[] bArr, long j, byte b) {
        zzamg.zze(bArr, zzami + j, b);
    }

    static void zza(byte[] bArr, long j, long j2, long j3) {
        zzamg.zza(bArr, j, j2, j3);
    }

    static long zzb(ByteBuffer byteBuffer) {
        return zzamg.zzl(byteBuffer, zzamv);
    }

    private static Field zzb(Class<?> cls, String str) {
        try {
            return cls.getDeclaredField(str);
        } catch (Throwable th) {
            return null;
        }
    }

    /* access modifiers changed from: private */
    public static void zzb(Object obj, long j, byte b) {
        int i = (((int) j) & 3) << 3;
        zzb(obj, -4 & j, (zzk(obj, j & -4) & ((255 << i) ^ -1)) | ((b & 255) << i));
    }

    static void zzb(Object obj, long j, int i) {
        zzamg.zzb(obj, j, i);
    }

    /* access modifiers changed from: private */
    public static void zzb(Object obj, long j, boolean z) {
        zza(obj, j, (byte) (z ? 1 : 0));
    }

    /* access modifiers changed from: private */
    public static void zzc(Object obj, long j, boolean z) {
        zzb(obj, j, (byte) (z ? 1 : 0));
    }

    static <T> T zzh(Class<T> cls) {
        try {
            return zzaki.allocateInstance(cls);
        } catch (InstantiationException e) {
            throw new IllegalStateException(e);
        }
    }

    private static int zzi(Class<?> cls) {
        if (zzaec) {
            return zzamg.zzana.arrayBaseOffset(cls);
        }
        return -1;
    }

    private static int zzj(Class<?> cls) {
        if (zzaec) {
            return zzamg.zzana.arrayIndexScale(cls);
        }
        return -1;
    }

    static int zzk(Object obj, long j) {
        return zzamg.zzk(obj, j);
    }

    private static boolean zzk(Class<?> cls) {
        if (!zzdi.zzrv()) {
            return false;
        }
        try {
            Class<?> cls2 = zzacx;
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

    static long zzl(Object obj, long j) {
        return zzamg.zzl(obj, j);
    }

    static boolean zzm(Object obj, long j) {
        return zzamg.zzm(obj, j);
    }

    static float zzn(Object obj, long j) {
        return zzamg.zzn(obj, j);
    }

    static double zzo(Object obj, long j) {
        return zzamg.zzo(obj, j);
    }

    static Object zzp(Object obj, long j) {
        return zzamg.zzana.getObject(obj, j);
    }

    /* access modifiers changed from: private */
    public static byte zzq(Object obj, long j) {
        return (byte) (zzk(obj, -4 & j) >>> ((int) (((-1 ^ j) & 3) << 3)));
    }

    /* access modifiers changed from: private */
    public static byte zzr(Object obj, long j) {
        return (byte) (zzk(obj, -4 & j) >>> ((int) ((3 & j) << 3)));
    }

    /* access modifiers changed from: private */
    public static boolean zzs(Object obj, long j) {
        return zzq(obj, j) != 0;
    }

    /* access modifiers changed from: private */
    public static boolean zzt(Object obj, long j) {
        return zzr(obj, j) != 0;
    }

    static boolean zzwt() {
        return zzaec;
    }

    static boolean zzwu() {
        return zzamh;
    }

    static Unsafe zzwv() {
        try {
            return (Unsafe) AccessController.doPrivileged(new zzhx());
        } catch (Throwable th) {
            return null;
        }
    }

    private static boolean zzww() {
        if (zzaki == null) {
            return false;
        }
        try {
            Class cls = zzaki.getClass();
            cls.getMethod("objectFieldOffset", new Class[]{Field.class});
            cls.getMethod("arrayBaseOffset", new Class[]{Class.class});
            cls.getMethod("arrayIndexScale", new Class[]{Class.class});
            cls.getMethod("getInt", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putInt", new Class[]{Object.class, Long.TYPE, Integer.TYPE});
            cls.getMethod("getLong", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putLong", new Class[]{Object.class, Long.TYPE, Long.TYPE});
            cls.getMethod("getObject", new Class[]{Object.class, Long.TYPE});
            cls.getMethod("putObject", new Class[]{Object.class, Long.TYPE, Object.class});
            if (zzdi.zzrv()) {
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
            Logger logger2 = logger;
            Level level = Level.WARNING;
            String valueOf = String.valueOf(th);
            logger2.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeArrayOperations", new StringBuilder(String.valueOf(valueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(valueOf).toString());
            return false;
        }
    }

    private static boolean zzwx() {
        if (zzaki == null) {
            return false;
        }
        try {
            Class cls = zzaki.getClass();
            cls.getMethod("objectFieldOffset", new Class[]{Field.class});
            cls.getMethod("getLong", new Class[]{Object.class, Long.TYPE});
            if (zzwy() == null) {
                return false;
            }
            if (zzdi.zzrv()) {
                return true;
            }
            cls.getMethod("getByte", new Class[]{Long.TYPE});
            cls.getMethod("putByte", new Class[]{Long.TYPE, Byte.TYPE});
            cls.getMethod("getInt", new Class[]{Long.TYPE});
            cls.getMethod("putInt", new Class[]{Long.TYPE, Integer.TYPE});
            cls.getMethod("getLong", new Class[]{Long.TYPE});
            cls.getMethod("putLong", new Class[]{Long.TYPE, Long.TYPE});
            cls.getMethod("copyMemory", new Class[]{Long.TYPE, Long.TYPE, Long.TYPE});
            cls.getMethod("copyMemory", new Class[]{Object.class, Long.TYPE, Object.class, Long.TYPE, Long.TYPE});
            return true;
        } catch (Throwable th) {
            Logger logger2 = logger;
            Level level = Level.WARNING;
            String valueOf = String.valueOf(th);
            logger2.logp(level, "com.google.protobuf.UnsafeUtil", "supportsUnsafeByteBufferOperations", new StringBuilder(String.valueOf(valueOf).length() + 71).append("platform method missing - proto runtime falling back to safer methods: ").append(valueOf).toString());
            return false;
        }
    }

    private static Field zzwy() {
        if (zzdi.zzrv()) {
            Field zzb2 = zzb(Buffer.class, "effectiveDirectAddress");
            if (zzb2 != null) {
                return zzb2;
            }
        }
        Field zzb3 = zzb(Buffer.class, "address");
        if (zzb3 == null || zzb3.getType() != Long.TYPE) {
            return null;
        }
        return zzb3;
    }
}
