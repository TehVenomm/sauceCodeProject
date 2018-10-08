package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveApi.DriveIdResult;

final class zzbin extends zzbhi {
    private final zzn<DriveIdResult> zzfwc;

    public zzbin(zzn<DriveIdResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbio(status, null));
    }

    public final void zza(zzblu zzblu) throws RemoteException {
        this.zzfwc.setResult(new zzbio(Status.zzfhp, zzblu.zzgfw));
    }

    public final void zza(zzbmf zzbmf) throws RemoteException {
        this.zzfwc.setResult(new zzbio(Status.zzfhp, new zzbhy(zzbmf.zzggg).getDriveId()));
    }
}
