package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;

public final class zzblk extends zza {
    public static final Creator<zzblk> CREATOR = new zzbll();
    private DriveId zzgit;

    public zzblk(DriveId driveId) {
        this.zzgit = driveId;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgit, i, false);
        zzd.zzai(parcel, zze);
    }
}
