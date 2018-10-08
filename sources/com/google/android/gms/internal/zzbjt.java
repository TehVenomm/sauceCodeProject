package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveFolder;
import com.google.android.gms.drive.DriveFolder.DriveFolderResult;

final class zzbjt implements DriveFolderResult {
    private final Status mStatus;
    private final DriveFolder zzgia;

    public zzbjt(Status status, DriveFolder driveFolder) {
        this.mStatus = status;
        this.zzgia = driveFolder;
    }

    public final DriveFolder getDriveFolder() {
        return this.zzgia;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
