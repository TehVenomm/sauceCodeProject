package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.MetadataBuffer;

final class zzbis extends zzbhi {
    private final zzn<MetadataBufferResult> zzfwc;

    public zzbis(zzn<MetadataBufferResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbiq(status, null, false));
    }

    public final void zza(zzbma zzbma) throws RemoteException {
        this.zzfwc.setResult(new zzbiq(Status.zzfhp, new MetadataBuffer(zzbma.zzgjl), zzbma.zzggs));
    }
}
