package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.drive.DriveFile.DownloadProgressListener;
import com.google.android.gms.drive.DriveId;

public final class zzbjh extends zzbkc implements DriveFile {
    public zzbjh(DriveId driveId) {
        super(driveId);
    }

    public final PendingResult<DriveContentsResult> open(GoogleApiClient googleApiClient, int i, DownloadProgressListener downloadProgressListener) {
        if (i == DriveFile.MODE_READ_ONLY || i == DriveFile.MODE_WRITE_ONLY || i == DriveFile.MODE_READ_WRITE) {
            return googleApiClient.zzd(new zzbji(this, googleApiClient, i, downloadProgressListener == null ? null : new zzbjj(googleApiClient.zzp(downloadProgressListener))));
        }
        throw new IllegalArgumentException("Invalid mode provided.");
    }
}
