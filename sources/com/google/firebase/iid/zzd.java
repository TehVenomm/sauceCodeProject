package com.google.firebase.iid;

import android.content.Intent;
import android.util.Log;

final /* synthetic */ class zzd implements Runnable {
    private final zze zzx;
    private final Intent zzy;

    zzd(zze zze, Intent intent) {
        this.zzx = zze;
        this.zzy = intent;
    }

    public final void run() {
        zze zze = this.zzx;
        String action = this.zzy.getAction();
        Log.w("EnhancedIntentService", new StringBuilder(String.valueOf(action).length() + 61).append("Service took too long to process intent: ").append(action).append(" App may get closed.").toString());
        zze.finish();
    }
}
