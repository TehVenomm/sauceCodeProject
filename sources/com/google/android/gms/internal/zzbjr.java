package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.drive.DriveFolder.DriveFileResult;

final class zzbjr implements DriveFileResult {
    private final Status mStatus;
    private final DriveFile zzghz;

    public zzbjr(Status status, DriveFile driveFile) {
        this.mStatus = status;
        this.zzghz = driveFile;
    }

    public final DriveFile getDriveFile() {
        return this.zzghz;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
