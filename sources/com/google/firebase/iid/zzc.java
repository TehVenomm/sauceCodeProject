package com.google.firebase.iid;

import android.content.Intent;

final class zzc implements Runnable {
    private /* synthetic */ Intent val$intent;
    private /* synthetic */ Intent zzmih;
    private /* synthetic */ zzb zzmii;

    zzc(zzb zzb, Intent intent, Intent intent2) {
        this.zzmii = zzb;
        this.val$intent = intent;
        this.zzmih = intent2;
    }

    public final void run() {
        this.zzmii.handleIntent(this.val$intent);
        this.zzmii.zzm(this.zzmih);
    }
}
