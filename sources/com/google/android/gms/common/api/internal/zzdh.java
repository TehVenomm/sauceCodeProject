package com.google.android.gms.common.api.internal;

import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;

final class zzdh extends Handler {
    private /* synthetic */ zzdf zzfpj;

    public zzdh(zzdf zzdf, Looper looper) {
        this.zzfpj = zzdf;
        super(looper);
    }

    public final void handleMessage(Message message) {
        switch (message.what) {
            case 0:
                PendingResult pendingResult = (PendingResult) message.obj;
                synchronized (this.zzfpj.zzfiz) {
                    if (pendingResult == null) {
                        this.zzfpj.zzfpc.zzd(new Status(13, "Transform returned null"));
                    } else if (pendingResult instanceof zzcu) {
                        this.zzfpj.zzfpc.zzd(((zzcu) pendingResult).getStatus());
                    } else {
                        this.zzfpj.zzfpc.zza(pendingResult);
                    }
                }
                return;
            case 1:
                RuntimeException runtimeException = (RuntimeException) message.obj;
                String valueOf = String.valueOf(runtimeException.getMessage());
                Log.e("TransformedResultImpl", valueOf.length() != 0 ? "Runtime exception on the transformation worker thread: ".concat(valueOf) : new String("Runtime exception on the transformation worker thread: "));
                throw runtimeException;
            default:
                Log.e("TransformedResultImpl", "TransformationResultHandler received unknown message type: " + message.what);
                return;
        }
    }
}
