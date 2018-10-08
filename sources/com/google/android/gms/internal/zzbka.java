package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DrivePreferencesApi.FileUploadPreferencesResult;
import com.google.android.gms.drive.FileUploadPreferences;

final class zzbka implements FileUploadPreferencesResult {
    private final Status mStatus;
    private final FileUploadPreferences zzgie;

    private zzbka(zzbjw zzbjw, Status status, FileUploadPreferences fileUploadPreferences) {
        this.mStatus = status;
        this.zzgie = fileUploadPreferences;
    }

    public final FileUploadPreferences getFileUploadPreferences() {
        return this.zzgie;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
