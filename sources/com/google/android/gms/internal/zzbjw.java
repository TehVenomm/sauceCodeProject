package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DrivePreferencesApi;
import com.google.android.gms.drive.DrivePreferencesApi.FileUploadPreferencesResult;
import com.google.android.gms.drive.FileUploadPreferences;

public final class zzbjw implements DrivePreferencesApi {
    public final PendingResult<FileUploadPreferencesResult> getFileUploadPreferences(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzbjx(this, googleApiClient));
    }

    public final PendingResult<Status> setFileUploadPreferences(GoogleApiClient googleApiClient, FileUploadPreferences fileUploadPreferences) {
        if (fileUploadPreferences instanceof zzbkv) {
            return googleApiClient.zze(new zzbjy(this, googleApiClient, (zzbkv) fileUploadPreferences));
        }
        throw new IllegalArgumentException("Invalid preference value");
    }
}
