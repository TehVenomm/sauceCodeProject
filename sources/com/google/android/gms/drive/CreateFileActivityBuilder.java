package com.google.android.gms.drive;

import android.content.IntentSender;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbhr;
import com.google.android.gms.internal.zzbjc;

public class CreateFileActivityBuilder {
    public static final String EXTRA_RESPONSE_DRIVE_ID = "response_drive_id";
    private final zzbhr zzgcz = new zzbhr(0);
    private DriveContents zzgda;
    private boolean zzgdb;

    public IntentSender build(GoogleApiClient googleApiClient) {
        zzbp.zzb(Boolean.valueOf(this.zzgdb), (Object) "Must call setInitialDriveContents to CreateFileActivityBuilder.");
        zzbp.zza(googleApiClient.isConnected(), (Object) "Client must be connected");
        if (this.zzgda != null) {
            this.zzgda.zzamr();
        }
        return this.zzgcz.build(googleApiClient);
    }

    public CreateFileActivityBuilder setActivityStartFolder(DriveId driveId) {
        this.zzgcz.zza(driveId);
        return this;
    }

    public CreateFileActivityBuilder setActivityTitle(String str) {
        this.zzgcz.zzgq(str);
        return this;
    }

    public CreateFileActivityBuilder setInitialDriveContents(DriveContents driveContents) {
        if (driveContents == null) {
            this.zzgcz.zzcp(1);
        } else if (!(driveContents instanceof zzbjc)) {
            throw new IllegalArgumentException("Only DriveContents obtained from the Drive API are accepted.");
        } else if (driveContents.getDriveId() != null) {
            throw new IllegalArgumentException("Only DriveContents obtained through DriveApi.newDriveContents are accepted for file creation.");
        } else if (driveContents.zzams()) {
            throw new IllegalArgumentException("DriveContents are already closed.");
        } else {
            this.zzgcz.zzcp(driveContents.zzamq().zzgcv);
            this.zzgda = driveContents;
        }
        this.zzgdb = true;
        return this;
    }

    public CreateFileActivityBuilder setInitialMetadata(MetadataChangeSet metadataChangeSet) {
        this.zzgcz.zza(metadataChangeSet);
        return this;
    }
}
