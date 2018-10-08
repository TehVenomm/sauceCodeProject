package com.google.firebase.iid;

import android.content.Intent;
import android.os.ConditionVariable;
import android.util.Log;
import java.io.IOException;

final class zzo implements zzp {
    private Intent intent;
    private final ConditionVariable zzmjl;
    private String zzmjm;

    private zzo() {
        this.zzmjl = new ConditionVariable();
    }

    public final void onError(String str) {
        this.zzmjm = str;
        this.zzmjl.open();
    }

    public final Intent zzbyo() throws IOException {
        if (!this.zzmjl.block(30000)) {
            Log.w("InstanceID/Rpc", "No response");
            throw new IOException("TIMEOUT");
        } else if (this.zzmjm == null) {
            return this.intent;
        } else {
            throw new IOException(this.zzmjm);
        }
    }

    public final void zzq(Intent intent) {
        this.intent = intent;
        this.zzmjl.open();
    }
}
