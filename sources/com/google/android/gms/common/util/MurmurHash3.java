package com.google.android.gms.common.util;

import com.google.android.gms.common.annotation.KeepForSdk;

@KeepForSdk
public class MurmurHash3 {
    private MurmurHash3() {
    }

    /* JADX WARNING: Code restructure failed: missing block: B:5:0x0046, code lost:
        r0 = r0 ^ r9;
        r0 = (r0 ^ (r0 >>> 16)) * -2048144789;
        r0 = (r0 ^ (r0 >>> 13)) * -1028477387;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:6:0x0058, code lost:
        return r0 ^ (r0 >>> 16);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:8:0x0061, code lost:
        r1 = r1 | ((r7[r2 + 1] & 255) << 8);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:9:0x006a, code lost:
        r1 = (r1 | (r7[r2] & 255)) * -862048943;
        r0 = r0 ^ (((r1 << 15) | (r1 >>> 17)) * 461845907);
     */
    @com.google.android.gms.common.annotation.KeepForSdk
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static int murmurhash3_x86_32(byte[] r7, int r8, int r9, int r10) {
        /*
            r6 = 461845907(0x1b873593, float:2.2368498E-22)
            r1 = 0
            r5 = -862048943(0xffffffffcc9e2d51, float:-8.2930312E7)
            r0 = r9 & -4
            int r2 = r8 + r0
            r0 = r10
        L_0x000c:
            if (r8 >= r2) goto L_0x0041
            byte r3 = r7[r8]
            r3 = r3 & 255(0xff, float:3.57E-43)
            int r4 = r8 + 1
            byte r4 = r7[r4]
            r4 = r4 & 255(0xff, float:3.57E-43)
            int r4 = r4 << 8
            r3 = r3 | r4
            int r4 = r8 + 2
            byte r4 = r7[r4]
            r4 = r4 & 255(0xff, float:3.57E-43)
            int r4 = r4 << 16
            r3 = r3 | r4
            int r4 = r8 + 3
            byte r4 = r7[r4]
            int r4 = r4 << 24
            r3 = r3 | r4
            int r3 = r3 * r5
            int r4 = r3 >>> 17
            int r3 = r3 << 15
            r3 = r3 | r4
            int r3 = r3 * r6
            r0 = r0 ^ r3
            r3 = -430675100(0xffffffffe6546b64, float:-2.5078068E23)
            int r4 = r0 >>> 19
            int r0 = r0 << 13
            r0 = r0 | r4
            int r0 = r0 * 5
            int r0 = r0 + r3
            int r8 = r8 + 4
            goto L_0x000c
        L_0x0041:
            r3 = r9 & 3
            switch(r3) {
                case 1: goto L_0x006a;
                case 2: goto L_0x0061;
                case 3: goto L_0x0059;
                default: goto L_0x0046;
            }
        L_0x0046:
            r0 = r0 ^ r9
            int r1 = r0 >>> 16
            r0 = r0 ^ r1
            r1 = -2048144789(0xffffffff85ebca6b, float:-2.217365E-35)
            int r0 = r0 * r1
            int r1 = r0 >>> 13
            r0 = r0 ^ r1
            r1 = -1028477387(0xffffffffc2b2ae35, float:-89.34025)
            int r0 = r0 * r1
            int r1 = r0 >>> 16
            r0 = r0 ^ r1
            return r0
        L_0x0059:
            int r1 = r2 + 2
            byte r1 = r7[r1]
            r1 = r1 & 255(0xff, float:3.57E-43)
            int r1 = r1 << 16
        L_0x0061:
            int r3 = r2 + 1
            byte r3 = r7[r3]
            r3 = r3 & 255(0xff, float:3.57E-43)
            int r3 = r3 << 8
            r1 = r1 | r3
        L_0x006a:
            byte r2 = r7[r2]
            r2 = r2 & 255(0xff, float:3.57E-43)
            r1 = r1 | r2
            int r1 = r1 * r5
            int r2 = r1 >>> 17
            int r1 = r1 << 15
            r1 = r1 | r2
            int r1 = r1 * r6
            r0 = r0 ^ r1
            goto L_0x0046
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.util.MurmurHash3.murmurhash3_x86_32(byte[], int, int, int):int");
    }
}
