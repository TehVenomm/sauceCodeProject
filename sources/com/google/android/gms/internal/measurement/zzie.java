package com.google.android.gms.internal.measurement;

import com.github.droidfu.support.DisplaySupport;
import p018jp.colopl.util.ImageUtil;

final class zzie extends zzhz {
    zzie() {
    }

    private static int zza(byte[] bArr, int i, long j, int i2) {
        switch (i2) {
            case 0:
                return zzhy.zzch(i);
            case 1:
                return zzhy.zzr(i, zzhv.zza(bArr, j));
            case 2:
                return zzhy.zzc(i, zzhv.zza(bArr, j), zzhv.zza(bArr, 1 + j));
            default:
                throw new AssertionError();
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:86:?, code lost:
        return -1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:88:?, code lost:
        return -1;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final int zzb(int r11, byte[] r12, int r13, int r14) {
        /*
            r10 = this;
            r0 = r13 | r14
            int r1 = r12.length
            int r1 = r1 - r14
            r0 = r0 | r1
            if (r0 >= 0) goto L_0x002c
            java.lang.ArrayIndexOutOfBoundsException r0 = new java.lang.ArrayIndexOutOfBoundsException
            java.lang.String r1 = "Array length=%d, index=%d, limit=%d"
            r2 = 3
            java.lang.Object[] r2 = new java.lang.Object[r2]
            r3 = 0
            int r4 = r12.length
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)
            r2[r3] = r4
            r3 = 1
            java.lang.Integer r4 = java.lang.Integer.valueOf(r13)
            r2[r3] = r4
            r3 = 2
            java.lang.Integer r4 = java.lang.Integer.valueOf(r14)
            r2[r3] = r4
            java.lang.String r1 = java.lang.String.format(r1, r2)
            r0.<init>(r1)
            throw r0
        L_0x002c:
            long r4 = (long) r13
            long r0 = (long) r14
            long r0 = r0 - r4
            int r1 = (int) r0
            r0 = 16
            if (r1 >= r0) goto L_0x004f
            r0 = 0
        L_0x0035:
            long r2 = (long) r0
            long r2 = r2 + r4
            int r4 = r1 - r0
            r0 = r2
        L_0x003a:
            r8 = 0
            r6 = r0
            r9 = r4
        L_0x003d:
            if (r9 <= 0) goto L_0x00f2
            r0 = 1
            long r2 = r0 + r6
            byte r0 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r6)
            if (r0 < 0) goto L_0x0061
            int r4 = r9 + -1
            r6 = r2
            r9 = r4
            r8 = r0
            goto L_0x003d
        L_0x004f:
            r0 = 0
            r2 = r4
        L_0x0051:
            if (r0 >= r1) goto L_0x005f
            byte r6 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r2)
            if (r6 < 0) goto L_0x0035
            int r0 = r0 + 1
            r6 = 1
            long r2 = r2 + r6
            goto L_0x0051
        L_0x005f:
            r0 = r1
            goto L_0x0035
        L_0x0061:
            r4 = r2
        L_0x0062:
            if (r9 != 0) goto L_0x0066
            r0 = 0
        L_0x0065:
            return r0
        L_0x0066:
            int r1 = r9 + -1
            r2 = -32
            if (r0 >= r2) goto L_0x0081
            if (r1 == 0) goto L_0x0065
            int r2 = r1 + -1
            r1 = -62
            if (r0 < r1) goto L_0x007f
            r0 = 1
            long r0 = r0 + r4
            byte r3 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r4)
            r4 = -65
            if (r3 <= r4) goto L_0x00ef
        L_0x007f:
            r0 = -1
            goto L_0x0065
        L_0x0081:
            r2 = -16
            if (r0 >= r2) goto L_0x00b7
            r2 = 2
            if (r1 >= r2) goto L_0x008d
            int r0 = zza(r12, r0, r4, r1)
            goto L_0x0065
        L_0x008d:
            int r2 = r1 + -2
            r6 = 1
            long r6 = r6 + r4
            byte r1 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r4)
            r3 = -65
            if (r1 > r3) goto L_0x00b5
            r3 = -32
            if (r0 != r3) goto L_0x00a2
            r3 = -96
            if (r1 < r3) goto L_0x00b5
        L_0x00a2:
            r3 = -19
            if (r0 != r3) goto L_0x00aa
            r0 = -96
            if (r1 >= r0) goto L_0x00b5
        L_0x00aa:
            r0 = 1
            long r0 = r0 + r6
            byte r3 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r6)
            r4 = -65
            if (r3 <= r4) goto L_0x00ef
        L_0x00b5:
            r0 = -1
            goto L_0x0065
        L_0x00b7:
            r2 = 3
            if (r1 >= r2) goto L_0x00bf
            int r0 = zza(r12, r0, r4, r1)
            goto L_0x0065
        L_0x00bf:
            int r2 = r1 + -3
            r6 = 1
            long r6 = r6 + r4
            byte r1 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r4)
            r3 = -65
            if (r1 > r3) goto L_0x00ec
            int r0 = r0 << 28
            int r1 = r1 + 112
            int r0 = r0 + r1
            int r0 = r0 >> 30
            if (r0 != 0) goto L_0x00ec
            r0 = 1
            long r4 = r0 + r6
            byte r0 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r6)
            r1 = -65
            if (r0 > r1) goto L_0x00ec
            r0 = 1
            long r0 = r0 + r4
            byte r3 = com.google.android.gms.internal.measurement.zzhv.zza(r12, r4)
            r4 = -65
            if (r3 <= r4) goto L_0x00ef
        L_0x00ec:
            r0 = -1
            goto L_0x0065
        L_0x00ef:
            r4 = r2
            goto L_0x003a
        L_0x00f2:
            r4 = r6
            r0 = r8
            goto L_0x0062
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzie.zzb(int, byte[], int, int):int");
    }

    /* access modifiers changed from: 0000 */
    public final int zzb(CharSequence charSequence, byte[] bArr, int i, int i2) {
        long j;
        long j2 = (long) i;
        long j3 = j2 + ((long) i2);
        int length = charSequence.length();
        if (length > i2 || bArr.length - i2 < i) {
            throw new ArrayIndexOutOfBoundsException("Failed writing " + charSequence.charAt(length - 1) + " at index " + (i + i2));
        }
        int i3 = 0;
        while (i3 < length) {
            char charAt = charSequence.charAt(i3);
            if (charAt >= 128) {
                break;
            }
            zzhv.zza(bArr, j2, (byte) charAt);
            i3++;
            j2++;
        }
        if (i3 == length) {
            return (int) j2;
        }
        long j4 = j2;
        while (i3 < length) {
            char charAt2 = charSequence.charAt(i3);
            if (charAt2 < 128 && j4 < j3) {
                j = 1 + j4;
                zzhv.zza(bArr, j4, (byte) charAt2);
            } else if (charAt2 < 2048 && j4 <= j3 - 2) {
                long j5 = j4 + 1;
                zzhv.zza(bArr, j4, (byte) ((charAt2 >>> 6) | 960));
                j = 1 + j5;
                zzhv.zza(bArr, j5, (byte) ((charAt2 & '?') | 128));
            } else if ((charAt2 < 55296 || 57343 < charAt2) && j4 <= j3 - 3) {
                long j6 = 1 + j4;
                zzhv.zza(bArr, j4, (byte) ((charAt2 >>> 12) | ImageUtil.THUMBNAIL_MAX_SIZE));
                long j7 = 1 + j6;
                zzhv.zza(bArr, j6, (byte) (((charAt2 >>> 6) & 63) | 128));
                j = 1 + j7;
                zzhv.zza(bArr, j7, (byte) ((charAt2 & '?') | 128));
            } else if (j4 <= j3 - 4) {
                if (i3 + 1 != length) {
                    i3++;
                    char charAt3 = charSequence.charAt(i3);
                    if (Character.isSurrogatePair(charAt2, charAt3)) {
                        int codePoint = Character.toCodePoint(charAt2, charAt3);
                        long j8 = 1 + j4;
                        zzhv.zza(bArr, j4, (byte) ((codePoint >>> 18) | DisplaySupport.SCREEN_DENSITY_HIGH));
                        long j9 = 1 + j8;
                        zzhv.zza(bArr, j8, (byte) (((codePoint >>> 12) & 63) | 128));
                        long j10 = j9 + 1;
                        zzhv.zza(bArr, j9, (byte) (((codePoint >>> 6) & 63) | 128));
                        j = 1 + j10;
                        zzhv.zza(bArr, j10, (byte) ((codePoint & 63) | 128));
                    }
                }
                throw new zzib(i3 - 1, length);
            } else if (55296 > charAt2 || charAt2 > 57343 || (i3 + 1 != length && Character.isSurrogatePair(charAt2, charSequence.charAt(i3 + 1)))) {
                throw new ArrayIndexOutOfBoundsException("Failed writing " + charAt2 + " at index " + j4);
            } else {
                throw new zzib(i3, length);
            }
            i3++;
            j4 = j;
        }
        return (int) j4;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:46:0x0156, code lost:
        if (java.lang.Character.isSurrogatePair(r12, r17.charAt(r4 + 1)) == false) goto L_0x0158;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zzb(java.lang.CharSequence r17, java.nio.ByteBuffer r18) {
        /*
            r16 = this;
            long r8 = com.google.android.gms.internal.measurement.zzhv.zzb(r18)
            int r2 = r18.position()
            long r2 = (long) r2
            long r2 = r2 + r8
            int r4 = r18.limit()
            long r4 = (long) r4
            long r10 = r8 + r4
            int r5 = r17.length()
            long r6 = (long) r5
            long r12 = r10 - r2
            int r4 = (r6 > r12 ? 1 : (r6 == r12 ? 0 : -1))
            if (r4 <= 0) goto L_0x004d
            int r2 = r5 + -1
            r0 = r17
            char r2 = r0.charAt(r2)
            int r3 = r18.limit()
            java.lang.ArrayIndexOutOfBoundsException r4 = new java.lang.ArrayIndexOutOfBoundsException
            java.lang.StringBuilder r5 = new java.lang.StringBuilder
            r6 = 37
            r5.<init>(r6)
            java.lang.String r6 = "Failed writing "
            java.lang.StringBuilder r5 = r5.append(r6)
            java.lang.StringBuilder r2 = r5.append(r2)
            java.lang.String r5 = " at index "
            java.lang.StringBuilder r2 = r2.append(r5)
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            r4.<init>(r2)
            throw r4
        L_0x004d:
            r4 = 0
        L_0x004e:
            if (r4 >= r5) goto L_0x0064
            r0 = r17
            char r6 = r0.charAt(r4)
            r7 = 128(0x80, float:1.794E-43)
            if (r6 >= r7) goto L_0x0064
            byte r6 = (byte) r6
            com.google.android.gms.internal.measurement.zzhv.zza(r2, r6)
            int r4 = r4 + 1
            r6 = 1
            long r2 = r2 + r6
            goto L_0x004e
        L_0x0064:
            if (r4 != r5) goto L_0x018d
            long r2 = r2 - r8
            int r2 = (int) r2
            r0 = r18
            r0.position(r2)
        L_0x006d:
            return
        L_0x006e:
            if (r4 >= r5) goto L_0x0183
            r0 = r17
            char r12 = r0.charAt(r4)
            r2 = 128(0x80, float:1.794E-43)
            if (r12 >= r2) goto L_0x0089
            int r2 = (r6 > r10 ? 1 : (r6 == r10 ? 0 : -1))
            if (r2 >= 0) goto L_0x0089
            r2 = 1
            long r2 = r2 + r6
            byte r12 = (byte) r12
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r12)
        L_0x0085:
            int r4 = r4 + 1
            r6 = r2
            goto L_0x006e
        L_0x0089:
            r2 = 2048(0x800, float:2.87E-42)
            if (r12 >= r2) goto L_0x00ad
            r2 = 2
            long r2 = r10 - r2
            int r2 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r2 > 0) goto L_0x00ad
            r2 = 1
            long r14 = r6 + r2
            int r2 = r12 >>> 6
            r2 = r2 | 960(0x3c0, float:1.345E-42)
            byte r2 = (byte) r2
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r2)
            r2 = 1
            long r2 = r2 + r14
            r6 = r12 & 63
            r6 = r6 | 128(0x80, float:1.794E-43)
            byte r6 = (byte) r6
            com.google.android.gms.internal.measurement.zzhv.zza(r14, r6)
            goto L_0x0085
        L_0x00ad:
            r2 = 55296(0xd800, float:7.7486E-41)
            if (r12 < r2) goto L_0x00b7
            r2 = 57343(0xdfff, float:8.0355E-41)
            if (r2 >= r12) goto L_0x00e3
        L_0x00b7:
            r2 = 3
            long r2 = r10 - r2
            int r2 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r2 > 0) goto L_0x00e3
            r2 = 1
            long r2 = r2 + r6
            int r13 = r12 >>> 12
            r13 = r13 | 480(0x1e0, float:6.73E-43)
            byte r13 = (byte) r13
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r13)
            r6 = 1
            long r6 = r6 + r2
            int r13 = r12 >>> 6
            r13 = r13 & 63
            r13 = r13 | 128(0x80, float:1.794E-43)
            byte r13 = (byte) r13
            com.google.android.gms.internal.measurement.zzhv.zza(r2, r13)
            r2 = 1
            long r2 = r2 + r6
            r12 = r12 & 63
            r12 = r12 | 128(0x80, float:1.794E-43)
            byte r12 = (byte) r12
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r12)
            goto L_0x0085
        L_0x00e3:
            r2 = 4
            long r2 = r10 - r2
            int r2 = (r6 > r2 ? 1 : (r6 == r2 ? 0 : -1))
            if (r2 > 0) goto L_0x013c
            int r2 = r4 + 1
            if (r2 == r5) goto L_0x00fd
            int r4 = r4 + 1
            r0 = r17
            char r2 = r0.charAt(r4)
            boolean r3 = java.lang.Character.isSurrogatePair(r12, r2)
            if (r3 != 0) goto L_0x0105
        L_0x00fd:
            com.google.android.gms.internal.measurement.zzib r2 = new com.google.android.gms.internal.measurement.zzib
            int r3 = r4 + -1
            r2.<init>(r3, r5)
            throw r2
        L_0x0105:
            int r12 = java.lang.Character.toCodePoint(r12, r2)
            r2 = 1
            long r2 = r2 + r6
            int r13 = r12 >>> 18
            r13 = r13 | 240(0xf0, float:3.36E-43)
            byte r13 = (byte) r13
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r13)
            r6 = 1
            long r6 = r6 + r2
            int r13 = r12 >>> 12
            r13 = r13 & 63
            r13 = r13 | 128(0x80, float:1.794E-43)
            byte r13 = (byte) r13
            com.google.android.gms.internal.measurement.zzhv.zza(r2, r13)
            r2 = 1
            long r14 = r6 + r2
            int r2 = r12 >>> 6
            r2 = r2 & 63
            r2 = r2 | 128(0x80, float:1.794E-43)
            byte r2 = (byte) r2
            com.google.android.gms.internal.measurement.zzhv.zza(r6, r2)
            r2 = 1
            long r2 = r2 + r14
            r6 = r12 & 63
            r6 = r6 | 128(0x80, float:1.794E-43)
            byte r6 = (byte) r6
            com.google.android.gms.internal.measurement.zzhv.zza(r14, r6)
            goto L_0x0085
        L_0x013c:
            r2 = 55296(0xd800, float:7.7486E-41)
            if (r2 > r12) goto L_0x015e
            r2 = 57343(0xdfff, float:8.0355E-41)
            if (r12 > r2) goto L_0x015e
            int r2 = r4 + 1
            if (r2 == r5) goto L_0x0158
            int r2 = r4 + 1
            r0 = r17
            char r2 = r0.charAt(r2)
            boolean r2 = java.lang.Character.isSurrogatePair(r12, r2)
            if (r2 != 0) goto L_0x015e
        L_0x0158:
            com.google.android.gms.internal.measurement.zzib r2 = new com.google.android.gms.internal.measurement.zzib
            r2.<init>(r4, r5)
            throw r2
        L_0x015e:
            java.lang.ArrayIndexOutOfBoundsException r2 = new java.lang.ArrayIndexOutOfBoundsException
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r4 = 46
            r3.<init>(r4)
            java.lang.String r4 = "Failed writing "
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.StringBuilder r3 = r3.append(r12)
            java.lang.String r4 = " at index "
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.StringBuilder r3 = r3.append(r6)
            java.lang.String r3 = r3.toString()
            r2.<init>(r3)
            throw r2
        L_0x0183:
            long r2 = r6 - r8
            int r2 = (int) r2
            r0 = r18
            r0.position(r2)
            goto L_0x006d
        L_0x018d:
            r6 = r2
            goto L_0x006e
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzie.zzb(java.lang.CharSequence, java.nio.ByteBuffer):void");
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: CFG modification limit reached, blocks count: 145 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.lang.String zzh(byte[] r13, int r14, int r15) throws com.google.android.gms.internal.measurement.zzfi {
        /*
            r12 = this;
            r6 = 0
            r0 = r14 | r15
            int r1 = r13.length
            int r1 = r1 - r14
            int r1 = r1 - r15
            r0 = r0 | r1
            if (r0 >= 0) goto L_0x002d
            java.lang.ArrayIndexOutOfBoundsException r0 = new java.lang.ArrayIndexOutOfBoundsException
            java.lang.String r1 = "buffer length=%d, index=%d, size=%d"
            r2 = 3
            java.lang.Object[] r2 = new java.lang.Object[r2]
            int r3 = r13.length
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)
            r2[r6] = r3
            r3 = 1
            java.lang.Integer r4 = java.lang.Integer.valueOf(r14)
            r2[r3] = r4
            r3 = 2
            java.lang.Integer r4 = java.lang.Integer.valueOf(r15)
            r2[r3] = r4
            java.lang.String r1 = java.lang.String.format(r1, r2)
            r0.<init>(r1)
            throw r0
        L_0x002d:
            int r7 = r14 + r15
            char[] r4 = new char[r15]
            r1 = r14
            r5 = r6
        L_0x0033:
            if (r1 >= r7) goto L_0x00de
            long r2 = (long) r1
            byte r0 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r2)
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzd(r0)
            if (r2 == 0) goto L_0x00de
            int r1 = r1 + 1
            com.google.android.gms.internal.measurement.zzia.zza(r0, r4, r5)
            int r5 = r5 + 1
            goto L_0x0033
        L_0x0048:
            long r2 = (long) r1
            byte r2 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r2)
            com.google.android.gms.internal.measurement.zzia.zza(r0, r2, r4, r5)
            int r5 = r5 + 1
            int r1 = r1 + 1
            r2 = r1
        L_0x0055:
            if (r2 >= r7) goto L_0x00d8
            int r1 = r2 + 1
            long r2 = (long) r2
            byte r0 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r2)
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzd(r0)
            if (r2 == 0) goto L_0x007e
            com.google.android.gms.internal.measurement.zzia.zza(r0, r4, r5)
            int r0 = r5 + 1
        L_0x0069:
            if (r1 >= r7) goto L_0x00d4
            long r2 = (long) r1
            byte r2 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r2)
            boolean r3 = com.google.android.gms.internal.measurement.zzia.zzd(r2)
            if (r3 == 0) goto L_0x00d4
            com.google.android.gms.internal.measurement.zzia.zza(r2, r4, r0)
            int r0 = r0 + 1
            int r1 = r1 + 1
            goto L_0x0069
        L_0x007e:
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zze(r0)
            if (r2 == 0) goto L_0x008b
            if (r1 < r7) goto L_0x0048
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x008b:
            boolean r2 = com.google.android.gms.internal.measurement.zzia.zzf(r0)
            if (r2 == 0) goto L_0x00af
            int r2 = r7 + -1
            if (r1 < r2) goto L_0x009a
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x009a:
            int r2 = r1 + 1
            long r8 = (long) r1
            byte r1 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r8)
            long r8 = (long) r2
            byte r3 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r8)
            com.google.android.gms.internal.measurement.zzia.zza(r0, r1, r3, r4, r5)
            int r5 = r5 + 1
            int r1 = r2 + 1
            r2 = r1
            goto L_0x0055
        L_0x00af:
            int r2 = r7 + -2
            if (r1 < r2) goto L_0x00b8
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzvb()
            throw r0
        L_0x00b8:
            int r2 = r1 + 1
            long r8 = (long) r1
            byte r1 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r8)
            int r8 = r2 + 1
            long r2 = (long) r2
            byte r2 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r2)
            long r10 = (long) r8
            byte r3 = com.google.android.gms.internal.measurement.zzhv.zza(r13, r10)
            com.google.android.gms.internal.measurement.zzia.zza(r0, r1, r2, r3, r4, r5)
            int r0 = r5 + 1
            int r0 = r0 + 1
            int r1 = r8 + 1
        L_0x00d4:
            r2 = r1
            r5 = r0
            goto L_0x0055
        L_0x00d8:
            java.lang.String r0 = new java.lang.String
            r0.<init>(r4, r6, r5)
            return r0
        L_0x00de:
            r2 = r1
            goto L_0x0055
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzie.zzh(byte[], int, int):java.lang.String");
    }
}
