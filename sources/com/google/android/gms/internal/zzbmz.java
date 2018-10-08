package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.events.zzp;

public final class zzbmz extends zza {
    public static final Creator<zzbmz> CREATOR = new zzbna();
    private int zzfxs;
    private DriveId zzgcx;
    private zzp zzgfv;

    public zzbmz(DriveId driveId, int i) {
        this(driveId, i, null);
    }

    zzbmz(DriveId driveId, int i, zzp zzp) {
        this.zzgcx = driveId;
        this.zzfxs = i;
        this.zzgfv = zzp;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgcx, i, false);
        zzd.zzc(parcel, 3, this.zzfxs);
        zzd.zza(parcel, 4, this.zzgfv, i, false);
        zzd.zzai(parcel, zze);
    }
}
