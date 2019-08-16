package com.google.android.gms.internal.drive;

import com.github.droidfu.support.DisplaySupport;
import com.google.android.gms.games.Notifications;
import java.io.IOException;
import java.nio.BufferOverflowException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.ReadOnlyBufferException;
import p018jp.colopl.util.ImageUtil;

public final class zzip {
    private final ByteBuffer zzmv;

    private zzip(ByteBuffer byteBuffer) {
        this.zzmv = byteBuffer;
        this.zzmv.order(ByteOrder.LITTLE_ENDIAN);
    }

    private zzip(byte[] bArr, int i, int i2) {
        this(ByteBuffer.wrap(bArr, i, i2));
    }

    private static int zza(CharSequence charSequence) {
        int i;
        int i2 = 0;
        int length = charSequence.length();
        int i3 = 0;
        while (i3 < length && charSequence.charAt(i3) < 128) {
            i3++;
        }
        int i4 = length;
        while (true) {
            if (i3 >= length) {
                i = i4;
                break;
            }
            char charAt = charSequence.charAt(i3);
            if (charAt < 2048) {
                i4 += (127 - charAt) >>> 31;
                i3++;
            } else {
                int length2 = charSequence.length();
                while (i3 < length2) {
                    char charAt2 = charSequence.charAt(i3);
                    if (charAt2 < 2048) {
                        i2 += (127 - charAt2) >>> 31;
                    } else {
                        i2 += 2;
                        if (55296 <= charAt2 && charAt2 <= 57343) {
                            if (Character.codePointAt(charSequence, i3) < 65536) {
                                throw new IllegalArgumentException("Unpaired surrogate at index " + i3);
                            }
                            i3++;
                        }
                    }
                    i3++;
                }
                i = i4 + i2;
            }
        }
        if (i >= length) {
            return i;
        }
        throw new IllegalArgumentException("UTF-8 length does not fit in int: " + (((long) i) + 4294967296L));
    }

    private final void zza(long j) throws IOException {
        while ((-128 & j) != 0) {
            zzn((((int) j) & Notifications.NOTIFICATION_TYPES_ALL) | 128);
            j >>>= 7;
        }
        zzn((int) j);
    }

    private static void zza(CharSequence charSequence, ByteBuffer byteBuffer) {
        int i;
        int i2;
        int i3 = 0;
        if (byteBuffer.isReadOnly()) {
            throw new ReadOnlyBufferException();
        } else if (byteBuffer.hasArray()) {
            try {
                byte[] array = byteBuffer.array();
                int arrayOffset = byteBuffer.arrayOffset() + byteBuffer.position();
                int remaining = byteBuffer.remaining();
                int length = charSequence.length();
                int i4 = arrayOffset + remaining;
                while (i3 < length && i3 + arrayOffset < i4) {
                    char charAt = charSequence.charAt(i3);
                    if (charAt >= 128) {
                        break;
                    }
                    array[arrayOffset + i3] = (byte) ((byte) charAt);
                    i3++;
                }
                if (i3 == length) {
                    i = arrayOffset + length;
                } else {
                    i = arrayOffset + i3;
                    while (i3 < length) {
                        char charAt2 = charSequence.charAt(i3);
                        if (charAt2 < 128 && i < i4) {
                            i2 = i + 1;
                            array[i] = (byte) ((byte) charAt2);
                        } else if (charAt2 < 2048 && i <= i4 - 2) {
                            int i5 = i + 1;
                            array[i] = (byte) ((byte) ((charAt2 >>> 6) | 960));
                            i2 = i5 + 1;
                            array[i5] = (byte) ((byte) ((charAt2 & '?') | 128));
                        } else if ((charAt2 < 55296 || 57343 < charAt2) && i <= i4 - 3) {
                            int i6 = i + 1;
                            array[i] = (byte) ((byte) ((charAt2 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                            int i7 = i6 + 1;
                            array[i6] = (byte) ((byte) (((charAt2 >>> 6) & 63) | 128));
                            i2 = i7 + 1;
                            array[i7] = (byte) ((byte) ((charAt2 & '?') | 128));
                        } else if (i <= i4 - 4) {
                            if (i3 + 1 != charSequence.length()) {
                                i3++;
                                char charAt3 = charSequence.charAt(i3);
                                if (Character.isSurrogatePair(charAt2, charAt3)) {
                                    int codePoint = Character.toCodePoint(charAt2, charAt3);
                                    int i8 = i + 1;
                                    array[i] = (byte) ((byte) ((codePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                                    int i9 = i8 + 1;
                                    array[i8] = (byte) ((byte) (((codePoint >>> 12) & 63) | 128));
                                    int i10 = i9 + 1;
                                    array[i9] = (byte) ((byte) (((codePoint >>> 6) & 63) | 128));
                                    i2 = i10 + 1;
                                    array[i10] = (byte) ((byte) ((codePoint & 63) | 128));
                                }
                            }
                            throw new IllegalArgumentException("Unpaired surrogate at index " + (i3 - 1));
                        } else {
                            throw new ArrayIndexOutOfBoundsException("Failed writing " + charAt2 + " at index " + i);
                        }
                        i3++;
                        i = i2;
                    }
                }
                byteBuffer.position(i - byteBuffer.arrayOffset());
            } catch (ArrayIndexOutOfBoundsException e) {
                BufferOverflowException bufferOverflowException = new BufferOverflowException();
                bufferOverflowException.initCause(e);
                throw bufferOverflowException;
            }
        } else {
            int length2 = charSequence.length();
            while (i3 < length2) {
                char charAt4 = charSequence.charAt(i3);
                if (charAt4 < 128) {
                    byteBuffer.put((byte) charAt4);
                } else if (charAt4 < 2048) {
                    byteBuffer.put((byte) ((charAt4 >>> 6) | 960));
                    byteBuffer.put((byte) ((charAt4 & '?') | 128));
                } else if (charAt4 < 55296 || 57343 < charAt4) {
                    byteBuffer.put((byte) ((charAt4 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                    byteBuffer.put((byte) (((charAt4 >>> 6) & 63) | 128));
                    byteBuffer.put((byte) ((charAt4 & '?') | 128));
                } else {
                    if (i3 + 1 != charSequence.length()) {
                        i3++;
                        char charAt5 = charSequence.charAt(i3);
                        if (Character.isSurrogatePair(charAt4, charAt5)) {
                            int codePoint2 = Character.toCodePoint(charAt4, charAt5);
                            byteBuffer.put((byte) ((codePoint2 >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                            byteBuffer.put((byte) (((codePoint2 >>> 12) & 63) | 128));
                            byteBuffer.put((byte) (((codePoint2 >>> 6) & 63) | 128));
                            byteBuffer.put((byte) ((codePoint2 & 63) | 128));
                        }
                    }
                    throw new IllegalArgumentException("Unpaired surrogate at index " + (i3 - 1));
                }
                i3++;
            }
        }
    }

    public static int zzb(int i, long j) {
        int zzo = zzo(i);
        long zzb = zzb(j);
        int i2 = (-128 & zzb) == 0 ? 1 : (-16384 & zzb) == 0 ? 2 : (-2097152 & zzb) == 0 ? 3 : (-268435456 & zzb) == 0 ? 4 : (-34359738368L & zzb) == 0 ? 5 : (-4398046511104L & zzb) == 0 ? 6 : (-562949953421312L & zzb) == 0 ? 7 : (-72057594037927936L & zzb) == 0 ? 8 : (zzb & Long.MIN_VALUE) == 0 ? 9 : 10;
        return i2 + zzo;
    }

    private static long zzb(long j) {
        return (j << 1) ^ (j >> 63);
    }

    public static zzip zzb(byte[] bArr) {
        return zzb(bArr, 0, bArr.length);
    }

    public static zzip zzb(byte[] bArr, int i, int i2) {
        return new zzip(bArr, 0, i2);
    }

    public static int zzc(int i, int i2) {
        return zzo(i) + zzm(i2);
    }

    public static int zzi(String str) {
        int zza = zza((CharSequence) str);
        return zza + zzq(zza);
    }

    public static int zzm(int i) {
        if (i >= 0) {
            return zzq(i);
        }
        return 10;
    }

    private final void zzn(int i) throws IOException {
        byte b = (byte) i;
        if (!this.zzmv.hasRemaining()) {
            throw new zziq(this.zzmv.position(), this.zzmv.limit());
        }
        this.zzmv.put(b);
    }

    public static int zzo(int i) {
        return zzq(i << 3);
    }

    public static int zzq(int i) {
        if ((i & -128) == 0) {
            return 1;
        }
        if ((i & -16384) == 0) {
            return 2;
        }
        if ((-2097152 & i) == 0) {
            return 3;
        }
        return (-268435456 & i) == 0 ? 4 : 5;
    }

    public final void zza(int i, long j) throws IOException {
        zzd(i, 0);
        zza(zzb(j));
    }

    public final void zzb(int i, int i2) throws IOException {
        zzd(i, 0);
        if (i2 >= 0) {
            zzp(i2);
        } else {
            zza((long) i2);
        }
    }

    public final void zzbh() {
        if (this.zzmv.remaining() != 0) {
            throw new IllegalStateException(String.format("Did not write as much data as expected, %s bytes remaining.", new Object[]{Integer.valueOf(this.zzmv.remaining())}));
        }
    }

    public final void zzc(byte[] bArr) throws IOException {
        int length = bArr.length;
        if (this.zzmv.remaining() >= length) {
            this.zzmv.put(bArr, 0, length);
            return;
        }
        throw new zziq(this.zzmv.position(), this.zzmv.limit());
    }

    public final void zzd(int i, int i2) throws IOException {
        zzp((i << 3) | i2);
    }

    public final void zzh(String str) throws IOException {
        try {
            int zzq = zzq(str.length());
            if (zzq == zzq(str.length() * 3)) {
                int position = this.zzmv.position();
                if (this.zzmv.remaining() < zzq) {
                    throw new zziq(zzq + position, this.zzmv.limit());
                }
                this.zzmv.position(position + zzq);
                zza((CharSequence) str, this.zzmv);
                int position2 = this.zzmv.position();
                this.zzmv.position(position);
                zzp((position2 - position) - zzq);
                this.zzmv.position(position2);
                return;
            }
            zzp(zza((CharSequence) str));
            zza((CharSequence) str, this.zzmv);
        } catch (BufferOverflowException e) {
            zziq zziq = new zziq(this.zzmv.position(), this.zzmv.limit());
            zziq.initCause(e);
            throw zziq;
        }
    }

    public final void zzp(int i) throws IOException {
        while ((i & -128) != 0) {
            zzn((i & Notifications.NOTIFICATION_TYPES_ALL) | 128);
            i >>>= 7;
        }
        zzn(i);
    }
}
