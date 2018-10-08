package com.google.android.gms.internal;

import com.google.android.gms.common.api.Releasable;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;
import com.google.android.gms.drive.DriveContents;

final class zzbil implements Releasable, DriveContentsResult {
    private final Status mStatus;
    private final DriveContents zzgda;

    public zzbil(Status status, DriveContents driveContents) {
        this.mStatus = status;
        this.zzgda = driveContents;
    }

    public final DriveContents getDriveContents() {
        return this.zzgda;
    }

    public final Status getStatus() {
        return this.mStatus;
    }

    public final void release() {
        if (this.zzgda != null) {
            this.zzgda.zzamr();
        }
    }
}
