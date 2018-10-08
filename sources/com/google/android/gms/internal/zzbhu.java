package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.drive.zzm;

public final class zzbhu extends zza {
    public static final Creator<zzbhu> CREATOR = new zzbhv();
    private String zzgdt;
    private String zzgdw;
    private zzc zzgfy;
    private MetadataBundle zzggg;
    private Integer zzggh;
    private DriveId zzggi;
    private boolean zzggj;
    private int zzggk;
    private int zzggl;

    public zzbhu(DriveId driveId, MetadataBundle metadataBundle, int i, int i2, zzm zzm) {
        this(driveId, metadataBundle, null, Integer.valueOf(i2), zzm.zzamu(), zzm.zzamt(), zzm.zzamv(), i, zzm.zzamx());
    }

    zzbhu(DriveId driveId, MetadataBundle metadataBundle, zzc zzc, Integer num, boolean z, String str, int i, int i2, String str2) {
        if (!(zzc == null || i2 == 0)) {
            zzbp.zzb(zzc.getRequestId() == i2, (Object) "inconsistent contents reference");
        }
        if ((num == null || num.intValue() == 0) && zzc == null && i2 == 0) {
            throw new IllegalArgumentException("Need a valid contents");
        }
        this.zzggi = (DriveId) zzbp.zzu(driveId);
        this.zzggg = (MetadataBundle) zzbp.zzu(metadataBundle);
        this.zzgfy = zzc;
        this.zzggh = num;
        this.zzgdt = str;
        this.zzggk = i;
        this.zzggj = z;
        this.zzggl = i2;
        this.zzgdw = str2;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzggi, i, false);
        zzd.zza(parcel, 3, this.zzggg, i, false);
        zzd.zza(parcel, 4, this.zzgfy, i, false);
        zzd.zza(parcel, 5, this.zzggh, false);
        zzd.zza(parcel, 6, this.zzggj);
        zzd.zza(parcel, 7, this.zzgdt, false);
        zzd.zzc(parcel, 8, this.zzggk);
        zzd.zzc(parcel, 9, this.zzggl);
        zzd.zza(parcel, 10, this.zzgdw, false);
        zzd.zzai(parcel, zze);
    }
}
