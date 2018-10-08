package com.google.firebase.iid;

import android.util.Log;

final class zzg implements Runnable {
    private /* synthetic */ zzd zzmio;
    private /* synthetic */ zzf zzmip;

    zzg(zzf zzf, zzd zzd) {
        this.zzmip = zzf;
        this.zzmio = zzd;
    }

    public final void run() {
        if (Log.isLoggable("EnhancedIntentService", 3)) {
            Log.d("EnhancedIntentService", "bg processing of the intent starting now");
        }
        this.zzmip.zzmin.handleIntent(this.zzmio.intent);
        this.zzmio.finish();
    }
}
