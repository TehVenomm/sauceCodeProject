package com.google.android.gms.common.util;

import android.os.Process;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import java.io.BufferedReader;
import java.io.Closeable;
import java.io.FileReader;
import java.io.IOException;

public final class zzq {
    private static String zzfyz = null;
    private static final int zzfza = Process.myPid();

    public static String zzalk() {
        if (zzfyz == null) {
            zzfyz = zzcg(zzfza);
        }
        return zzfyz;
    }

    private static String zzcg(int i) {
        String trim;
        Throwable th;
        Closeable closeable = null;
        if (i > 0) {
            ThreadPolicy allowThreadDiskReads;
            Closeable bufferedReader;
            try {
                allowThreadDiskReads = StrictMode.allowThreadDiskReads();
                bufferedReader = new BufferedReader(new FileReader("/proc/" + i + "/cmdline"));
                try {
                    StrictMode.setThreadPolicy(allowThreadDiskReads);
                    trim = bufferedReader.readLine().trim();
                    zzm.closeQuietly(bufferedReader);
                } catch (IOException e) {
                    zzm.closeQuietly(bufferedReader);
                    return trim;
                } catch (Throwable th2) {
                    Closeable closeable2 = bufferedReader;
                    th = th2;
                    closeable = closeable2;
                    zzm.closeQuietly(closeable);
                    throw th;
                }
            } catch (IOException e2) {
                bufferedReader = closeable;
                zzm.closeQuietly(bufferedReader);
                return trim;
            } catch (Throwable th3) {
                th = th3;
                zzm.closeQuietly(closeable);
                throw th;
            }
        }
        return trim;
    }
}
