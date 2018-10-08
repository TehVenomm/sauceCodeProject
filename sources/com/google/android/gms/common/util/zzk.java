package com.google.android.gms.common.util;

public final class zzk {
    public static String zza(byte[] bArr, int i, int i2, boolean z) {
        if (bArr == null || bArr.length == 0 || i2 <= 0 || i2 > bArr.length) {
            return null;
        }
        StringBuilder stringBuilder = new StringBuilder((((i2 + 16) - 1) / 16) * 57);
        int i3 = 0;
        int i4 = 0;
        int i5 = i2;
        while (i5 > 0) {
            if (i3 == 0) {
                if (i2 < 65536) {
                    stringBuilder.append(String.format("%04X:", new Object[]{Integer.valueOf(i4)}));
                } else {
                    stringBuilder.append(String.format("%08X:", new Object[]{Integer.valueOf(i4)}));
                }
            } else if (i3 == 8) {
                stringBuilder.append(" -");
            }
            stringBuilder.append(String.format(" %02X", new Object[]{Integer.valueOf(bArr[i4] & 255)}));
            i5--;
            i3++;
            if (i3 == 16 || i5 == 0) {
                stringBuilder.append('\n');
                i3 = 0;
            }
            i4++;
        }
        return stringBuilder.toString();
    }
}
