package com.google.android.gms.dynamic;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.ViewGroup;
import android.widget.FrameLayout;

final class zze implements zzi {
    private /* synthetic */ Bundle zzaxl;
    private /* synthetic */ zza zzgop;
    private /* synthetic */ FrameLayout zzgor;
    private /* synthetic */ LayoutInflater zzgos;
    private /* synthetic */ ViewGroup zzgot;

    zze(zza zza, FrameLayout frameLayout, LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        this.zzgop = zza;
        this.zzgor = frameLayout;
        this.zzgos = layoutInflater;
        this.zzgot = viewGroup;
        this.zzaxl = bundle;
    }

    public final int getState() {
        return 2;
    }

    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzgor.removeAllViews();
        this.zzgor.addView(this.zzgop.zzgol.onCreateView(this.zzgos, this.zzgot, this.zzaxl));
    }
}
