package com.google.android.gms.common.api.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;

final class zzbi extends Handler {
    private /* synthetic */ zzbd zzfmp;

    zzbi(zzbd zzbd, Looper looper) {
        this.zzfmp = zzbd;
        super(looper);
    }

    public final void handleMessage(Message message) {
        switch (message.what) {
            case 1:
                this.zzfmp.zzahg();
                return;
            case 2:
                this.zzfmp.resume();
                return;
            default:
                Log.w("GoogleApiClientImpl", "Unknown message id: " + message.what);
                return;
        }
    }
}
