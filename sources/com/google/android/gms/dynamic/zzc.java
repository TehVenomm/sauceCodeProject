package com.google.android.gms.dynamic;

import android.app.Activity;
import android.os.Bundle;

final class zzc implements zzi {
    private /* synthetic */ Activity val$activity;
    private /* synthetic */ Bundle zzaxl;
    private /* synthetic */ zza zzgop;
    private /* synthetic */ Bundle zzgoq;

    zzc(zza zza, Activity activity, Bundle bundle, Bundle bundle2) {
        this.zzgop = zza;
        this.val$activity = activity;
        this.zzgoq = bundle;
        this.zzaxl = bundle2;
    }

    public final int getState() {
        return 0;
    }

    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzgop.zzgol.onInflate(this.val$activity, this.zzgoq, this.zzaxl);
    }
}
