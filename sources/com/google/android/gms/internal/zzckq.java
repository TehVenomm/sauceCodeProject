package com.google.android.gms.internal;

import android.util.Log;
import com.google.android.gms.common.util.zzm;
import java.io.InputStream;
import java.io.OutputStream;

final class zzckq implements Runnable {
    private /* synthetic */ InputStream zzjct;
    private /* synthetic */ OutputStream zzjcu;
    private /* synthetic */ long zzjcv;
    private /* synthetic */ OutputStream zzjcw;
    private /* synthetic */ zzckp zzjcx;

    zzckq(zzckp zzckp, InputStream inputStream, OutputStream outputStream, long j, OutputStream outputStream2) {
        this.zzjcx = zzckp;
        this.zzjct = inputStream;
        this.zzjcu = outputStream;
        this.zzjcv = j;
        this.zzjcw = outputStream2;
    }

    public final void run() {
        Throwable e;
        boolean z = true;
        this.zzjcx.zzjcr = this.zzjct;
        try {
            zzm.zza(this.zzjct, this.zzjcu, false, 65536);
            zzm.closeQuietly(this.zzjct);
            zzckp.zza(this.zzjcw, false, this.zzjcv);
            zzm.closeQuietly(this.zzjcu);
            this.zzjcx.zzjcr = null;
        } catch (Throwable e2) {
            if (this.zzjcx.zzjcs) {
                Log.d("NearbyConnections", String.format("Terminating copying stream for Payload %d due to shutdown of OutgoingPayloadStreamer.", new Object[]{Long.valueOf(this.zzjcv)}));
            } else {
                Log.w("NearbyConnections", String.format("Exception copying stream for Payload %d", new Object[]{Long.valueOf(this.zzjcv)}), e2);
            }
            zzm.closeQuietly(this.zzjct);
            zzckp.zza(this.zzjcw, true, this.zzjcv);
            zzm.closeQuietly(this.zzjcu);
            this.zzjcx.zzjcr = null;
        } catch (Throwable th) {
            e2 = th;
            zzm.closeQuietly(this.zzjct);
            zzckp.zza(this.zzjcw, z, this.zzjcv);
            zzm.closeQuietly(this.zzjcu);
            this.zzjcx.zzjcr = null;
            throw e2;
        }
    }
}
