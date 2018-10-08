package com.google.android.gms.common.api.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

final class zzbn extends Handler {
    private /* synthetic */ zzbl zzfnc;

    zzbn(zzbl zzbl, Looper looper) {
        this.zzfnc = zzbl;
        super(looper);
    }

    public final void handleMessage(Message message) {
        switch (message.what) {
            case 1:
                ((zzbm) message.obj).zzc(this.zzfnc);
                return;
            case 2:
                throw ((RuntimeException) message.obj);
            default:
                Log.w("GACStateManager", "Unknown message id: " + message.what);
                return;
        }
    }
}
