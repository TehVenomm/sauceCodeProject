package com.google.android.gms.common.api.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import com.google.android.gms.common.internal.zzbp;

final class zzck extends Handler {
    private /* synthetic */ zzcj zzfoq;

    public zzck(zzcj zzcj, Looper looper) {
        this.zzfoq = zzcj;
        super(looper);
    }

    public final void handleMessage(Message message) {
        boolean z = true;
        if (message.what != 1) {
            z = false;
        }
        zzbp.zzbh(z);
        this.zzfoq.zzb((zzcm) message.obj);
    }
}
