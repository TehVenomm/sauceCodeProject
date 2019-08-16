package com.google.android.gms.internal.measurement;

import com.github.droidfu.support.DisplaySupport;
import com.google.android.gms.games.Notifications;
import java.io.IOException;
import java.nio.BufferOverflowException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.ReadOnlyBufferException;
import p018jp.colopl.util.ImageUtil;

public final class zzio {
    private final ByteBuffer zzaei;
    private zzee zzaol;
    private int zzaom;

    private zzio(ByteBuffer byteBuffer) {
        this.zzaei = byteBuffer;
        this.zzaei.order(ByteOrder.LITTLE_ENDIAN);
    }

    private zzio(byte[] bArr, int i, int i2) {
        this(ByteBuffer.wrap(bArr, 0, i2));
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

    public static int zzb(int i, zziw zziw) {
        int zzbi = zzbi(i);
        int zzuk = zziw.zzuk();
        return zzbi + zzuk + zzbq(zzuk);
    }

    public static int zzbi(int i) {
        return zzbq(i << 3);
    }

    public static int zzbj(int i) {
        if (i >= 0) {
            return zzbq(i);
        }
        return 10;
    }

    public static int zzbq(int i) {
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

    public static int zzc(int i, String str) {
        int zzbi = zzbi(i);
        int zza = zza(str);
        return zzbi + zza + zzbq(zza);
    }

    private final void zzcj(int i) throws IOException {
        byte b = (byte) i;
        if (!this.zzaei.hasRemaining()) {
            throw new zzin(this.zzaei.position(), this.zzaei.limit());
        }
        this.zzaei.put(b);
    }

    private static void zzd(CharSequence charSequence, ByteBuffer byteBuffer) {
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

    public static int zzg(int i, int i2) {
        return zzbi(i) + zzbj(i2);
    }

    public static zzio zzj(byte[] bArr) {
        return new zzio(bArr, 0, bArr.length);
    }

    public static zzio zzk(byte[] bArr, int i, int i2) {
        return new zzio(bArr, 0, i2);
    }

    public final void zza(int i, zziw zziw) throws IOException {
        zzb(i, 2);
        if (zziw.zzaow < 0) {
            zziw.zzuk();
        }
        zzck(zziw.zzaow);
        zziw.zza(this);
    }

    public final void zzb(int i, int i2) throws IOException {
        zzck((i << 3) | i2);
    }

    public final void zzb(int i, String str) throws IOException {
        zzb(i, 2);
        try {
            int zzbq = zzbq(str.length());
            if (zzbq == zzbq(str.length() * 3)) {
                int position = this.zzaei.position();
                if (this.zzaei.remaining() < zzbq) {
                    throw new zzin(zzbq + position, this.zzaei.limit());
                }
                this.zzaei.position(position + zzbq);
                zzd(str, this.zzaei);
                int position2 = this.zzaei.position();
                this.zzaei.position(position);
                zzck((position2 - position) - zzbq);
                this.zzaei.position(position2);
                return;
            }
            zzck(zza(str));
            zzd(str, this.zzaei);
        } catch (BufferOverflowException e) {
            zzin zzin = new zzin(this.zzaei.position(), this.zzaei.limit());
            zzin.initCause(e);
            throw zzin;
        }
    }

    public final void zzb(int i, boolean z) throws IOException {
        int i2 = 0;
        zzb(i, 0);
        if (z) {
            i2 = 1;
        }
        byte b = (byte) i2;
        if (!this.zzaei.hasRemaining()) {
            throw new zzin(this.zzaei.position(), this.zzaei.limit());
        }
        this.zzaei.put(b);
    }

    public final void zzbz(long j) throws IOException {
        while ((-128 & j) != 0) {
            zzcj((((int) j) & Notifications.NOTIFICATION_TYPES_ALL) | 128);
            j >>>= 7;
        }
        zzcj((int) j);
    }

    public final void zzc(int i, int i2) throws IOException {
        zzb(i, 0);
        if (i2 >= 0) {
            zzck(i2);
        } else {
            zzbz((long) i2);
        }
    }

    public final void zzck(int i) throws IOException {
        while ((i & -128) != 0) {
            zzcj((i & Notifications.NOTIFICATION_TYPES_ALL) | 128);
            i >>>= 7;
        }
        zzcj(i);
    }

    public final void zze(int i, zzgi zzgi) throws IOException {
        if (this.zzaol == null) {
            this.zzaol = zzee.zza(this.zzaei);
            this.zzaom = this.zzaei.position();
        } else if (this.zzaom != this.zzaei.position()) {
            this.zzaol.write(this.zzaei.array(), this.zzaom, this.zzaei.position() - this.zzaom);
            this.zzaom = this.zzaei.position();
        }
        zzee zzee = this.zzaol;
        zzee.zza(i, zzgi);
        zzee.flush();
        this.zzaom = this.zzaei.position();
    }

    public final void zzk(byte[] bArr) throws IOException {
        int length = bArr.length;
        if (this.zzaei.remaining() >= length) {
            this.zzaei.put(bArr, 0, length);
            return;
        }
        throw new zzin(this.zzaei.position(), this.zzaei.limit());
    }
}
