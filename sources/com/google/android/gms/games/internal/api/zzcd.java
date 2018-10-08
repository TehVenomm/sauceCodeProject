package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzcd extends zzcn {
    private /* synthetic */ String zzhhv;
    private /* synthetic */ boolean zzhhw;
    private /* synthetic */ int zzhhx;

    zzcd(zzcb zzcb, GoogleApiClient googleApiClient, String str, boolean z, int i) {
        this.zzhhv = str;
        this.zzhhw = z;
        this.zzhhx = i;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhv, this.zzhhw, this.zzhhx);
    }
}
