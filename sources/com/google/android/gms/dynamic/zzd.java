package com.google.android.gms.dynamic;

import android.os.Bundle;

final class zzd implements zzi {
    private /* synthetic */ Bundle zzaxl;
    private /* synthetic */ zza zzgop;

    zzd(zza zza, Bundle bundle) {
        this.zzgop = zza;
        this.zzaxl = bundle;
    }

    public final int getState() {
        return 1;
    }

    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzgop.zzgol.onCreate(this.zzaxl);
    }
}
