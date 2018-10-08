package com.google.android.gms.drive.query.internal;

import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import java.util.Set;

final class zzi {
    static MetadataField<?> zza(MetadataBundle metadataBundle) {
        Set zzanv = metadataBundle.zzanv();
        if (zzanv.size() == 1) {
            return (MetadataField) zzanv.iterator().next();
        }
        throw new IllegalArgumentException("bundle should have exactly 1 populated field");
    }
}
