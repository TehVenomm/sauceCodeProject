package com.google.android.gms.internal;

import com.google.android.gms.drive.Metadata;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzbhy extends Metadata {
    private final MetadataBundle zzggm;

    public zzbhy(MetadataBundle metadataBundle) {
        this.zzggm = metadataBundle;
    }

    public final /* synthetic */ Object freeze() {
        return new zzbhy(this.zzggm.zzanu());
    }

    public final boolean isDataValid() {
        return this.zzggm != null;
    }

    public final String toString() {
        String valueOf = String.valueOf(this.zzggm);
        return new StringBuilder(String.valueOf(valueOf).length() + 17).append("Metadata [mImpl=").append(valueOf).append("]").toString();
    }

    public final <T> T zza(MetadataField<T> metadataField) {
        return this.zzggm.zza(metadataField);
    }
}
