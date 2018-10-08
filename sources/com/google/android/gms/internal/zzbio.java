package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveApi.DriveIdResult;
import com.google.android.gms.drive.DriveId;

final class zzbio implements DriveIdResult {
    private final Status mStatus;
    private final DriveId zzgcx;

    public zzbio(Status status, DriveId driveId) {
        this.mStatus = status;
        this.zzgcx = driveId;
    }

    public final DriveId getDriveId() {
        return this.zzgcx;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
