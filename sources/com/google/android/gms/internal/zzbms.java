package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;
import com.google.android.gms.drive.DriveFile.DownloadProgressListener;

final class zzbms extends zzbhi {
    private final zzn<DriveContentsResult> zzfwc;
    private final DownloadProgressListener zzgjs;

    zzbms(zzn<DriveContentsResult> zzn, DownloadProgressListener downloadProgressListener) {
        this.zzfwc = zzn;
        this.zzgjs = downloadProgressListener;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbil(status, null));
    }

    public final void zza(zzblo zzblo) throws RemoteException {
        this.zzfwc.setResult(new zzbil(zzblo.zzgiy ? new Status(-1) : Status.zzfhp, new zzbjc(zzblo.zzghj)));
    }

    public final void zza(zzbls zzbls) throws RemoteException {
        if (this.zzgjs != null) {
            this.zzgjs.onProgress(zzbls.zzgjb, zzbls.zzgjc);
        }
    }
}
