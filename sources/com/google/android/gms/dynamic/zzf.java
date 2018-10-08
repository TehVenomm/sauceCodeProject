package com.google.android.gms.dynamic;

import android.content.Context;
import android.content.Intent;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;

final class zzf implements OnClickListener {
    private /* synthetic */ Context zzaok;
    private /* synthetic */ Intent zzgou;

    zzf(Context context, Intent intent) {
        this.zzaok = context;
        this.zzgou = intent;
    }

    public final void onClick(View view) {
        try {
            this.zzaok.startActivity(this.zzgou);
        } catch (Throwable e) {
            Log.e("DeferredLifecycleHelper", "Failed to start resolution intent", e);
        }
    }
}
