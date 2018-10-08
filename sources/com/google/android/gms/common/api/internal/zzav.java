package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.ConnectionResult;

final class zzav extends zzbm {
    private /* synthetic */ ConnectionResult zzflu;
    private /* synthetic */ zzau zzflv;

    zzav(zzau zzau, zzbk zzbk, ConnectionResult connectionResult) {
        this.zzflv = zzau;
        this.zzflu = connectionResult;
        super(zzbk);
    }

    public final void zzagy() {
        this.zzflv.zzflr.zze(this.zzflu);
    }
}
