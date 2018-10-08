package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzam extends zzat {
    private /* synthetic */ String zzhhf;
    private /* synthetic */ long zzhhl;
    private /* synthetic */ String zzhhm;

    zzam(zzaf zzaf, GoogleApiClient googleApiClient, String str, long j, String str2) {
        this.zzhhf = str;
        this.zzhhl = j;
        this.zzhhm = str2;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhf, this.zzhhl, this.zzhhm);
    }
}
