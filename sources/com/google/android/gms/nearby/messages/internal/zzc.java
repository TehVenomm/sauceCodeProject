package com.google.android.gms.nearby.messages.internal;

import java.util.Arrays;

public class zzc {
    private static final char[] zzjfp = "0123456789abcdef".toCharArray();
    private final byte[] zzjfq;

    protected zzc(byte[] bArr) {
        this.zzjfq = bArr;
    }

    public static byte[] zzkk(String str) {
        int length = str.length() / 2;
        byte[] bArr = new byte[length];
        for (int i = 0; i < length; i++) {
            bArr[i] = (byte) ((byte) ((Character.digit(str.charAt(i * 2), 16) << 4) + Character.digit(str.charAt((i * 2) + 1), 16)));
        }
        return bArr;
    }

    public static String zzr(byte[] bArr) {
        StringBuilder stringBuilder = new StringBuilder(bArr.length * 2);
        for (byte b : bArr) {
            stringBuilder.append(zzjfp[(b >> 4) & 15]).append(zzjfp[b & 15]);
        }
        return stringBuilder.toString();
    }

    public boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!obj.getClass().isAssignableFrom(getClass())) {
            return false;
        }
        return Arrays.equals(this.zzjfq, ((zzc) obj).zzjfq);
    }

    public final byte[] getBytes() {
        return this.zzjfq;
    }

    public final String getHex() {
        return zzr(this.zzjfq);
    }

    public int hashCode() {
        return Arrays.hashCode(this.zzjfq);
    }

    public String toString() {
        return zzr(this.zzjfq);
    }
}
