package com.google.android.gms.games.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.SparseArray;
import java.util.Arrays;

public final class zzc {
    public static int zza(@Nullable Bundle bundle) {
        if (bundle == null) {
            return 0;
        }
        int size = bundle.size();
        if (size == 0) {
            return 0;
        }
        String[] strArr = (String[]) bundle.keySet().toArray(new String[size]);
        Arrays.sort(strArr);
        int i = 1;
        for (String str : strArr) {
            int i2 = i * 31;
            Object obj = bundle.get(str);
            if (obj == null) {
                i = i2;
            } else if (obj instanceof Bundle) {
                i = zza((Bundle) obj) + i2;
            } else if (obj instanceof byte[]) {
                i = Arrays.hashCode((byte[]) obj) + i2;
            } else if (obj instanceof char[]) {
                i = Arrays.hashCode((char[]) obj) + i2;
            } else if (obj instanceof short[]) {
                i = Arrays.hashCode((short[]) obj) + i2;
            } else if (obj instanceof float[]) {
                i = Arrays.hashCode((float[]) obj) + i2;
            } else if (obj instanceof CharSequence[]) {
                i = Arrays.hashCode((CharSequence[]) obj) + i2;
            } else if (obj instanceof Object[]) {
                Object[] objArr = (Object[]) obj;
                int length = objArr.length;
                int i3 = 0;
                int i4 = 1;
                while (i3 < length) {
                    Object obj2 = objArr[i3];
                    int i5 = i4 * 31;
                    int i6 = obj2 instanceof Bundle ? zza((Bundle) obj2) + i5 : obj2 != null ? obj2.hashCode() + i5 : i5;
                    i3++;
                    i4 = i6;
                }
                i = i2 + i4;
            } else if (obj instanceof SparseArray) {
                SparseArray sparseArray = (SparseArray) obj;
                int size2 = sparseArray.size();
                int i7 = 1;
                for (int i8 = 0; i8 < size2; i8++) {
                    int keyAt = ((i7 * 31) + sparseArray.keyAt(i8)) * 31;
                    Object valueAt = sparseArray.valueAt(i8);
                    i7 = valueAt instanceof Bundle ? zza((Bundle) valueAt) + keyAt : valueAt != null ? valueAt.hashCode() + keyAt : keyAt;
                }
                i = i2 + i7;
            } else {
                i = obj.hashCode() + i2;
            }
        }
        return i;
    }

    /* JADX WARNING: Removed duplicated region for block: B:128:0x0125 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:145:0x00d8 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:153:0x002c A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:154:0x002c A[SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static boolean zza(@android.support.annotation.Nullable android.os.Bundle r10, @android.support.annotation.Nullable android.os.Bundle r11) {
        /*
            r4 = 1
            r5 = 0
            if (r10 != r11) goto L_0x0006
            r0 = r4
        L_0x0005:
            return r0
        L_0x0006:
            if (r10 == 0) goto L_0x000a
            if (r11 != 0) goto L_0x000c
        L_0x000a:
            r0 = r5
            goto L_0x0005
        L_0x000c:
            int r0 = r10.size()
            int r1 = r11.size()
            if (r0 == r1) goto L_0x0018
            r0 = r5
            goto L_0x0005
        L_0x0018:
            java.util.Set r0 = r10.keySet()
            java.util.Set r1 = r11.keySet()
            boolean r1 = r0.equals(r1)
            if (r1 != 0) goto L_0x0028
            r0 = r5
            goto L_0x0005
        L_0x0028:
            java.util.Iterator r7 = r0.iterator()
        L_0x002c:
            boolean r0 = r7.hasNext()
            if (r0 == 0) goto L_0x0170
            java.lang.Object r0 = r7.next()
            java.lang.String r0 = (java.lang.String) r0
            java.lang.Object r1 = r10.get(r0)
            java.lang.Object r2 = r11.get(r0)
            if (r1 != 0) goto L_0x0046
            if (r2 == 0) goto L_0x002c
            r0 = r5
            goto L_0x0005
        L_0x0046:
            boolean r0 = r1 instanceof android.os.Bundle
            if (r0 == 0) goto L_0x005c
            boolean r0 = r2 instanceof android.os.Bundle
            if (r0 == 0) goto L_0x005a
            r0 = r1
            android.os.Bundle r0 = (android.os.Bundle) r0
            r1 = r2
            android.os.Bundle r1 = (android.os.Bundle) r1
            boolean r0 = zza(r0, r1)
            if (r0 != 0) goto L_0x002c
        L_0x005a:
            r0 = r5
            goto L_0x0005
        L_0x005c:
            boolean r0 = r1 instanceof byte[]
            if (r0 == 0) goto L_0x0070
            boolean r0 = r2 instanceof byte[]
            if (r0 == 0) goto L_0x006e
            byte[] r1 = (byte[]) r1
            byte[] r2 = (byte[]) r2
            boolean r0 = java.util.Arrays.equals(r1, r2)
            if (r0 != 0) goto L_0x002c
        L_0x006e:
            r0 = r5
            goto L_0x0005
        L_0x0070:
            boolean r0 = r1 instanceof char[]
            if (r0 == 0) goto L_0x0084
            boolean r0 = r2 instanceof char[]
            if (r0 == 0) goto L_0x0082
            char[] r1 = (char[]) r1
            char[] r2 = (char[]) r2
            boolean r0 = java.util.Arrays.equals(r1, r2)
            if (r0 != 0) goto L_0x002c
        L_0x0082:
            r0 = r5
            goto L_0x0005
        L_0x0084:
            boolean r0 = r1 instanceof short[]
            if (r0 == 0) goto L_0x0099
            boolean r0 = r2 instanceof short[]
            if (r0 == 0) goto L_0x0096
            short[] r1 = (short[]) r1
            short[] r2 = (short[]) r2
            boolean r0 = java.util.Arrays.equals(r1, r2)
            if (r0 != 0) goto L_0x002c
        L_0x0096:
            r0 = r5
            goto L_0x0005
        L_0x0099:
            boolean r0 = r1 instanceof float[]
            if (r0 == 0) goto L_0x00ae
            boolean r0 = r2 instanceof float[]
            if (r0 == 0) goto L_0x00ab
            float[] r1 = (float[]) r1
            float[] r2 = (float[]) r2
            boolean r0 = java.util.Arrays.equals(r1, r2)
            if (r0 != 0) goto L_0x002c
        L_0x00ab:
            r0 = r5
            goto L_0x0005
        L_0x00ae:
            boolean r0 = r1 instanceof java.lang.CharSequence[]
            if (r0 == 0) goto L_0x00c3
            boolean r0 = r2 instanceof java.lang.CharSequence[]
            if (r0 == 0) goto L_0x00c0
            java.lang.CharSequence[] r1 = (java.lang.CharSequence[]) r1
            java.lang.CharSequence[] r2 = (java.lang.CharSequence[]) r2
            boolean r0 = java.util.Arrays.equals(r1, r2)
            if (r0 != 0) goto L_0x002c
        L_0x00c0:
            r0 = r5
            goto L_0x0005
        L_0x00c3:
            boolean r0 = r1 instanceof java.lang.Object[]
            if (r0 == 0) goto L_0x010a
            boolean r0 = r2 instanceof java.lang.Object[]
            if (r0 == 0) goto L_0x00d8
            android.os.Parcelable[] r1 = (android.os.Parcelable[]) r1
            android.os.Parcelable[] r2 = (android.os.Parcelable[]) r2
            if (r1 == r2) goto L_0x0108
            int r8 = r1.length
            int r0 = r2.length
            if (r0 == r8) goto L_0x00db
            r0 = r5
        L_0x00d6:
            if (r0 != 0) goto L_0x002c
        L_0x00d8:
            r0 = r5
            goto L_0x0005
        L_0x00db:
            r6 = r5
        L_0x00dc:
            if (r6 >= r8) goto L_0x0108
            r0 = r1[r6]
            r3 = r2[r6]
            if (r0 != 0) goto L_0x00e8
            if (r3 == 0) goto L_0x0104
            r0 = r5
            goto L_0x00d6
        L_0x00e8:
            boolean r9 = r0 instanceof android.os.Bundle
            if (r9 == 0) goto L_0x00fc
            boolean r9 = r3 instanceof android.os.Bundle
            if (r9 == 0) goto L_0x00fa
            android.os.Bundle r0 = (android.os.Bundle) r0
            android.os.Bundle r3 = (android.os.Bundle) r3
            boolean r0 = zza(r0, r3)
            if (r0 != 0) goto L_0x0104
        L_0x00fa:
            r0 = r5
            goto L_0x00d6
        L_0x00fc:
            boolean r0 = r0.equals(r3)
            if (r0 != 0) goto L_0x0104
            r0 = r5
            goto L_0x00d6
        L_0x0104:
            int r0 = r6 + 1
            r6 = r0
            goto L_0x00dc
        L_0x0108:
            r0 = r4
            goto L_0x00d6
        L_0x010a:
            boolean r0 = r1 instanceof android.util.SparseArray
            if (r0 == 0) goto L_0x0167
            boolean r0 = r2 instanceof android.util.SparseArray
            if (r0 == 0) goto L_0x0125
            android.util.SparseArray r1 = (android.util.SparseArray) r1
            android.util.SparseArray r2 = (android.util.SparseArray) r2
            if (r1 == r2) goto L_0x0165
            int r8 = r1.size()
            int r0 = r2.size()
            if (r0 == r8) goto L_0x0128
            r0 = r5
        L_0x0123:
            if (r0 != 0) goto L_0x002c
        L_0x0125:
            r0 = r5
            goto L_0x0005
        L_0x0128:
            r6 = r5
        L_0x0129:
            if (r6 >= r8) goto L_0x0165
            int r0 = r1.keyAt(r6)
            int r3 = r2.keyAt(r6)
            if (r0 == r3) goto L_0x0137
            r0 = r5
            goto L_0x0123
        L_0x0137:
            java.lang.Object r0 = r1.valueAt(r6)
            java.lang.Object r3 = r2.valueAt(r6)
            if (r0 != 0) goto L_0x0145
            if (r3 == 0) goto L_0x0161
            r0 = r5
            goto L_0x0123
        L_0x0145:
            boolean r9 = r0 instanceof android.os.Bundle
            if (r9 == 0) goto L_0x0159
            boolean r9 = r3 instanceof android.os.Bundle
            if (r9 == 0) goto L_0x0157
            android.os.Bundle r0 = (android.os.Bundle) r0
            android.os.Bundle r3 = (android.os.Bundle) r3
            boolean r0 = zza(r0, r3)
            if (r0 != 0) goto L_0x0161
        L_0x0157:
            r0 = r5
            goto L_0x0123
        L_0x0159:
            boolean r0 = r0.equals(r3)
            if (r0 != 0) goto L_0x0161
            r0 = r5
            goto L_0x0123
        L_0x0161:
            int r0 = r6 + 1
            r6 = r0
            goto L_0x0129
        L_0x0165:
            r0 = r4
            goto L_0x0123
        L_0x0167:
            boolean r0 = r1.equals(r2)
            if (r0 != 0) goto L_0x002c
            r0 = r5
            goto L_0x0005
        L_0x0170:
            r0 = r4
            goto L_0x0005
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.games.internal.zzc.zza(android.os.Bundle, android.os.Bundle):boolean");
    }
}
