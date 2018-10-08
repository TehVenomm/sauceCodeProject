package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.Drive;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.DriveResource;
import com.google.android.gms.drive.DriveResource.MetadataResult;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.events.ChangeListener;
import com.google.android.gms.drive.events.zzj;
import java.util.ArrayList;
import java.util.Set;

public class zzbkc implements DriveResource {
    protected final DriveId zzgcx;

    public zzbkc(DriveId driveId) {
        this.zzgcx = driveId;
    }

    public PendingResult<Status> addChangeListener(GoogleApiClient googleApiClient, ChangeListener changeListener) {
        return ((zzbiw) googleApiClient.zza(Drive.zzdwq)).zza(googleApiClient, this.zzgcx, changeListener);
    }

    public PendingResult<Status> addChangeSubscription(GoogleApiClient googleApiClient) {
        return ((zzbiw) googleApiClient.zza(Drive.zzdwq)).zza(googleApiClient, this.zzgcx);
    }

    public PendingResult<Status> delete(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzbkh(this, googleApiClient));
    }

    public DriveId getDriveId() {
        return this.zzgcx;
    }

    public PendingResult<MetadataResult> getMetadata(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzbkd(this, googleApiClient, false));
    }

    public PendingResult<MetadataBufferResult> listParents(GoogleApiClient googleApiClient) {
        return googleApiClient.zzd(new zzbke(this, googleApiClient));
    }

    public PendingResult<Status> removeChangeListener(GoogleApiClient googleApiClient, ChangeListener changeListener) {
        return ((zzbiw) googleApiClient.zza(Drive.zzdwq)).zzb(googleApiClient, this.zzgcx, changeListener);
    }

    public PendingResult<Status> removeChangeSubscription(GoogleApiClient googleApiClient) {
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        DriveId driveId = this.zzgcx;
        zzbp.zzbh(zzj.zza(1, driveId));
        zzbp.zza(zzbiw.isConnected(), (Object) "Client must be connected");
        return googleApiClient.zze(new zzbja(zzbiw, googleApiClient, driveId, 1));
    }

    public PendingResult<Status> setParents(GoogleApiClient googleApiClient, Set<DriveId> set) {
        if (set != null) {
            return googleApiClient.zze(new zzbkf(this, googleApiClient, new ArrayList(set)));
        }
        throw new IllegalArgumentException("ParentIds must be provided.");
    }

    public PendingResult<Status> trash(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzbki(this, googleApiClient));
    }

    public PendingResult<Status> untrash(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzbkj(this, googleApiClient));
    }

    public PendingResult<MetadataResult> updateMetadata(GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet) {
        if (metadataChangeSet != null) {
            return googleApiClient.zze(new zzbkg(this, googleApiClient, metadataChangeSet));
        }
        throw new IllegalArgumentException("ChangeSet must be provided.");
    }
}
