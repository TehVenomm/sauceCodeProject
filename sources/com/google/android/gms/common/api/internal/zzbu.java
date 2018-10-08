package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.ConnectionResult;

final class zzbu implements Runnable {
    private /* synthetic */ zzbr zzfnx;
    private /* synthetic */ ConnectionResult zzfny;

    zzbu(zzbr zzbr, ConnectionResult connectionResult) {
        this.zzfnx = zzbr;
        this.zzfny = connectionResult;
    }

    public final void run() {
        this.zzfnx.onConnectionFailed(this.zzfny);
    }
}
