package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbj extends zzbo {
    private /* synthetic */ String zzhho;
    private /* synthetic */ String zzhhp;

    zzbj(zzbh zzbh, GoogleApiClient googleApiClient, String str, String str2) {
        this.zzhho = str;
        this.zzhhp = str2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzb((zzn) this, this.zzhho, this.zzhhp);
    }
}
