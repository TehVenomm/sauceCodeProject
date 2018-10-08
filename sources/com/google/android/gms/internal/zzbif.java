package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.DriveFile;

final class zzbif extends zzbim {
    private /* synthetic */ int zzggo = DriveFile.MODE_WRITE_ONLY;

    zzbif(zzbid zzbid, GoogleApiClient googleApiClient, int i) {
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbhp(this.zzggo), new zzbik(this));
    }
}
