package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import java.util.List;

public final class zzbnd extends zza {
    public static final Creator<zzbnd> CREATOR = new zzbne();
    private DriveId zzgjw;
    private List<DriveId> zzgjx;

    public zzbnd(DriveId driveId, List<DriveId> list) {
        this.zzgjw = driveId;
        this.zzgjx = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjw, i, false);
        zzd.zzc(parcel, 3, this.zzgjx, false);
        zzd.zzai(parcel, zze);
    }
}
