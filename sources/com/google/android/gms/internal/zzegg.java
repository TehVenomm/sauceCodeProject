package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import com.github.droidfu.support.DisplaySupport;
import java.io.IOException;
import java.nio.BufferOverflowException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.ReadOnlyBufferException;
import jp.colopl.util.ImageUtil;

public final class zzegg {
    private final ByteBuffer zznct;

    private zzegg(ByteBuffer byteBuffer) {
        this.zznct = byteBuffer;
        this.zznct.order(ByteOrder.LITTLE_ENDIAN);
    }

    private zzegg(byte[] bArr, int i, int i2) {
        this(ByteBuffer.wrap(bArr, i, i2));
    }

    private static int zza(CharSequence charSequence, byte[] bArr, int i, int i2) {
        int length = charSequence.length();
        int i3 = 0;
        int i4 = i + i2;
        while (i3 < length && i3 + i < i4) {
            char charAt = charSequence.charAt(i3);
            if (charAt >= '') {
                break;
            }
            bArr[i + i3] = (byte) ((byte) charAt);
            i3++;
        }
        if (i3 == length) {
            return i + length;
        }
        int i5 = i + i3;
        while (i3 < length) {
            int i6;
            char charAt2 = charSequence.charAt(i3);
            int i7;
            if (charAt2 < '' && i5 < i4) {
                i7 = i5 + 1;
                bArr[i5] = (byte) ((byte) charAt2);
                i5 = i3;
                i3 = i7;
            } else if (charAt2 < 'ࠀ' && i5 <= i4 - 2) {
                i7 = i5 + 1;
                bArr[i5] = (byte) ((byte) ((charAt2 >>> 6) | 960));
                i5 = i7 + 1;
                bArr[i7] = (byte) ((byte) ((charAt2 & 63) | 128));
                i6 = i3;
                i3 = i5;
                i5 = i6;
            } else if ((charAt2 < '?' || '?' < charAt2) && i5 <= i4 - 3) {
                i7 = i5 + 1;
                bArr[i5] = (byte) ((byte) ((charAt2 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                int i8 = i7 + 1;
                bArr[i7] = (byte) ((byte) (((charAt2 >>> 6) & 63) | 128));
                i5 = i8 + 1;
                bArr[i8] = (byte) ((byte) ((charAt2 & 63) | 128));
                i6 = i3;
                i3 = i5;
                i5 = i6;
            } else if (i5 <= i4 - 4) {
                if (i3 + 1 != charSequence.length()) {
                    i3++;
                    char charAt3 = charSequence.charAt(i3);
                    if (Character.isSurrogatePair(charAt2, charAt3)) {
                        i7 = Character.toCodePoint(charAt2, charAt3);
                        int i9 = i5 + 1;
                        bArr[i5] = (byte) ((byte) ((i7 >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                        i5 = i9 + 1;
                        bArr[i9] = (byte) ((byte) (((i7 >>> 12) & 63) | 128));
                        i9 = i5 + 1;
                        bArr[i5] = (byte) ((byte) (((i7 >>> 6) & 63) | 128));
                        i5 = i9 + 1;
                        bArr[i9] = (byte) ((byte) ((i7 & 63) | 128));
                        i6 = i3;
                        i3 = i5;
                        i5 = i6;
                    }
                }
                throw new IllegalArgumentException("Unpaired surrogate at index " + (i3 - 1));
            } else {
                throw new ArrayIndexOutOfBoundsException("Failed writing " + charAt2 + " at index " + i5);
            }
            i6 = i5 + 1;
            i5 = i3;
            i3 = i6;
        }
        return i5;
    }

    private static void zza(CharSequence charSequence, ByteBuffer byteBuffer) {
        if (byteBuffer.isReadOnly()) {
            throw new ReadOnlyBufferException();
        } else if (byteBuffer.hasArray()) {
            try {
                byteBuffer.position(zza(charSequence, byteBuffer.array(), byteBuffer.arrayOffset() + byteBuffer.position(), byteBuffer.remaining()) - byteBuffer.arrayOffset());
            } catch (Throwable e) {
                BufferOverflowException bufferOverflowException = new BufferOverflowException();
                bufferOverflowException.initCause(e);
                throw bufferOverflowException;
            }
        } else {
            zzb(charSequence, byteBuffer);
        }
    }

    public static zzegg zzav(byte[] bArr) {
        return zzi(bArr, 0, bArr.length);
    }

    public static int zzaw(byte[] bArr) {
        return zzhd(bArr.length) + bArr.length;
    }

    public static int zzb(int i, zzego zzego) {
        int zzgr = zzgr(i);
        int zzbjo = zzego.zzbjo();
        return zzgr + (zzbjo + zzhd(zzbjo));
    }

    private static int zzb(CharSequence charSequence) {
        int i = 0;
        int length = charSequence.length();
        int i2 = 0;
        while (i2 < length && charSequence.charAt(i2) < '') {
            i2++;
        }
        int i3 = length;
        while (i2 < length) {
            char charAt = charSequence.charAt(i2);
            if (charAt < 'ࠀ') {
                i3 += (127 - charAt) >>> 31;
                i2++;
            } else {
                int length2 = charSequence.length();
                while (i2 < length2) {
                    char charAt2 = charSequence.charAt(i2);
                    if (charAt2 < 'ࠀ') {
                        i += (127 - charAt2) >>> 31;
                    } else {
                        i += 2;
                        if ('?' <= charAt2 && charAt2 <= '?') {
                            if (Character.codePointAt(charSequence, i2) < 65536) {
                                throw new IllegalArgumentException("Unpaired surrogate at index " + i2);
                            }
                            i2++;
                        }
                    }
                    i2++;
                }
                i2 = i3 + i;
                if (i2 < length) {
                    return i2;
                }
                throw new IllegalArgumentException("UTF-8 length does not fit in int: " + (((long) i2) + 4294967296L));
            }
        }
        i2 = i3;
        if (i2 < length) {
            return i2;
        }
        throw new IllegalArgumentException("UTF-8 length does not fit in int: " + (((long) i2) + 4294967296L));
    }

    private static void zzb(CharSequence charSequence, ByteBuffer byteBuffer) {
        int length = charSequence.length();
        int i = 0;
        while (i < length) {
            char charAt = charSequence.charAt(i);
            if (charAt < '') {
                byteBuffer.put((byte) charAt);
            } else if (charAt < 'ࠀ') {
                byteBuffer.put((byte) ((charAt >>> 6) | 960));
                byteBuffer.put((byte) ((charAt & 63) | 128));
            } else if (charAt < '?' || '?' < charAt) {
                byteBuffer.put((byte) ((charAt >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                byteBuffer.put((byte) (((charAt >>> 6) & 63) | 128));
                byteBuffer.put((byte) ((charAt & 63) | 128));
            } else {
                if (i + 1 != charSequence.length()) {
                    i++;
                    char charAt2 = charSequence.charAt(i);
                    if (Character.isSurrogatePair(charAt, charAt2)) {
                        int toCodePoint = Character.toCodePoint(charAt, charAt2);
                        byteBuffer.put((byte) ((toCodePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                        byteBuffer.put((byte) (((toCodePoint >>> 12) & 63) | 128));
                        byteBuffer.put((byte) (((toCodePoint >>> 6) & 63) | 128));
                        byteBuffer.put((byte) ((toCodePoint & 63) | 128));
                    }
                }
                throw new IllegalArgumentException("Unpaired surrogate at index " + (i - 1));
            }
            i++;
        }
    }

    private final void zzco(long j) throws IOException {
        while ((-128 & j) != 0) {
            zzhb((((int) j) & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128);
            j >>>= 7;
        }
        zzhb((int) j);
    }

    public static int zzcp(long j) {
        return (-128 & j) == 0 ? 1 : (-16384 & j) == 0 ? 2 : (-2097152 & j) == 0 ? 3 : (-268435456 & j) == 0 ? 4 : (-34359738368L & j) == 0 ? 5 : (-4398046511104L & j) == 0 ? 6 : (-562949953421312L & j) == 0 ? 7 : (-72057594037927936L & j) == 0 ? 8 : (Long.MIN_VALUE & j) == 0 ? 9 : 10;
    }

    private final void zzcq(long j) throws IOException {
        if (this.zznct.remaining() < 8) {
            throw new zzegh(this.zznct.position(), this.zznct.limit());
        }
        this.zznct.putLong(j);
    }

    private static long zzcr(long j) {
        return (j << 1) ^ (j >> 63);
    }

    public static int zzd(int i, byte[] bArr) {
        return zzgr(i) + zzaw(bArr);
    }

    public static int zze(int i, long j) {
        return zzgr(i) + zzcp(j);
    }

    public static int zzf(int i, long j) {
        return zzgr(i) + zzcp(zzcr(j));
    }

    public static int zzgr(int i) {
        return zzhd(i << 3);
    }

    public static int zzgs(int i) {
        return i >= 0 ? zzhd(i) : 10;
    }

    private final void zzhb(int i) throws IOException {
        byte b = (byte) i;
        if (this.zznct.hasRemaining()) {
            this.zznct.put(b);
            return;
        }
        throw new zzegh(this.zznct.position(), this.zznct.limit());
    }

    public static int zzhd(int i) {
        return (i & -128) == 0 ? 1 : (i & -16384) == 0 ? 2 : (-2097152 & i) == 0 ? 3 : (-268435456 & i) == 0 ? 4 : 5;
    }

    public static int zzhe(int i) {
        return (i << 1) ^ (i >> 31);
    }

    public static zzegg zzi(byte[] bArr, int i, int i2) {
        return new zzegg(bArr, 0, i2);
    }

    public static int zzm(int i, String str) {
        return zzgr(i) + zzrc(str);
    }

    public static int zzrc(String str) {
        int zzb = zzb((CharSequence) str);
        return zzb + zzhd(zzb);
    }

    public static int zzv(int i, int i2) {
        return zzgr(i) + zzgs(i2);
    }

    public final void zza(int i, double d) throws IOException {
        zzt(i, 1);
        zzcq(Double.doubleToLongBits(d));
    }

    public final void zza(int i, long j) throws IOException {
        zzt(i, 0);
        zzco(j);
    }

    public final void zza(int i, zzego zzego) throws IOException {
        zzt(i, 2);
        zzb(zzego);
    }

    public final void zzax(byte[] bArr) throws IOException {
        int length = bArr.length;
        if (this.zznct.remaining() >= length) {
            this.zznct.put(bArr, 0, length);
            return;
        }
        throw new zzegh(this.zznct.position(), this.zznct.limit());
    }

    public final void zzb(int i, long j) throws IOException {
        zzt(i, 0);
        zzco(j);
    }

    public final void zzb(zzego zzego) throws IOException {
        zzhc(zzego.zzcef());
        zzego.zza(this);
    }

    public final void zzc(int i, float f) throws IOException {
        zzt(i, 5);
        int floatToIntBits = Float.floatToIntBits(f);
        if (this.zznct.remaining() < 4) {
            throw new zzegh(this.zznct.position(), this.zznct.limit());
        }
        this.zznct.putInt(floatToIntBits);
    }

    public final void zzc(int i, long j) throws IOException {
        zzt(i, 1);
        zzcq(j);
    }

    public final void zzc(int i, byte[] bArr) throws IOException {
        zzt(i, 2);
        zzhc(bArr.length);
        zzax(bArr);
    }

    public final void zzccd() {
        if (this.zznct.remaining() != 0) {
            throw new IllegalStateException(String.format("Did not write as much data as expected, %s bytes remaining.", new Object[]{Integer.valueOf(this.zznct.remaining())}));
        }
    }

    public final void zzd(int i, long j) throws IOException {
        zzt(i, 0);
        zzco(zzcr(j));
    }

    public final void zzhc(int i) throws IOException {
        while ((i & -128) != 0) {
            zzhb((i & TransportMediator.KEYCODE_MEDIA_PAUSE) | 128);
            i >>>= 7;
        }
        zzhb(i);
    }

    public final void zzl(int i, String str) throws IOException {
        zzt(i, 2);
        try {
            int zzhd = zzhd(str.length());
            if (zzhd == zzhd(str.length() * 3)) {
                int position = this.zznct.position();
                if (this.zznct.remaining() < zzhd) {
                    throw new zzegh(zzhd + position, this.zznct.limit());
                }
                this.zznct.position(position + zzhd);
                zza((CharSequence) str, this.zznct);
                int position2 = this.zznct.position();
                this.zznct.position(position);
                zzhc((position2 - position) - zzhd);
                this.zznct.position(position2);
                return;
            }
            zzhc(zzb((CharSequence) str));
            zza((CharSequence) str, this.zznct);
        } catch (Throwable e) {
            zzegh zzegh = new zzegh(this.zznct.position(), this.zznct.limit());
            zzegh.initCause(e);
            throw zzegh;
        }
    }

    public final void zzl(int i, boolean z) throws IOException {
        int i2 = 0;
        zzt(i, 0);
        if (z) {
            i2 = 1;
        }
        byte b = (byte) i2;
        if (this.zznct.hasRemaining()) {
            this.zznct.put(b);
            return;
        }
        throw new zzegh(this.zznct.position(), this.zznct.limit());
    }

    public final void zzt(int i, int i2) throws IOException {
        zzhc((i << 3) | i2);
    }

    public final void zzu(int i, int i2) throws IOException {
        zzt(i, 0);
        if (i2 >= 0) {
            zzhc(i2);
        } else {
            zzco((long) i2);
        }
    }
}
