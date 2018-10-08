package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.MetadataBuffer;

final class zzbkk extends zzbhi {
    private final zzn<MetadataBufferResult> zzfwc;

    public zzbkk(zzn<MetadataBufferResult> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbiq(status, null, false));
    }

    public final void zza(zzbmc zzbmc) throws RemoteException {
        this.zzfwc.setResult(new zzbiq(Status.zzfhp, new MetadataBuffer(zzbmc.zzgjm), false));
    }
}
