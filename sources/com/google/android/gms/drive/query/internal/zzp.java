package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.metadata.SearchableCollectionMetadataField;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.metadata.zzb;
import java.util.Collection;
import java.util.Collections;

public final class zzp<T> extends zza {
    public static final zzq CREATOR = new zzq();
    private MetadataBundle zzgnh;
    private final zzb<T> zzgnu;

    public zzp(SearchableCollectionMetadataField<T> searchableCollectionMetadataField, T t) {
        this(MetadataBundle.zzb(searchableCollectionMetadataField, Collections.singleton(t)));
    }

    zzp(MetadataBundle metadataBundle) {
        this.zzgnh = metadataBundle;
        this.zzgnu = (zzb) zzi.zza(metadataBundle);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgnh, i, false);
        zzd.zzai(parcel, zze);
    }

    public final <F> F zza(zzj<F> zzj) {
        return zzj.zza(this.zzgnu, ((Collection) this.zzgnh.zza(this.zzgnu)).iterator().next());
    }
}
