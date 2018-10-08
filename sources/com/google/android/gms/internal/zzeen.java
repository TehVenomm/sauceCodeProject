package com.google.android.gms.internal;

import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import org.apache.commons.lang3.CharEncoding;

public final class zzeen {
    public static final byte[] EMPTY_BYTE_ARRAY;
    private static Charset ISO_8859_1 = Charset.forName(CharEncoding.ISO_8859_1);
    static final Charset UTF_8 = Charset.forName("UTF-8");
    private static ByteBuffer zzmzm;
    private static zzedt zzmzn = zzedt.zzas(EMPTY_BYTE_ARRAY);

    static {
        byte[] bArr = new byte[0];
        EMPTY_BYTE_ARRAY = bArr;
        zzmzm = ByteBuffer.wrap(bArr);
    }

    static int zza(int i, byte[] bArr, int i2, int i3) {
        for (int i4 = i2; i4 < i2 + i3; i4++) {
            i = (i * 31) + bArr[i4];
        }
        return i;
    }

    static <T> T zzu(T t) {
        if (t != null) {
            return t;
        }
        throw new NullPointerException();
    }
}
