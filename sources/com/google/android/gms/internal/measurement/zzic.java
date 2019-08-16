package com.google.android.gms.internal.measurement;

import com.github.droidfu.support.DisplaySupport;
import java.nio.ByteBuffer;
import p018jp.colopl.util.ImageUtil;

final class zzic extends zzhz {
    zzic() {
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: CFG modification limit reached, blocks count: 150 */
    /* JADX WARNING: Code restructure failed: missing block: B:11:0x001e, code lost:
        if (r2 >= -32) goto L_0x002e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:12:0x0020, code lost:
        if (r3 >= r13) goto L_0x0079;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:14:0x0024, code lost:
        if (r2 < -62) goto L_0x002c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0026, code lost:
        r2 = r3 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:16:0x002a, code lost:
        if (r11[r3] <= -65) goto L_0x0016;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x002c, code lost:
        r0 = -1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x0030, code lost:
        if (r2 >= -16) goto L_0x0053;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:21:0x0034, code lost:
        if (r3 < (r13 - 1)) goto L_0x003b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x0036, code lost:
        r0 = com.google.android.gms.internal.measurement.zzhy.zzg(r11, r3, r13);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:23:0x003b, code lost:
        r4 = r3 + 1;
        r3 = r11[r3];
     */
    /* JADX WARNING: Code restructure failed: missing block: B:24:0x003f, code lost:
        if (r3 > -65) goto L_0x0051;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:25:0x0041, code lost:
        if (r2 != -32) goto L_0x0045;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x0043, code lost:
        if (r3 < -96) goto L_0x0051;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:0x0047, code lost:
        if (r2 != -19) goto L_0x004b;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:29:0x0049, code lost:
        if (r3 >= -96) goto L_0x0051;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:30:0x004b, code lost:
        r2 = r4 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:31:0x004f, code lost:
        if (r11[r4] <= -65) goto L_0x0016;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:32:0x0051, code lost:
        r0 = -1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:34:0x0055, code lost:
        if (r3 < (r13 - 2)) goto L_0x005c;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:35:0x0057, code lost:
        r0 = com.google.android.gms.internal.measurement.zzhy.zzg(r11, r3, r13);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:36:0x005c, code lost:
        r4 = r3 + 1;
        r3 = r11[r3];
     */
    /* JADX WARNING: Code restructure failed: missing block: B:37:0x0060, code lost:
        if (r3 > -65) goto L_0x0077;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:0x0069, code lost:
        if ((((r2 << 28) + (r3 + 112)) >> 30) != 0) goto L_0x0077;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:40:0x006b, code lost:
        r3 = r4 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:41:0x006f, code lost:
        if (r11[r4] > -65) goto L_0x0077;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:42:0x0071, code lost:
        r2 = r3 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:43:0x0075, code lost:
        if (r11[r3] <= -65) goto L_0x0016;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:44:0x0077, code lost:
        r0 = -1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:45:0x0079, code lost:
        r0 = r2;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final int zzb(int r10, byte[] r11, int r12, int r13) {
        /*
            r9 = this;
            r0 = 0
            r8 = -32
            r7 = -96
            r1 = -1
            r6 = -65
            r2 = r12
        L_0x0009:
            if (r2 >= r13) goto L_0x0012
            byte r3 = r11[r2]
            if (r3 < 0) goto L_0x0012
            int r2 = r2 + 1
            goto L_0x0009
        L_0x0012:
            if (r2 < r13) goto L_0x0016
        L_0x0014:
            return r0
        L_0x0015:
            r2 = r3
        L_0x0016:
            if (r2 >= r13) goto L_0x0014
            int r3 = r2 + 1
            byte r2 = r11[r2]
            if (r2 >= 0) goto L_0x0015
            if (r2 >= r8) goto L_0x002e
            if (r3 >= r13) goto L_0x0079
            r4 = -62
            if (r2 < r4) goto L_0x002c
            int r2 = r3 + 1
            byte r3 = r11[r3]
            if (r3 <= r6) goto L_0x0016
        L_0x002c:
            r0 = r1
            goto L_0x0014
        L_0x002e:
            r4 = -16
            if (r2 >= r4) goto L_0x0053
            int r4 = r13 + -1
            if (r3 < r4) goto L_0x003b
            int r0 = com.google.android.gms.internal.measurement.zzhy.zzg(r11, r3, r13)
            goto L_0x0014
        L_0x003b:
            int r4 = r3 + 1
            byte r3 = r11[r3]
            if (r3 > r6) goto L_0x0051
            if (r2 != r8) goto L_0x0045
            if (r3 < r7) goto L_0x0051
        L_0x0045:
            r5 = -19
            if (r2 != r5) goto L_0x004b
            if (r3 >= r7) goto L_0x0051
        L_0x004b:
            int r2 = r4 + 1
            byte r3 = r11[r4]
            if (r3 <= r6) goto L_0x0016
        L_0x0051:
            r0 = r1
            goto L_0x0014
        L_0x0053:
            int r4 = r13 + -2
            if (r3 < r4) goto L_0x005c
            int r0 = com.google.android.gms.internal.measurement.zzhy.zzg(r11, r3, r13)
            goto L_0x0014
        L_0x005c:
            int r4 = r3 + 1
            byte r3 = r11[r3]
            if (r3 > r6) goto L_0x0077
            int r2 = r2 << 28
            int r3 = r3 + 112
            int r2 = r2 + r3
            int r2 = r2 >> 30
            if (r2 != 0) goto L_0x0077
            int r3 = r4 + 1
            byte r2 = r11[r4]
            if (r2 > r6) goto L_0x0077
            int r2 = r3 + 1
            byte r3 = r11[r3]
            if (r3 <= r6) goto L_0x0016
        L_0x0077:
            r0 = r1
            goto L_0x0014
        L_0x0079:
            r0 = r2
            goto L_0x0014
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzic.zzb(int, byte[], int, int):int");
    }

    /* access modifiers changed from: 0000 */
    public final int zzb(CharSequence charSequence, byte[] bArr, int i, int i2) {
        int i3;
        int length = charSequence.length();
        int i4 = 0;
        int i5 = i + i2;
        while (i4 < length && i4 + i < i5) {
            char charAt = charSequence.charAt(i4);
            if (charAt >= 128) {
                break;
            }
            bArr[i + i4] = (byte) ((byte) charAt);
            i4++;
        }
        if (i4 == length) {
            return i + length;
        }
        int i6 = i + i4;
        while (i4 < length) {
            char charAt2 = charSequence.charAt(i4);
            if (charAt2 < 128 && i6 < i5) {
                i3 = i6 + 1;
                bArr[i6] = (byte) ((byte) charAt2);
            } else if (charAt2 < 2048 && i6 <= i5 - 2) {
                int i7 = i6 + 1;
                bArr[i6] = (byte) ((byte) ((charAt2 >>> 6) | 960));
                i3 = i7 + 1;
                bArr[i7] = (byte) ((byte) ((charAt2 & '?') | 128));
            } else if ((charAt2 < 55296 || 57343 < charAt2) && i6 <= i5 - 3) {
                int i8 = i6 + 1;
                bArr[i6] = (byte) ((byte) ((charAt2 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                int i9 = i8 + 1;
                bArr[i8] = (byte) ((byte) (((charAt2 >>> 6) & 63) | 128));
                i3 = i9 + 1;
                bArr[i9] = (byte) ((byte) ((charAt2 & '?') | 128));
            } else if (i6 <= i5 - 4) {
                if (i4 + 1 != charSequence.length()) {
                    i4++;
                    char charAt3 = charSequence.charAt(i4);
                    if (Character.isSurrogatePair(charAt2, charAt3)) {
                        int codePoint = Character.toCodePoint(charAt2, charAt3);
                        int i10 = i6 + 1;
                        bArr[i6] = (byte) ((byte) ((codePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                        int i11 = i10 + 1;
                        bArr[i10] = (byte) ((byte) (((codePoint >>> 12) & 63) | 128));
                        int i12 = i11 + 1;
                        bArr[i11] = (byte) ((byte) (((codePoint >>> 6) & 63) | 128));
                        i3 = i12 + 1;
                        bArr[i12] = (byte) ((byte) ((codePoint & 63) | 128));
                    }
                }
                throw new zzib(i4 - 1, length);
            } else if (55296 > charAt2 || charAt2 > 57343 || (i4 + 1 != charSequence.length() && Character.isSurrogatePair(charAt2, charSequence.charAt(i4 + 1)))) {
                throw new ArrayIndexOutOfBoundsException("Failed writing " + charAt2 + " at index " + i6);
            } else {
                throw new zzib(i4, length);
            }
            i4++;
            i6 = i3;
        }
        return i6;
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(CharSequence charSequence, ByteBuffer byteBuffer) {
        zzc(charSequence, byteBuffer);
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: CFG modification limit reached, blocks count: 145 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.lang.String zzh(byte[] r10, int r11, int r12) throws com.google.android.gms.internal.measurement.zzfi {
        /*
            r9 = this;
            r6 = 0
            r0 = r11 | r12
            int r1 = r10.length
            int r1 = r1 - r11
            int r1 = r1 - r12
            r0 = r0 | r1
            if (r0 >= 0) goto L_0x002d
            java.lang.ArrayIndexOutOfBoundsException r0 = new java.lang.ArrayIndexOutOfBoundsException
            java.lang.String r1 = "buffer length=%d, index=%d, size=%d"
            r2 = 3
            java.lang.Object[] r2 = new java.lang.Object[r2]
            int r3 = r10.length
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)
            r2[r6] = r3
            r3 = 1
            java.lang.Integer r4 = java.lang.Integer.valueOf(r11)
            r2[r3] = r4
            r3 = 2
            java.lang.Integer r4 = java.lang.Integer.valueOf(r12)
            r2[r3] = r4
            java.lang.String r1 = java.lang.String.format(r1, r2)
            r0.<init>(r1)
            throw r0
        L_0x002d:
            int r7 = r11 + r12
            char[] r4 = new char[r12]
            r1 = r11
            r5 = r6
        L_0x0033:
            if (r1 >= r7) goto L_0x00c2
            byte r0 = r10[r1]
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzd(r0)
            if (r2 == 0) goto L_0x00c2
            int r1 = r1 + 1
            com.google.android.gms.internal.measurement.zzia.zza(r0, r4, r5)
            int r5 = r5 + 1
            goto L_0x0033
        L_0x0045:
            byte r2 = r10[r1]
            com.google.android.gms.internal.measurement.zzia.zza(r0, r2, r4, r5)
            int r5 = r5 + 1
            int r1 = r1 + 1
            r2 = r1
        L_0x004f:
            if (r2 >= r7) goto L_0x00bc
            int r1 = r2 + 1
            byte r0 = r10[r2]
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzd(r0)
            if (r2 == 0) goto L_0x0072
            com.google.android.gms.internal.measurement.zzia.zza(r0, r4, r5)
            int r0 = r5 + 1
        L_0x0060:
            if (r1 >= r7) goto L_0x00b9
            byte r2 = r10[r1]
            boolean r3 = com.google.android.gms.internal.measurement.zzia.zzd(r2)
            if (r3 == 0) goto L_0x00b9
            com.google.android.gms.internal.measurement.zzia.zza(r2, r4, r0)
            int r0 = r0 + 1
            int r1 = r1 + 1
            goto L_0x0060
        L_0x0072:
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zze(r0)
            if (r2 == 0) goto L_0x007f
            if (r1 < r7) goto L_0x0045
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x007f:
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzf(r0)
            if (r2 == 0) goto L_0x009d
            int r2 = r7 + -1
            if (r1 < r2) goto L_0x008e
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x008e:
            int r2 = r1 + 1
            byte r1 = r10[r1]
            byte r3 = r10[r2]
            com.google.android.gms.internal.measurement.zzia.zza(r0, r1, r3, r4, r5)
            int r5 = r5 + 1
            int r1 = r2 + 1
            r2 = r1
            goto L_0x004f
        L_0x009d:
            int r2 = r7 + -2
            if (r1 < r2) goto L_0x00a6
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x00a6:
            int r2 = r1 + 1
            byte r1 = r10[r1]
            int r8 = r2 + 1
            byte r2 = r10[r2]
            byte r3 = r10[r8]
            com.google.android.gms.internal.measurement.zzia.zza(r0, r1, r2, r3, r4, r5)
            int r0 = r5 + 1
            int r0 = r0 + 1
            int r1 = r8 + 1
        L_0x00b9:
            r2 = r1
            r5 = r0
            goto L_0x004f
        L_0x00bc:
            java.lang.String r0 = new java.lang.String
            r0.<init>(r4, r6, r5)
            return r0
        L_0x00c2:
            r2 = r1
            goto L_0x004f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzic.zzh(byte[], int, int):java.lang.String");
    }
}
