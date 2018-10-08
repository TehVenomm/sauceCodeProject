package com.google.android.gms.internal;

import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.drive.DriveFile.DownloadProgressListener;

final class zzbjj implements DownloadProgressListener {
    private final zzcj<DownloadProgressListener> zzghr;

    public zzbjj(zzcj<DownloadProgressListener> zzcj) {
        this.zzghr = zzcj;
    }

    public final void onProgress(long j, long j2) {
        this.zzghr.zza(new zzbjk(this, j, j2));
    }
}
