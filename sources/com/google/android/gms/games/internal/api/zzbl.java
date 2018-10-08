package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbl extends zzbq {
    private /* synthetic */ boolean zzhgy;
    private /* synthetic */ String[] zzhhr;

    zzbl(zzbh zzbh, GoogleApiClient googleApiClient, boolean z, String[] strArr) {
        this.zzhgy = z;
        this.zzhhr = strArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzb((zzn) this, this.zzhgy, this.zzhhr);
    }
}
