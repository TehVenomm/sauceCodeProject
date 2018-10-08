package com.google.android.gms.internal;

import android.util.Log;
import com.google.android.gms.common.util.zzm;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public final class zzckp {
    private final ExecutorService zzjcq = Executors.newSingleThreadExecutor();
    private volatile InputStream zzjcr = null;
    private volatile boolean zzjcs = false;

    private static void zza(OutputStream outputStream, boolean z, long j) {
        int i = 0;
        if (z) {
            i = 1;
        }
        try {
            outputStream.write(i);
        } catch (Throwable e) {
            Log.w("NearbyConnections", String.format("Unable to deliver status for Payload %d", new Object[]{Long.valueOf(j)}), e);
        } finally {
            zzm.closeQuietly(outputStream);
        }
    }

    final void shutdown() {
        this.zzjcs = true;
        this.zzjcq.shutdownNow();
        zzm.closeQuietly(this.zzjcr);
    }

    final void zza(InputStream inputStream, OutputStream outputStream, OutputStream outputStream2, long j) {
        this.zzjcq.execute(new zzckq(this, inputStream, outputStream, j, outputStream2));
    }
}
