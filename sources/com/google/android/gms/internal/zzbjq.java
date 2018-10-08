package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveFolder.DriveFolderResult;

final class zzbjq extends zzbhi {
    private final zzn<DriveFolderResult> zzfwc;

    public zzbjq(zzn<DriveFolderResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbjt(status, null));
    }

    public final void zza(zzblu zzblu) throws RemoteException {
        this.zzfwc.setResult(new zzbjt(Status.zzfhp, new zzbjm(zzblu.zzgfw)));
    }
}
