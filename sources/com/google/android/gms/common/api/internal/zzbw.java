package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.ConnectionResult;
import java.util.Collections;

final class zzbw implements Runnable {
    private /* synthetic */ ConnectionResult zzfny;
    private /* synthetic */ zzbv zzfoa;

    zzbw(zzbv zzbv, ConnectionResult connectionResult) {
        this.zzfoa = zzbv;
        this.zzfny = connectionResult;
    }

    public final void run() {
        if (this.zzfny.isSuccess()) {
            this.zzfoa.zzfnz = true;
            if (this.zzfoa.zzfkb.zzaaa()) {
                this.zzfoa.zzaic();
                return;
            } else {
                this.zzfoa.zzfkb.zza(null, Collections.emptySet());
                return;
            }
        }
        ((zzbr) this.zzfoa.zzfno.zzfke.get(this.zzfoa.zzfgm)).onConnectionFailed(this.zzfny);
    }
}
