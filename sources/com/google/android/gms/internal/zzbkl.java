package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveResource.MetadataResult;

final class zzbkl extends zzbhi {
    private final zzn<MetadataResult> zzfwc;

    public zzbkl(zzn<MetadataResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbkm(status, null));
    }

    public final void zza(zzbmf zzbmf) throws RemoteException {
        this.zzfwc.setResult(new zzbkm(Status.zzfhp, new zzbhy(zzbmf.zzggg)));
    }
}
