package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzaz extends zzbf {
    private /* synthetic */ String zzezj;
    private /* synthetic */ boolean zzhgy;

    zzaz(zzax zzax, GoogleApiClient googleApiClient, String str, boolean z) {
        this.zzezj = str;
        this.zzhgy = z;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzezj, this.zzhgy);
    }
}
