package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzbhs extends zza {
    public static final Creator<zzbhs> CREATOR = new zzbht();
    private String zzehi;
    private int zzgcv;
    private DriveId zzgeg;
    private MetadataBundle zzggg;
    private Integer zzggh;

    public zzbhs(MetadataBundle metadataBundle, int i, String str, DriveId driveId, Integer num) {
        this.zzggg = metadataBundle;
        this.zzgcv = i;
        this.zzehi = str;
        this.zzgeg = driveId;
        this.zzggh = num;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzggg, i, false);
        zzd.zzc(parcel, 3, this.zzgcv);
        zzd.zza(parcel, 4, this.zzehi, false);
        zzd.zza(parcel, 5, this.zzgeg, i, false);
        zzd.zza(parcel, 6, this.zzggh, false);
        zzd.zzai(parcel, zze);
    }
}
