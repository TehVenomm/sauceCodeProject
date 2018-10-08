package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzbnm extends zza {
    public static final Creator<zzbnm> CREATOR = new zzbnn();
    private DriveId zzgfw;
    private MetadataBundle zzgfx;

    public zzbnm(DriveId driveId, MetadataBundle metadataBundle) {
        this.zzgfw = driveId;
        this.zzgfx = metadataBundle;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfw, i, false);
        zzd.zza(parcel, 3, this.zzgfx, i, false);
        zzd.zzai(parcel, zze);
    }
}
