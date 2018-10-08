package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;
import com.google.android.gms.games.snapshot.SnapshotContents;
import com.google.android.gms.games.snapshot.SnapshotMetadataChange;

final class zzcg extends zzcn {
    private /* synthetic */ SnapshotMetadataChange zzhhz;
    private /* synthetic */ String zzhib;
    private /* synthetic */ String zzhic;
    private /* synthetic */ SnapshotContents zzhid;

    zzcg(zzcb zzcb, GoogleApiClient googleApiClient, String str, String str2, SnapshotMetadataChange snapshotMetadataChange, SnapshotContents snapshotContents) {
        this.zzhib = str;
        this.zzhic = str2;
        this.zzhhz = snapshotMetadataChange;
        this.zzhid = snapshotContents;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhib, this.zzhic, this.zzhhz, this.zzhid);
    }
}
