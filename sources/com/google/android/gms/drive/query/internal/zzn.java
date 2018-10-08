package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.SearchableMetadataField;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzn<T> extends zza {
    public static final zzo CREATOR = new zzo();
    private MetadataBundle zzgnh;
    private MetadataField<T> zzgni;

    public zzn(SearchableMetadataField<T> searchableMetadataField, T t) {
        this(MetadataBundle.zzb(searchableMetadataField, t));
    }

    zzn(MetadataBundle metadataBundle) {
        this.zzgnh = metadataBundle;
        this.zzgni = zzi.zza(metadataBundle);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgnh, i, false);
        zzd.zzai(parcel, zze);
    }

    public final <F> F zza(zzj<F> zzj) {
        return zzj.zzd(this.zzgni, this.zzgnh.zza(this.zzgni));
    }
}
