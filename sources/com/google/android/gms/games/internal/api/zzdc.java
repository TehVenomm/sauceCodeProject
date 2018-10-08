package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzdc extends zzdj {
    private /* synthetic */ String zzhif;
    private /* synthetic */ String zzhii;

    zzdc(zzcu zzcu, GoogleApiClient googleApiClient, String str, String str2) {
        this.zzhif = str;
        this.zzhii = str2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhif, this.zzhii);
    }
}
