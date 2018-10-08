package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzde extends zzdn {
    private /* synthetic */ int zzhik;
    private /* synthetic */ int[] zzhil;

    zzde(zzcu zzcu, GoogleApiClient googleApiClient, int i, int[] iArr) {
        this.zzhik = i;
        this.zzhil = iArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhik, this.zzhil);
    }
}
