package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.DriveId;

final class zzbig extends zzbip {
    private /* synthetic */ String zzggp;

    zzbig(zzbid zzbid, GoogleApiClient googleApiClient, String str) {
        this.zzggp = str;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbkx(DriveId.zzgo(this.zzggp), false), new zzbin(this));
    }
}
