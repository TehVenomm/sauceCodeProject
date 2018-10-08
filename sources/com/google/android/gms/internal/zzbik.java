package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;

final class zzbik extends zzbhi {
    private final zzn<DriveContentsResult> zzfwc;

    public zzbik(zzn<DriveContentsResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbil(status, null));
    }

    public final void zza(zzblo zzblo) throws RemoteException {
        this.zzfwc.setResult(new zzbil(Status.zzfhp, new zzbjc(zzblo.zzghj)));
    }
}
