package com.google.android.gms.internal.measurement;

import java.nio.ByteBuffer;

final class zzhy {
    private static final zzhz zzamz = ((!(zzhv.zzwt() && zzhv.zzwu()) || zzdi.zzrv()) ? new zzic() : new zzie());

    static int zza(CharSequence charSequence) {
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
                                throw new zzib(i3, length2);
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

    static int zza(CharSequence charSequence, byte[] bArr, int i, int i2) {
        return zzamz.zzb(charSequence, bArr, i, i2);
    }

    static void zza(CharSequence charSequence, ByteBuffer byteBuffer) {
        zzhz zzhz = zzamz;
        if (byteBuffer.hasArray()) {
            int arrayOffset = byteBuffer.arrayOffset();
            byteBuffer.position(zza(charSequence, byteBuffer.array(), byteBuffer.position() + arrayOffset, byteBuffer.remaining()) - arrayOffset);
        } else if (byteBuffer.isDirect()) {
            zzhz.zzb(charSequence, byteBuffer);
        } else {
            zzhz.zzc(charSequence, byteBuffer);
        }
    }

    /* access modifiers changed from: private */
    public static int zzc(int i, int i2, int i3) {
        if (i > -12 || i2 > -65 || i3 > -65) {
            return -1;
        }
        return ((i2 << 8) ^ i) ^ (i3 << 16);
    }

    /* access modifiers changed from: private */
    public static int zzch(int i) {
        if (i > -12) {
            return -1;
        }
        return i;
    }

    public static boolean zzf(byte[] bArr, int i, int i2) {
        return zzamz.zzf(bArr, i, i2);
    }

    /* access modifiers changed from: private */
    public static int zzg(byte[] bArr, int i, int i2) {
        byte b = bArr[i - 1];
        switch (i2 - i) {
            case 0:
                return zzch(b);
            case 1:
                return zzr(b, bArr[i]);
            case 2:
                return zzc(b, bArr[i], bArr[i + 1]);
            default:
                throw new AssertionError();
        }
    }

    static String zzh(byte[] bArr, int i, int i2) throws zzfi {
        return zzamz.zzh(bArr, i, i2);
    }

    public static boolean zzh(byte[] bArr) {
        return zzamz.zzf(bArr, 0, bArr.length);
    }

    /* access modifiers changed from: private */
    public static int zzr(int i, int i2) {
        if (i > -12 || i2 > -65) {
            return -1;
        }
        return (i2 << 8) ^ i;
    }
}
