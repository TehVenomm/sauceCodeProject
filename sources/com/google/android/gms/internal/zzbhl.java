package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.drive.zzp;

public final class zzbhl extends zza {
    public static final Creator<zzbhl> CREATOR = new zzbhm();
    private String zzgdt;
    private boolean zzgdu;
    private boolean zzgdy;
    private DriveId zzgfw;
    private MetadataBundle zzgfx;
    private zzc zzgfy;
    private int zzgfz;
    private int zzgga;
    private boolean zzggb;

    public zzbhl(DriveId driveId, MetadataBundle metadataBundle, int i, boolean z, zzp zzp) {
        this(driveId, metadataBundle, null, zzp.zzamu(), zzp.zzamt(), zzp.zzamv(), i, z, zzp.zzamz());
    }

    zzbhl(DriveId driveId, MetadataBundle metadataBundle, zzc zzc, boolean z, String str, int i, int i2, boolean z2, boolean z3) {
        this.zzgfw = driveId;
        this.zzgfx = metadataBundle;
        this.zzgfy = zzc;
        this.zzgdu = z;
        this.zzgdt = str;
        this.zzgfz = i;
        this.zzgga = i2;
        this.zzggb = z2;
        this.zzgdy = z3;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfw, i, false);
        zzd.zza(parcel, 3, this.zzgfx, i, false);
        zzd.zza(parcel, 4, this.zzgfy, i, false);
        zzd.zza(parcel, 5, this.zzgdu);
        zzd.zza(parcel, 6, this.zzgdt, false);
        zzd.zzc(parcel, 7, this.zzgfz);
        zzd.zzc(parcel, 8, this.zzgga);
        zzd.zza(parcel, 9, this.zzggb);
        zzd.zza(parcel, 10, this.zzgdy);
        zzd.zzai(parcel, zze);
    }
}
