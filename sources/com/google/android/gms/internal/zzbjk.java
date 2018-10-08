package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcm;
import com.google.android.gms.drive.DriveFile.DownloadProgressListener;

final class zzbjk implements zzcm<DownloadProgressListener> {
    private /* synthetic */ long zzghs;
    private /* synthetic */ long zzght;

    zzbjk(zzbjj zzbjj, long j, long j2) {
        this.zzghs = j;
        this.zzght = j2;
    }

    public final void zzagw() {
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((DownloadProgressListener) obj).onProgress(this.zzghs, this.zzght);
    }
}
