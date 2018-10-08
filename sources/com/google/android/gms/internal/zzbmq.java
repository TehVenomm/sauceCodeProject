package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;

public final class zzbmq extends zza {
    public static final Creator<zzbmq> CREATOR = new zzbmr();
    private int zzgcw;
    private DriveId zzgfw;
    private int zzgjr;

    public zzbmq(DriveId driveId, int i, int i2) {
        this.zzgfw = driveId;
        this.zzgcw = i;
        this.zzgjr = i2;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfw, i, false);
        zzd.zzc(parcel, 3, this.zzgcw);
        zzd.zzc(parcel, 4, this.zzgjr);
        zzd.zzai(parcel, zze);
    }
}
