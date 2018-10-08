package com.google.android.gms.internal;

import com.google.android.gms.common.api.BooleanResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.CreateFileActivityBuilder;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveApi;
import com.google.android.gms.drive.DriveApi.DriveContentsResult;
import com.google.android.gms.drive.DriveApi.DriveIdResult;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.drive.DriveFolder;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.OpenFileActivityBuilder;
import com.google.android.gms.drive.query.Query;
import java.util.List;

public final class zzbid implements DriveApi {
    public final PendingResult<Status> cancelPendingActions(GoogleApiClient googleApiClient, List<String> list) {
        boolean z = true;
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        zzbp.zzbh(list != null);
        if (list.isEmpty()) {
            z = false;
        }
        zzbp.zzbh(z);
        zzbp.zza(zzbiw.isConnected(), (Object) "Client must be connected");
        return googleApiClient.zze(new zzbjb(zzbiw, googleApiClient, list));
    }

    public final PendingResult<DriveIdResult> fetchDriveId(GoogleApiClient googleApiClient, String str) {
        return googleApiClient.zzd(new zzbig(this, googleApiClient, str));
    }

    public final DriveFolder getAppFolder(GoogleApiClient googleApiClient) {
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        if (zzbiw.zzanl()) {
            DriveId zzank = zzbiw.zzank();
            return zzank != null ? new zzbjm(zzank) : null;
        } else {
            throw new IllegalStateException("Client is not yet connected");
        }
    }

    public final DriveFile getFile(GoogleApiClient googleApiClient, DriveId driveId) {
        if (driveId == null) {
            throw new IllegalArgumentException("Id must be provided.");
        } else if (googleApiClient.isConnected()) {
            return new zzbjh(driveId);
        } else {
            throw new IllegalStateException("Client must be connected");
        }
    }

    public final DriveFolder getFolder(GoogleApiClient googleApiClient, DriveId driveId) {
        if (driveId == null) {
            throw new IllegalArgumentException("Id must be provided.");
        } else if (googleApiClient.isConnected()) {
            return new zzbjm(driveId);
        } else {
            throw new IllegalStateException("Client must be connected");
        }
    }

    public final DriveFolder getRootFolder(GoogleApiClient googleApiClient) {
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        if (zzbiw.zzanl()) {
            DriveId zzanj = zzbiw.zzanj();
            return zzanj != null ? new zzbjm(zzanj) : null;
        } else {
            throw new IllegalStateException("Client is not yet connected");
        }
    }

    public final PendingResult<BooleanResult> isAutobackupEnabled(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzbii(this, googleApiClient));
    }

    public final CreateFileActivityBuilder newCreateFileActivityBuilder() {
        return new CreateFileActivityBuilder();
    }

    public final PendingResult<DriveContentsResult> newDriveContents(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzbif(this, googleApiClient, DriveFile.MODE_WRITE_ONLY));
    }

    public final OpenFileActivityBuilder newOpenFileActivityBuilder() {
        return new OpenFileActivityBuilder();
    }

    public final PendingResult<MetadataBufferResult> query(GoogleApiClient googleApiClient, Query query) {
        if (query != null) {
            return googleApiClient.zzd(new zzbie(this, googleApiClient, query));
        }
        throw new IllegalArgumentException("Query must be provided.");
    }

    public final PendingResult<Status> requestSync(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzbih(this, googleApiClient));
    }
}
