package com.google.android.gms.common.util;

import android.os.Process;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import com.google.android.gms.common.annotation.KeepForSdk;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import javax.annotation.Nullable;

@KeepForSdk
public class ProcessUtils {
    private static String zzhf = null;
    private static int zzhg = 0;

    private ProcessUtils() {
    }

    @KeepForSdk
    @Nullable
    public static String getMyProcessName() {
        if (zzhf == null) {
            if (zzhg == 0) {
                zzhg = Process.myPid();
            }
            zzhf = zzd(zzhg);
        }
        return zzhf;
    }

    /* JADX WARNING: type inference failed for: r2v0, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r2v2, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r2v3 */
    /* JADX WARNING: type inference failed for: r2v8 */
    /* JADX WARNING: type inference failed for: r2v9 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 2 */
    @javax.annotation.Nullable
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.String zzd(int r3) {
        /*
            r0 = 0
            if (r3 > 0) goto L_0x0004
        L_0x0003:
            return r0
        L_0x0004:
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            r2 = 25
            r1.<init>(r2)     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.lang.String r2 = "/proc/"
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.lang.StringBuilder r1 = r1.append(r3)     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.lang.String r2 = "/cmdline"
            java.lang.StringBuilder r1 = r1.append(r2)     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.lang.String r1 = r1.toString()     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.io.BufferedReader r2 = zzk(r1)     // Catch:{ IOException -> 0x002f, all -> 0x0035 }
            java.lang.String r1 = r2.readLine()     // Catch:{ IOException -> 0x003e, all -> 0x003b }
            java.lang.String r0 = r1.trim()     // Catch:{ IOException -> 0x003e, all -> 0x003b }
            com.google.android.gms.common.util.IOUtils.closeQuietly(r2)
            goto L_0x0003
        L_0x002f:
            r1 = move-exception
            r2 = r0
        L_0x0031:
            com.google.android.gms.common.util.IOUtils.closeQuietly(r2)
            goto L_0x0003
        L_0x0035:
            r1 = move-exception
            r2 = r0
        L_0x0037:
            com.google.android.gms.common.util.IOUtils.closeQuietly(r2)
            throw r1
        L_0x003b:
            r0 = move-exception
            r1 = r0
            goto L_0x0037
        L_0x003e:
            r1 = move-exception
            goto L_0x0031
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.util.ProcessUtils.zzd(int):java.lang.String");
    }

    private static BufferedReader zzk(String str) throws IOException {
        ThreadPolicy allowThreadDiskReads = StrictMode.allowThreadDiskReads();
        try {
            return new BufferedReader(new FileReader(str));
        } finally {
            StrictMode.setThreadPolicy(allowThreadDiskReads);
        }
    }
}
