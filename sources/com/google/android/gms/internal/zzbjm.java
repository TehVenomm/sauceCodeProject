package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.DriveContents;
import com.google.android.gms.drive.DriveFolder;
import com.google.android.gms.drive.DriveFolder.DriveFileResult;
import com.google.android.gms.drive.DriveFolder.DriveFolderResult;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.ExecutionOptions;
import com.google.android.gms.drive.ExecutionOptions.Builder;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.metadata.internal.zzk;
import com.google.android.gms.drive.query.Filters;
import com.google.android.gms.drive.query.Query;
import com.google.android.gms.drive.query.SearchableField;
import com.google.android.gms.drive.zzm;
import com.google.android.gms.drive.zzo;

public final class zzbjm extends zzbkc implements DriveFolder {
    public zzbjm(DriveId driveId) {
        super(driveId);
    }

    private final PendingResult<DriveFileResult> zza(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, DriveContents driveContents, zzm zzm) {
        ExecutionOptions executionOptions = zzm == null ? (zzm) new zzo().build() : zzm;
        if (metadataChangeSet == null) {
            throw new IllegalArgumentException("MetadataChangeSet must be provided.");
        }
        zzk zzgs = zzk.zzgs(metadataChangeSet.getMimeType());
        if (zzgs == null || !zzgs.isFolder()) {
            int i;
            executionOptions.zze(googleApiClient);
            if (driveContents != null) {
                if (!(driveContents instanceof zzbjc)) {
                    throw new IllegalArgumentException("Only DriveContents obtained from the Drive API are accepted.");
                } else if (driveContents.getDriveId() != null) {
                    throw new IllegalArgumentException("Only DriveContents obtained through DriveApi.newDriveContents are accepted for file creation.");
                } else if (driveContents.zzams()) {
                    throw new IllegalArgumentException("DriveContents are already closed.");
                }
            }
            zzgs = zzk.zzgs(metadataChangeSet.getMimeType());
            if (driveContents == null) {
                i = (zzgs == null || !zzgs.zzanw()) ? 1 : 0;
            } else {
                i = driveContents.zzamq().getRequestId();
                driveContents.zzamr();
            }
            String zzamy = executionOptions.zzamy();
            MetadataChangeSet zza = zzamy != null ? metadataChangeSet.zza(zzbnr.zzgmd, zzamy) : metadataChangeSet;
            zzgs = zzk.zzgs(zza.getMimeType());
            int i2 = (zzgs == null || !zzgs.zzanw()) ? 0 : 1;
            return googleApiClient.zze(new zzbjn(this, googleApiClient, zza, i, i2, executionOptions));
        }
        throw new IllegalArgumentException("May not create folders using this method. Use DriveFolder.createFolder() instead of mime type application/vnd.google-apps.folder");
    }

    private static void zzb(MetadataChangeSet metadataChangeSet) {
        if (metadataChangeSet == null) {
            throw new IllegalArgumentException("MetadataChangeSet must be provided.");
        }
        zzk zzgs = zzk.zzgs(metadataChangeSet.getMimeType());
        if (zzgs != null) {
            Object obj = (zzgs.zzanw() || zzgs.isFolder()) ? null : 1;
            if (obj == null) {
                throw new IllegalArgumentException("May not create shortcut files using this method. Use DriveFolder.createShortcutFile() instead.");
            }
        }
    }

    public final PendingResult<DriveFileResult> createFile(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, DriveContents driveContents) {
        zzb(metadataChangeSet);
        return zza(googleApiClient, metadataChangeSet, driveContents, null);
    }

    public final PendingResult<DriveFileResult> createFile(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, DriveContents driveContents, ExecutionOptions executionOptions) {
        zzb(metadataChangeSet);
        Builder zzo = new zzo();
        if (executionOptions != null) {
            if (executionOptions.zzamv() != 0) {
                throw new IllegalStateException("May not set a conflict strategy for new file creation.");
            }
            String zzamt = executionOptions.zzamt();
            if (zzamt != null) {
                zzo.setTrackingTag(zzamt);
            }
            zzo.setNotifyOnCompletion(executionOptions.zzamu());
        }
        return zza(googleApiClient, metadataChangeSet, driveContents, (zzm) zzo.build());
    }

    public final PendingResult<DriveFolderResult> createFolder(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet) {
        if (metadataChangeSet == null) {
            throw new IllegalArgumentException("MetadataChangeSet must be provided.");
        } else if (metadataChangeSet.getMimeType() == null || metadataChangeSet.getMimeType().equals(DriveFolder.MIME_TYPE)) {
            return googleApiClient.zze(new zzbjo(this, googleApiClient, metadataChangeSet));
        } else {
            throw new IllegalArgumentException("The mimetype must be of type application/vnd.google-apps.folder");
        }
    }

    public final PendingResult<MetadataBufferResult> listChildren(GoogleApiClient googleApiClient) {
        return queryChildren(googleApiClient, null);
    }

    public final PendingResult<MetadataBufferResult> queryChildren(GoogleApiClient googleApiClient, Query query) {
        zzbid zzbid = new zzbid();
        Query.Builder addFilter = new Query.Builder().addFilter(Filters.in(SearchableField.PARENTS, getDriveId()));
        if (query != null) {
            if (query.getFilter() != null) {
                addFilter.addFilter(query.getFilter());
            }
            addFilter.setPageToken(query.getPageToken());
            addFilter.setSortOrder(query.getSortOrder());
        }
        return zzbid.query(googleApiClient, addFilter.build());
    }
}
