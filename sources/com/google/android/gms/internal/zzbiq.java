package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DriveApi.MetadataBufferResult;
import com.google.android.gms.drive.MetadataBuffer;

final class zzbiq implements MetadataBufferResult {
    private final Status mStatus;
    private final MetadataBuffer zzggr;
    private final boolean zzggs;

    public zzbiq(Status status, MetadataBuffer metadataBuffer, boolean z) {
        this.mStatus = status;
        this.zzggr = metadataBuffer;
        this.zzggs = z;
    }

    public final MetadataBuffer getMetadataBuffer() {
        return this.zzggr;
    }

    public final Status getStatus() {
        return this.mStatus;
    }

    public final void release() {
        if (this.zzggr != null) {
            this.zzggr.release();
        }
    }
}
