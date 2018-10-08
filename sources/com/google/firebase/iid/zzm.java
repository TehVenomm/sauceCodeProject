package com.google.firebase.iid;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;

final class zzm extends Handler {
    private /* synthetic */ zzl zzmjk;

    zzm(zzl zzl, Looper looper) {
        this.zzmjk = zzl;
        super(looper);
    }

    public final void handleMessage(Message message) {
        this.zzmjk.zzc(message);
    }
}
