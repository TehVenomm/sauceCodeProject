package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbw extends zzbx {
    private /* synthetic */ int zzhhe;
    private /* synthetic */ int zzhht;
    private /* synthetic */ int zzhhu;

    zzbw(zzbt zzbt, GoogleApiClient googleApiClient, int i, int i2, int i3) {
        this.zzhht = i;
        this.zzhhu = i2;
        this.zzhhe = i3;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhht, this.zzhhu, this.zzhhe);
    }
}
