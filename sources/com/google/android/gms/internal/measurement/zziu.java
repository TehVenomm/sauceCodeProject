package com.google.android.gms.internal.measurement;

import java.nio.charset.Charset;
import org.apache.commons.lang3.CharEncoding;

public final class zziu {
    private static final Charset ISO_8859_1 = Charset.forName(CharEncoding.ISO_8859_1);
    protected static final Charset UTF_8 = Charset.forName("UTF-8");
    public static final Object zzaov = new Object();

    public static boolean equals(Object[] objArr, Object[] objArr2) {
        int length = objArr == null ? 0 : objArr.length;
        int length2 = objArr2 == null ? 0 : objArr2.length;
        int i = 0;
        int i2 = 0;
        while (true) {
            if (i2 >= length || objArr[i2] != null) {
                int i3 = i;
                while (i3 < length2 && objArr2[i3] == null) {
                    i3++;
                }
                boolean z = i2 >= length;
                boolean z2 = i3 >= length2;
                if (z && z2) {
                    return true;
                }
                if (z != z2 || !objArr[i2].equals(objArr2[i3])) {
                    return false;
                }
                i = i3 + 1;
                i2++;
            } else {
                i2++;
            }
        }
    }

    public static int hashCode(Object[] objArr) {
        int length = objArr == null ? 0 : objArr.length;
        int i = 0;
        int i2 = 0;
        while (i2 < length) {
            Object obj = objArr[i2];
            i2++;
            i = obj != null ? obj.hashCode() + (i * 31) : i;
        }
        return i;
    }

    public static void zza(zziq zziq, zziq zziq2) {
        if (zziq.zzaoo != null) {
            zziq2.zzaoo = (zzis) zziq.zzaoo.clone();
        }
    }
}
