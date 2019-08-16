package com.google.firebase.iid;

import android.os.Looper;
import android.os.Message;
import com.google.android.gms.internal.firebase_messaging.zze;

final class zzat extends zze {
    private final /* synthetic */ zzau zzcw;

    zzat(zzau zzau, Looper looper) {
        this.zzcw = zzau;
        super(looper);
    }

    public final void handleMessage(Message message) {
        this.zzcw.zzb(message);
    }
}
