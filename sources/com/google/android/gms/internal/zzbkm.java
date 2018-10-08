package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveResource.MetadataResult;
import com.google.android.gms.drive.Metadata;

final class zzbkm implements MetadataResult {
    private final Status mStatus;
    private final Metadata zzgii;

    public zzbkm(Status status, Metadata metadata) {
        this.mStatus = status;
        this.zzgii = metadata;
    }

    public final Metadata getMetadata() {
        return this.zzgii;
    }

    public final Status getStatus() {
        return this.mStatus;
    }
}
