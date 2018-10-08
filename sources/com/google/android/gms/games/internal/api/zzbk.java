package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbk extends zzbq {
    private /* synthetic */ boolean zzhgy;
    private /* synthetic */ int zzhhe;
    private /* synthetic */ int[] zzhhq;

    zzbk(zzbh zzbh, GoogleApiClient googleApiClient, int[] iArr, int i, boolean z) {
        this.zzhhq = iArr;
        this.zzhhe = i;
        this.zzhgy = z;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhq, this.zzhhe, this.zzhgy);
    }
}
