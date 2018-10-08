package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.zzam;
import com.google.android.gms.common.internal.zzj;
import java.util.Set;

final class zzbv implements zzcy, zzj {
    private Set<Scope> zzecn = null;
    private final zzh<?> zzfgm;
    private final zze zzfkb;
    private zzam zzfln = null;
    final /* synthetic */ zzbp zzfno;
    private boolean zzfnz = false;

    public zzbv(zzbp zzbp, zze zze, zzh<?> zzh) {
        this.zzfno = zzbp;
        this.zzfkb = zze;
        this.zzfgm = zzh;
    }

    @WorkerThread
    private final void zzaic() {
        if (this.zzfnz && this.zzfln != null) {
            this.zzfkb.zza(this.zzfln, this.zzecn);
        }
    }

    @WorkerThread
    public final void zzb(zzam zzam, Set<Scope> set) {
        if (zzam == null || set == null) {
            Log.wtf("GoogleApiManager", "Received null response from onSignInSuccess", new Exception());
            zzh(new ConnectionResult(4));
            return;
        }
        this.zzfln = zzam;
        this.zzecn = set;
        zzaic();
    }

    public final void zzf(@NonNull ConnectionResult connectionResult) {
        this.zzfno.mHandler.post(new zzbw(this, connectionResult));
    }

    @WorkerThread
    public final void zzh(ConnectionResult connectionResult) {
        ((zzbr) this.zzfno.zzfke.get(this.zzfgm)).zzh(connectionResult);
    }
}
