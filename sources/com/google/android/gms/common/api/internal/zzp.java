package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.zzbp;

final class zzp {
    private final int zzfis;
    private final ConnectionResult zzfit;

    zzp(ConnectionResult connectionResult, int i) {
        zzbp.zzu(connectionResult);
        this.zzfit = connectionResult;
        this.zzfis = i;
    }

    final int zzagb() {
        return this.zzfis;
    }

    final ConnectionResult zzagc() {
        return this.zzfit;
    }
}
