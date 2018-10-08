package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.snapshot.SnapshotMetadata;

final class zzcf extends zzcj {
    private /* synthetic */ SnapshotMetadata zzhia;

    zzcf(zzcb zzcb, GoogleApiClient googleApiClient, SnapshotMetadata snapshotMetadata) {
        this.zzhia = snapshotMetadata;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzi(this, this.zzhia.getSnapshotId());
    }
}
