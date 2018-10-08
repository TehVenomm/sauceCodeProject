package com.google.android.gms.common.internal;

import android.app.PendingIntent;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;

final class zzh extends Handler {
    private /* synthetic */ zzd zzftf;

    public zzh(zzd zzd, Looper looper) {
        this.zzftf = zzd;
        super(looper);
    }

    private static void zza(Message message) {
        ((zzi) message.obj).unregister();
    }

    private static boolean zzb(Message message) {
        return message.what == 2 || message.what == 1 || message.what == 7;
    }

    public final void handleMessage(Message message) {
        PendingIntent pendingIntent = null;
        if (this.zzftf.zzftc.get() != message.arg1) {
            if (zzb(message)) {
                zza(message);
            }
        } else if ((message.what == 1 || message.what == 7 || message.what == 4 || message.what == 5) && !this.zzftf.isConnecting()) {
            zza(message);
        } else if (message.what == 4) {
            this.zzftf.zzfta = new ConnectionResult(message.arg2);
            if (!this.zzftf.zzajm() || this.zzftf.zzftb) {
                r0 = this.zzftf.zzfta != null ? this.zzftf.zzfta : new ConnectionResult(8);
                this.zzftf.zzfsr.zzf(r0);
                this.zzftf.onConnectionFailed(r0);
                return;
            }
            this.zzftf.zza(3, null);
        } else if (message.what == 5) {
            r0 = this.zzftf.zzfta != null ? this.zzftf.zzfta : new ConnectionResult(8);
            this.zzftf.zzfsr.zzf(r0);
            this.zzftf.onConnectionFailed(r0);
        } else if (message.what == 3) {
            if (message.obj instanceof PendingIntent) {
                pendingIntent = (PendingIntent) message.obj;
            }
            ConnectionResult connectionResult = new ConnectionResult(message.arg2, pendingIntent);
            this.zzftf.zzfsr.zzf(connectionResult);
            this.zzftf.onConnectionFailed(connectionResult);
        } else if (message.what == 6) {
            this.zzftf.zza(5, null);
            if (this.zzftf.zzfsw != null) {
                this.zzftf.zzfsw.onConnectionSuspended(message.arg2);
            }
            this.zzftf.onConnectionSuspended(message.arg2);
            this.zzftf.zza(5, 1, null);
        } else if (message.what == 2 && !this.zzftf.isConnected()) {
            zza(message);
        } else if (zzb(message)) {
            ((zzi) message.obj).zzajo();
        } else {
            Log.wtf("GmsClient", "Don't know how to handle message: " + message.what, new Exception());
        }
    }
}
