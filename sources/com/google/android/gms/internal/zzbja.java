package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.DriveId;

final class zzbja extends zzbiv {
    private /* synthetic */ DriveId zzghg;
    private /* synthetic */ int zzghh = 1;

    zzbja(zzbiw zzbiw, GoogleApiClient googleApiClient, DriveId driveId, int i) {
        this.zzghg = driveId;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbmz(this.zzghg, this.zzghh), null, null, new zzbnf(this));
    }
}
