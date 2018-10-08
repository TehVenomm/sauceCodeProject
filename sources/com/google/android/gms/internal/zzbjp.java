package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveFolder.DriveFileResult;

final class zzbjp extends zzbhi {
    private final zzn<DriveFileResult> zzfwc;

    public zzbjp(zzn<DriveFileResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbjr(status, null));
    }

    public final void zza(zzblu zzblu) throws RemoteException {
        this.zzfwc.setResult(new zzbjr(Status.zzfhp, new zzbjh(zzblu.zzgfw)));
    }
}
