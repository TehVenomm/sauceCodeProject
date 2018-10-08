package com.google.android.gms.internal;

import com.github.droidfu.support.DisplaySupport;
import jp.colopl.util.ImageUtil;

final class zzefx extends zzefu {
    zzefx() {
    }

    private static int zza(byte[] bArr, int i, long j, int i2) {
        switch (i2) {
            case 0:
                return zzeft.zzgy(i);
            case 1:
                return zzeft.zzx(i, zzefr.zzb(bArr, j));
            case 2:
                return zzeft.zzh(i, zzefr.zzb(bArr, j), zzefr.zzb(bArr, 1 + j));
            default:
                throw new AssertionError();
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static int zza(byte[] r9, long r10, int r12) {
        /*
        r0 = 16;
        if (r12 >= r0) goto L_0x001d;
    L_0x0004:
        r0 = 0;
    L_0x0005:
        r2 = (long) r0;
        r2 = r2 + r10;
        r0 = r12 - r0;
    L_0x0009:
        r1 = 0;
        r6 = r0;
    L_0x000b:
        if (r6 <= 0) goto L_0x00c2;
    L_0x000d:
        r0 = 1;
        r0 = r0 + r2;
        r2 = com.google.android.gms.internal.zzefr.zzb(r9, r2);
        if (r2 < 0) goto L_0x0031;
    L_0x0016:
        r3 = r6 + -1;
        r6 = r3;
        r8 = r2;
        r2 = r0;
        r1 = r8;
        goto L_0x000b;
    L_0x001d:
        r0 = 0;
        r2 = r10;
    L_0x001f:
        if (r0 >= r12) goto L_0x002f;
    L_0x0021:
        r1 = com.google.android.gms.internal.zzefr.zzb(r9, r2);
        if (r1 < 0) goto L_0x0005;
    L_0x0027:
        r4 = r0 + 1;
        r0 = 1;
        r0 = r0 + r2;
        r2 = r0;
        r0 = r4;
        goto L_0x001f;
    L_0x002f:
        r0 = r12;
        goto L_0x0005;
    L_0x0031:
        r4 = r0;
        r1 = r2;
    L_0x0033:
        if (r6 != 0) goto L_0x0037;
    L_0x0035:
        r0 = 0;
    L_0x0036:
        return r0;
    L_0x0037:
        r0 = r6 + -1;
        r2 = -32;
        if (r1 >= r2) goto L_0x0054;
    L_0x003d:
        if (r0 != 0) goto L_0x0041;
    L_0x003f:
        r0 = r1;
        goto L_0x0036;
    L_0x0041:
        r0 = r0 + -1;
        r2 = -62;
        if (r1 < r2) goto L_0x0052;
    L_0x0047:
        r2 = 1;
        r2 = r2 + r4;
        r1 = com.google.android.gms.internal.zzefr.zzb(r9, r4);
        r4 = -65;
        if (r1 <= r4) goto L_0x0009;
    L_0x0052:
        r0 = -1;
        goto L_0x0036;
    L_0x0054:
        r2 = -16;
        if (r1 >= r2) goto L_0x008b;
    L_0x0058:
        r2 = 2;
        if (r0 >= r2) goto L_0x0060;
    L_0x005b:
        r0 = zza(r9, r1, r4, r0);
        goto L_0x0036;
    L_0x0060:
        r0 = r0 + -2;
        r2 = 1;
        r6 = r4 + r2;
        r2 = com.google.android.gms.internal.zzefr.zzb(r9, r4);
        r3 = -65;
        if (r2 > r3) goto L_0x0089;
    L_0x006e:
        r3 = -32;
        if (r1 != r3) goto L_0x0076;
    L_0x0072:
        r3 = -96;
        if (r2 < r3) goto L_0x0089;
    L_0x0076:
        r3 = -19;
        if (r1 != r3) goto L_0x007e;
    L_0x007a:
        r1 = -96;
        if (r2 >= r1) goto L_0x0089;
    L_0x007e:
        r2 = 1;
        r2 = r2 + r6;
        r1 = com.google.android.gms.internal.zzefr.zzb(r9, r6);
        r4 = -65;
        if (r1 <= r4) goto L_0x0009;
    L_0x0089:
        r0 = -1;
        goto L_0x0036;
    L_0x008b:
        r2 = 3;
        if (r0 >= r2) goto L_0x0093;
    L_0x008e:
        r0 = zza(r9, r1, r4, r0);
        goto L_0x0036;
    L_0x0093:
        r0 = r0 + -3;
        r2 = 1;
        r2 = r2 + r4;
        r4 = com.google.android.gms.internal.zzefr.zzb(r9, r4);
        r5 = -65;
        if (r4 > r5) goto L_0x00bf;
    L_0x00a0:
        r1 = r1 << 28;
        r4 = r4 + 112;
        r1 = r1 + r4;
        r1 = r1 >> 30;
        if (r1 != 0) goto L_0x00bf;
    L_0x00a9:
        r4 = 1;
        r4 = r4 + r2;
        r1 = com.google.android.gms.internal.zzefr.zzb(r9, r2);
        r2 = -65;
        if (r1 > r2) goto L_0x00bf;
    L_0x00b4:
        r2 = 1;
        r2 = r2 + r4;
        r1 = com.google.android.gms.internal.zzefr.zzb(r9, r4);
        r4 = -65;
        if (r1 <= r4) goto L_0x0009;
    L_0x00bf:
        r0 = -1;
        goto L_0x0036;
    L_0x00c2:
        r4 = r2;
        goto L_0x0033;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzefx.zza(byte[], long, int):int");
    }

    final int zzb(int i, byte[] bArr, int i2, int i3) {
        if (((i2 | i3) | (bArr.length - i3)) < 0) {
            throw new ArrayIndexOutOfBoundsException(String.format("Array length=%d, index=%d, limit=%d", new Object[]{Integer.valueOf(bArr.length), Integer.valueOf(i2), Integer.valueOf(i3)}));
        }
        long j = (long) i2;
        return zza(bArr, j, (int) (((long) i3) - j));
    }

    final int zzb(CharSequence charSequence, byte[] bArr, int i, int i2) {
        long j = (long) i;
        long j2 = j + ((long) i2);
        int length = charSequence.length();
        if (length > i2 || bArr.length - i2 < i) {
            throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(length - 1) + " at index " + (i + i2));
        }
        int i3 = 0;
        while (i3 < length) {
            char charAt = charSequence.charAt(i3);
            if (charAt >= '') {
                break;
            }
            zzefr.zza(bArr, j, (byte) charAt);
            i3++;
            j++;
        }
        if (i3 == length) {
            return (int) j;
        }
        long j3 = j;
        while (i3 < length) {
            char charAt2 = charSequence.charAt(i3);
            if (charAt2 < '' && j3 < j2) {
                j = 1 + j3;
                zzefr.zza(bArr, j3, (byte) charAt2);
            } else if (charAt2 < 'ࠀ' && j3 <= j2 - 2) {
                r12 = j3 + 1;
                zzefr.zza(bArr, j3, (byte) ((charAt2 >>> 6) | 960));
                j = 1 + r12;
                zzefr.zza(bArr, r12, (byte) ((charAt2 & 63) | 128));
            } else if ((charAt2 < '?' || '?' < charAt2) && j3 <= j2 - 3) {
                j = 1 + j3;
                zzefr.zza(bArr, j3, (byte) ((charAt2 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                j3 = 1 + j;
                zzefr.zza(bArr, j, (byte) (((charAt2 >>> 6) & 63) | 128));
                j = 1 + j3;
                zzefr.zza(bArr, j3, (byte) ((charAt2 & 63) | 128));
            } else if (j3 <= j2 - 4) {
                if (i3 + 1 != length) {
                    i3++;
                    char charAt3 = charSequence.charAt(i3);
                    if (Character.isSurrogatePair(charAt2, charAt3)) {
                        int toCodePoint = Character.toCodePoint(charAt2, charAt3);
                        j = 1 + j3;
                        zzefr.zza(bArr, j3, (byte) ((toCodePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                        j3 = 1 + j;
                        zzefr.zza(bArr, j, (byte) (((toCodePoint >>> 12) & 63) | 128));
                        r12 = j3 + 1;
                        zzefr.zza(bArr, j3, (byte) (((toCodePoint >>> 6) & 63) | 128));
                        j = 1 + r12;
                        zzefr.zza(bArr, r12, (byte) ((toCodePoint & 63) | 128));
                    }
                }
                throw new zzefw(i3 - 1, length);
            } else if ('?' > charAt2 || charAt2 > '?' || (i3 + 1 != length && Character.isSurrogatePair(charAt2, charSequence.charAt(i3 + 1)))) {
                throw new ArrayIndexOutOfBoundsException("Failed writing " + charAt2 + " at index " + j3);
            } else {
                throw new zzefw(i3, length);
            }
            i3++;
            j3 = j;
        }
        return (int) j3;
    }
}
