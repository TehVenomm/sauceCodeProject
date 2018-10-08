package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

public abstract class zzedw extends zzedj {
    private static final Logger logger = Logger.getLogger(zzedw.class.getName());
    private static final boolean zzmyg = zzefr.zzcdi();

    static class zza extends zzedw {
        private final byte[] buffer;
        private final int limit;
        private final int offset;
        private int position;

        zza(byte[] bArr, int i, int i2) {
            super();
            if (bArr == null) {
                throw new NullPointerException("buffer");
            } else if (((i | i2) | (bArr.length - (i + i2))) < 0) {
                throw new IllegalArgumentException(String.format("Array range is invalid. Buffer.length=%d, offset=%d, length=%d", new Object[]{Integer.valueOf(bArr.length), Integer.valueOf(i), Integer.valueOf(i2)}));
            } else {
                this.buffer = bArr;
                this.offset = i;
                this.position = i;
                this.limit = i + i2;
            }
        }

        public final void write(byte[] bArr, int i, int i2) throws IOException {
            try {
                System.arraycopy(bArr, i, this.buffer, this.position, i2);
                this.position += i2;
            } catch (Throwable e) {
                throw new zzb(String.format("Pos: %d, limit: %d, len: %d", new Object[]{Integer.valueOf(this.position), Integer.valueOf(this.limit), Integer.valueOf(i2)}), e);
            }
        }

        public final void zza(int i, zzedk zzedk) throws IOException {
            zzt(i, 2);
            zzah(zzedk);
        }

        public final void zza(int i, zzeey zzeey) throws IOException {
            zzt(i, 2);
            zzd(zzeey);
        }

        public final void zzah(zzedk zzedk) throws IOException {
            zzgq(zzedk.size());
            zzedk.zza(this);
        }

        public final void zzb(byte[] bArr, int i, int i2) throws IOException {
            write(bArr, i, i2);
        }

        public final int zzccc() {
            return this.limit - this.position;
        }

        public final void zzcn(long j) throws IOException {
            byte[] bArr;
            int i;
            if (!zzedw.zzmyg || zzccc() < 10) {
                while ((j & -128) != 0) {
                    try {
                        bArr = this.buffer;
                        i = this.position;
                        this.position = i + 1;
                        bArr[i] = (byte) ((byte) ((((int) j) & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128));
                        j >>>= 7;
                    } catch (Throwable e) {
                        throw new zzb(String.format("Pos: %d, limit: %d, len: %d", new Object[]{Integer.valueOf(this.position), Integer.valueOf(this.limit), Integer.valueOf(1)}), e);
                    }
                }
                bArr = this.buffer;
                i = this.position;
                this.position = i + 1;
                bArr[i] = (byte) ((byte) ((int) j));
                return;
            }
            while ((j & -128) != 0) {
                bArr = this.buffer;
                i = this.position;
                this.position = i + 1;
                zzefr.zza(bArr, (long) i, (byte) ((((int) j) & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128));
                j >>>= 7;
            }
            bArr = this.buffer;
            i = this.position;
            this.position = i + 1;
            zzefr.zza(bArr, (long) i, (byte) ((int) j));
        }

        public final void zzd(zzeey zzeey) throws IOException {
            zzgq(zzeey.zzbjo());
            zzeey.zza(this);
        }

        public final void zzgp(int i) throws IOException {
            if (i >= 0) {
                zzgq(i);
            } else {
                zzcn((long) i);
            }
        }

        public final void zzgq(int i) throws IOException {
            byte[] bArr;
            int i2;
            if (!zzedw.zzmyg || zzccc() < 10) {
                while ((i & -128) != 0) {
                    try {
                        bArr = this.buffer;
                        i2 = this.position;
                        this.position = i2 + 1;
                        bArr[i2] = (byte) ((byte) ((i & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128));
                        i >>>= 7;
                    } catch (Throwable e) {
                        throw new zzb(String.format("Pos: %d, limit: %d, len: %d", new Object[]{Integer.valueOf(this.position), Integer.valueOf(this.limit), Integer.valueOf(1)}), e);
                    }
                }
                bArr = this.buffer;
                i2 = this.position;
                this.position = i2 + 1;
                bArr[i2] = (byte) ((byte) i);
                return;
            }
            while ((i & -128) != 0) {
                bArr = this.buffer;
                i2 = this.position;
                this.position = i2 + 1;
                zzefr.zza(bArr, (long) i2, (byte) ((i & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128));
                i >>>= 7;
            }
            bArr = this.buffer;
            i2 = this.position;
            this.position = i2 + 1;
            zzefr.zza(bArr, (long) i2, (byte) i);
        }

        public final void zzl(int i, String str) throws IOException {
            zzt(1, 2);
            zzrb(str);
        }

        public final void zzrb(String str) throws IOException {
            int i = this.position;
            try {
                int zzgt = zzedw.zzgt(str.length() * 3);
                int zzgt2 = zzedw.zzgt(str.length());
                if (zzgt2 == zzgt) {
                    this.position = i + zzgt2;
                    zzgt = zzeft.zza(str, this.buffer, this.position, zzccc());
                    this.position = i;
                    zzgq((zzgt - i) - zzgt2);
                    this.position = zzgt;
                    return;
                }
                zzgq(zzeft.zzb(str));
                this.position = zzeft.zza(str, this.buffer, this.position, zzccc());
            } catch (zzefw e) {
                this.position = i;
                zza(str, e);
            } catch (Throwable e2) {
                throw new zzb(e2);
            }
        }

        public final void zzt(int i, int i2) throws IOException {
            zzgq((i << 3) | i2);
        }

        public final void zzu(int i, int i2) throws IOException {
            zzt(i, 0);
            zzgp(i2);
        }
    }

    public static final class zzb extends IOException {
        zzb() {
            super("CodedOutputStream was writing to a flat byte array and ran out of space.");
        }

        zzb(String str, Throwable th) {
            String valueOf = String.valueOf("CodedOutputStream was writing to a flat byte array and ran out of space.: ");
            String valueOf2 = String.valueOf(str);
            super(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf), th);
        }

        zzb(Throwable th) {
            super("CodedOutputStream was writing to a flat byte array and ran out of space.", th);
        }
    }

    private zzedw() {
    }

    public static zzedw zzat(byte[] bArr) {
        return new zza(bArr, 0, bArr.length);
    }

    public static int zzb(int i, zzedk zzedk) {
        int zzgr = zzgr(i);
        int size = zzedk.size();
        return zzgr + (size + zzgt(size));
    }

    public static int zzb(int i, zzeey zzeey) {
        int zzgr = zzgr(i);
        int zzbjo = zzeey.zzbjo();
        return zzgr + (zzbjo + zzgt(zzbjo));
    }

    private static int zzgr(int i) {
        return zzgt(i << 3);
    }

    private static int zzgs(int i) {
        return i >= 0 ? zzgt(i) : 10;
    }

    public static int zzgt(int i) {
        return (i & -128) == 0 ? 1 : (i & -16384) == 0 ? 2 : (-2097152 & i) == 0 ? 3 : (-268435456 & i) == 0 ? 4 : 5;
    }

    public static int zzm(int i, String str) {
        return zzgr(1) + zzrc(str);
    }

    private static int zzrc(String str) {
        int zzb;
        try {
            zzb = zzeft.zzb(str);
        } catch (zzefw e) {
            zzb = str.getBytes(zzeen.UTF_8).length;
        }
        return zzb + zzgt(zzb);
    }

    public static int zzv(int i, int i2) {
        return zzgr(i) + zzgs(i2);
    }

    public static int zzw(int i, int i2) {
        return zzgr(i) + zzgs(i2);
    }

    public abstract void write(byte[] bArr, int i, int i2) throws IOException;

    public abstract void zza(int i, zzedk zzedk) throws IOException;

    public abstract void zza(int i, zzeey zzeey) throws IOException;

    final void zza(String str, zzefw zzefw) throws IOException {
        logger.logp(Level.WARNING, "com.google.protobuf.CodedOutputStream", "inefficientWriteStringNoTag", "Converting ill-formed UTF-16. Your Protocol Buffer will not round trip correctly!", zzefw);
        byte[] bytes = str.getBytes(zzeen.UTF_8);
        try {
            zzgq(bytes.length);
            zzb(bytes, 0, bytes.length);
        } catch (Throwable e) {
            throw new zzb(e);
        } catch (zzb e2) {
            throw e2;
        }
    }

    public abstract void zzah(zzedk zzedk) throws IOException;

    public abstract int zzccc();

    public final void zzccd() {
        if (zzccc() != 0) {
            throw new IllegalStateException("Did not write as much data as expected.");
        }
    }

    public abstract void zzcn(long j) throws IOException;

    public abstract void zzd(zzeey zzeey) throws IOException;

    public abstract void zzgp(int i) throws IOException;

    public abstract void zzgq(int i) throws IOException;

    public abstract void zzl(int i, String str) throws IOException;

    public abstract void zzrb(String str) throws IOException;

    public abstract void zzt(int i, int i2) throws IOException;

    public abstract void zzu(int i, int i2) throws IOException;
}
