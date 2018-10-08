package com.google.android.gms.common.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;

public final class zzm implements zzj {
    private /* synthetic */ zzd zzftf;

    public zzm(zzd zzd) {
        this.zzftf = zzd;
    }

    public final void zzf(@NonNull ConnectionResult connectionResult) {
        if (connectionResult.isSuccess()) {
            this.zzftf.zza(null, this.zzftf.zzajl());
        } else if (this.zzftf.zzfsx != null) {
            this.zzftf.zzfsx.onConnectionFailed(connectionResult);
        }
    }
}
