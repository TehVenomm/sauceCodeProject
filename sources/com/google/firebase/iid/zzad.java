package com.google.firebase.iid;

import android.os.Handler.Callback;
import android.os.Message;

final /* synthetic */ class zzad implements Callback {
    private final zzae zzcc;

    zzad(zzae zzae) {
        this.zzcc = zzae;
    }

    public final boolean handleMessage(Message message) {
        return this.zzcc.zza(message);
    }
}
