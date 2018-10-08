package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzaj extends zzar {
    private /* synthetic */ boolean zzhgy;
    private /* synthetic */ String zzhhf;
    private /* synthetic */ int zzhhg;
    private /* synthetic */ int zzhhh;
    private /* synthetic */ int zzhhi;

    zzaj(zzaf zzaf, GoogleApiClient googleApiClient, String str, int i, int i2, int i3, boolean z) {
        this.zzhhf = str;
        this.zzhhg = i;
        this.zzhhh = i2;
        this.zzhhi = i3;
        this.zzhgy = z;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza(this, this.zzhhf, this.zzhhg, this.zzhhh, this.zzhhi, this.zzhgy);
    }
}
