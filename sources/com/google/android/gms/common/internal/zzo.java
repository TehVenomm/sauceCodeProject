package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.support.annotation.BinderThread;
import android.support.annotation.Nullable;
import com.google.android.gms.common.ConnectionResult;

public final class zzo extends zze {
    private /* synthetic */ zzd zzftf;

    @BinderThread
    public zzo(zzd zzd, @Nullable int i, Bundle bundle) {
        this.zzftf = zzd;
        super(zzd, i, null);
    }

    protected final boolean zzajn() {
        this.zzftf.zzfsr.zzf(ConnectionResult.zzfez);
        return true;
    }

    protected final void zzj(ConnectionResult connectionResult) {
        this.zzftf.zzfsr.zzf(connectionResult);
        this.zzftf.onConnectionFailed(connectionResult);
    }
}
