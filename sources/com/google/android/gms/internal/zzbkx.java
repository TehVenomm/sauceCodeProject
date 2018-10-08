package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;

public final class zzbkx extends zza {
    public static final Creator<zzbkx> CREATOR = new zzbky();
    private DriveId zzgfw;
    private boolean zzgir;

    public zzbkx(DriveId driveId, boolean z) {
        this.zzgfw = driveId;
        this.zzgir = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfw, i, false);
        zzd.zza(parcel, 3, this.zzgir);
        zzd.zzai(parcel, zze);
    }
}
