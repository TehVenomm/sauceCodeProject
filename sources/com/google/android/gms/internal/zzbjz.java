package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.drive.DrivePreferencesApi.FileUploadPreferencesResult;

final class zzbjz extends zzbhi {
    private final zzn<FileUploadPreferencesResult> zzfwc;
    private /* synthetic */ zzbjw zzgic;

    private zzbjz(zzbjw zzbjw, zzn<FileUploadPreferencesResult> zzn) {
        this.zzgic = zzbjw;
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(new zzbka(this.zzgic, status, null));
    }

    public final void zza(zzblq zzblq) throws RemoteException {
        this.zzfwc.setResult(new zzbka(this.zzgic, Status.zzfhp, zzblq.zzgiz));
    }
}
