package com.google.android.gms.measurement.internal;

import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.Preconditions;

public final class zzho {
    /* JADX WARNING: Removed duplicated region for block: B:13:0x0033 A[Catch:{ IOException | ClassNotFoundException -> 0x003c }] */
    /* JADX WARNING: Removed duplicated region for block: B:15:0x0038 A[Catch:{ IOException | ClassNotFoundException -> 0x003c }] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.Object zza(java.lang.Object r5) {
        /*
            r0 = 0
            if (r5 != 0) goto L_0x0004
        L_0x0003:
            return r0
        L_0x0004:
            java.io.ByteArrayOutputStream r1 = new java.io.ByteArrayOutputStream     // Catch:{ all -> 0x002e }
            r1.<init>()     // Catch:{ all -> 0x002e }
            java.io.ObjectOutputStream r2 = new java.io.ObjectOutputStream     // Catch:{ all -> 0x002e }
            r2.<init>(r1)     // Catch:{ all -> 0x002e }
            r2.writeObject(r5)     // Catch:{ all -> 0x003e }
            r2.flush()     // Catch:{ all -> 0x003e }
            java.io.ObjectInputStream r3 = new java.io.ObjectInputStream     // Catch:{ all -> 0x003e }
            java.io.ByteArrayInputStream r4 = new java.io.ByteArrayInputStream     // Catch:{ all -> 0x003e }
            byte[] r1 = r1.toByteArray()     // Catch:{ all -> 0x003e }
            r4.<init>(r1)     // Catch:{ all -> 0x003e }
            r3.<init>(r4)     // Catch:{ all -> 0x003e }
            java.lang.Object r1 = r3.readObject()     // Catch:{ all -> 0x0041 }
            r2.close()     // Catch:{ IOException -> 0x003c, ClassNotFoundException -> 0x0043 }
            r3.close()     // Catch:{ IOException -> 0x003c, ClassNotFoundException -> 0x0043 }
            r0 = r1
            goto L_0x0003
        L_0x002e:
            r1 = move-exception
            r2 = r0
            r3 = r0
        L_0x0031:
            if (r2 == 0) goto L_0x0036
            r2.close()     // Catch:{ IOException -> 0x003c, ClassNotFoundException -> 0x0043 }
        L_0x0036:
            if (r3 == 0) goto L_0x003b
            r3.close()     // Catch:{ IOException -> 0x003c, ClassNotFoundException -> 0x0043 }
        L_0x003b:
            throw r1     // Catch:{ IOException -> 0x003c, ClassNotFoundException -> 0x0043 }
        L_0x003c:
            r1 = move-exception
            goto L_0x0003
        L_0x003e:
            r1 = move-exception
            r3 = r0
            goto L_0x0031
        L_0x0041:
            r1 = move-exception
            goto L_0x0031
        L_0x0043:
            r1 = move-exception
            goto L_0x0003
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzho.zza(java.lang.Object):java.lang.Object");
    }

    @Nullable
    public static String zza(String str, String[] strArr, String[] strArr2) {
        Preconditions.checkNotNull(strArr);
        Preconditions.checkNotNull(strArr2);
        int min = Math.min(strArr.length, strArr2.length);
        for (int i = 0; i < min; i++) {
            String str2 = strArr[i];
            boolean equals = (str == null && str2 == null) ? true : str == null ? false : str.equals(str2);
            if (equals) {
                return strArr2[i];
            }
        }
        return null;
    }
}
