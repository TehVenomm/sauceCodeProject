package com.fasterxml.jackson.core.sym;

import java.util.Arrays;

public final class NameN extends Name {

    /* renamed from: q */
    private final int[] f419q;

    /* renamed from: q1 */
    private final int f420q1;

    /* renamed from: q2 */
    private final int f421q2;

    /* renamed from: q3 */
    private final int f422q3;

    /* renamed from: q4 */
    private final int f423q4;
    private final int qlen;

    NameN(String str, int i, int i2, int i3, int i4, int i5, int[] iArr, int i6) {
        super(str, i);
        this.f420q1 = i2;
        this.f421q2 = i3;
        this.f422q3 = i4;
        this.f423q4 = i5;
        this.f419q = iArr;
        this.qlen = i6;
    }

    public static NameN construct(String str, int i, int[] iArr, int i2) {
        int[] iArr2;
        if (i2 < 4) {
            throw new IllegalArgumentException();
        }
        int i3 = iArr[0];
        int i4 = iArr[1];
        int i5 = iArr[2];
        int i6 = iArr[3];
        if (i2 - 4 > 0) {
            iArr2 = Arrays.copyOfRange(iArr, 4, i2);
        } else {
            iArr2 = null;
        }
        return new NameN(str, i, i3, i4, i5, i6, iArr2, i2);
    }

    public boolean equals(int i) {
        return false;
    }

    public boolean equals(int i, int i2) {
        return false;
    }

    public boolean equals(int i, int i2, int i3) {
        return false;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:15:0x0039, code lost:
        if (r7[6] != r6.f419q[2]) goto L_?;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:17:0x0042, code lost:
        if (r7[5] != r6.f419q[1]) goto L_?;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:19:0x004b, code lost:
        if (r7[4] != r6.f419q[0]) goto L_?;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:?, code lost:
        return false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:29:?, code lost:
        return false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:30:?, code lost:
        return false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:31:?, code lost:
        return true;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public boolean equals(int[] r7, int r8) {
        /*
            r6 = this;
            r5 = 3
            r4 = 2
            r1 = 1
            r0 = 0
            int r2 = r6.qlen
            if (r8 == r2) goto L_0x0009
        L_0x0008:
            return r0
        L_0x0009:
            r2 = r7[r0]
            int r3 = r6.f420q1
            if (r2 != r3) goto L_0x0008
            r2 = r7[r1]
            int r3 = r6.f421q2
            if (r2 != r3) goto L_0x0008
            r2 = r7[r4]
            int r3 = r6.f422q3
            if (r2 != r3) goto L_0x0008
            r2 = r7[r5]
            int r3 = r6.f423q4
            if (r2 != r3) goto L_0x0008
            switch(r8) {
                case 4: goto L_0x004d;
                case 5: goto L_0x0044;
                case 6: goto L_0x003b;
                case 7: goto L_0x0032;
                case 8: goto L_0x0029;
                default: goto L_0x0024;
            }
        L_0x0024:
            boolean r0 = r6._equals2(r7)
            goto L_0x0008
        L_0x0029:
            r2 = 7
            r2 = r7[r2]
            int[] r3 = r6.f419q
            r3 = r3[r5]
            if (r2 != r3) goto L_0x0008
        L_0x0032:
            r2 = 6
            r2 = r7[r2]
            int[] r3 = r6.f419q
            r3 = r3[r4]
            if (r2 != r3) goto L_0x0008
        L_0x003b:
            r2 = 5
            r2 = r7[r2]
            int[] r3 = r6.f419q
            r3 = r3[r1]
            if (r2 != r3) goto L_0x0008
        L_0x0044:
            r2 = 4
            r2 = r7[r2]
            int[] r3 = r6.f419q
            r3 = r3[r0]
            if (r2 != r3) goto L_0x0008
        L_0x004d:
            r0 = r1
            goto L_0x0008
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.core.sym.NameN.equals(int[], int):boolean");
    }

    private final boolean _equals2(int[] iArr) {
        int i = this.qlen - 4;
        for (int i2 = 0; i2 < i; i2++) {
            if (iArr[i2 + 4] != this.f419q[i2]) {
                return false;
            }
        }
        return true;
    }
}
