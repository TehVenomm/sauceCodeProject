package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzaq;
import com.google.android.gms.drive.DriveFile.DownloadProgressListener;

final class zzbji extends zzbim {
    private /* synthetic */ int zzggo;
    private /* synthetic */ DownloadProgressListener zzghp;
    private /* synthetic */ zzbjh zzghq;

    zzbji(zzbjh zzbjh, GoogleApiClient googleApiClient, int i, DownloadProgressListener downloadProgressListener) {
        this.zzghq = zzbjh;
        this.zzggo = i;
        this.zzghp = downloadProgressListener;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zza(zzaq.zzaj(((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbmq(this.zzghq.getDriveId(), this.zzggo, 0), new zzbms(this, this.zzghp)).zzgij));
    }
}
