package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.DriveFile;

final class zzbjd extends zzbim {
    private /* synthetic */ zzbjc zzghm;

    zzbjd(zzbjc zzbjc, GoogleApiClient googleApiClient) {
        this.zzghm = zzbjc;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbmq(this.zzghm.getDriveId(), DriveFile.MODE_WRITE_ONLY, this.zzghm.zzghj.getRequestId()), new zzbms(this, null));
    }
}
