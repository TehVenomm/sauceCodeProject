package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzbhw extends zza {
    public static final Creator<zzbhw> CREATOR = new zzbhx();
    private MetadataBundle zzggg;
    private DriveId zzggi;

    public zzbhw(DriveId driveId, MetadataBundle metadataBundle) {
        this.zzggi = (DriveId) zzbp.zzu(driveId);
        this.zzggg = (MetadataBundle) zzbp.zzu(metadataBundle);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzggi, i, false);
        zzd.zza(parcel, 3, this.zzggg, i, false);
        zzd.zzai(parcel, zze);
    }
}
