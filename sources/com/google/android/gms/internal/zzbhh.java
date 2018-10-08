package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.events.zze;
import com.google.android.gms.drive.events.zzp;
import com.google.android.gms.drive.events.zzt;

public final class zzbhh implements Creator<zzbhg> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        DriveId driveId = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        zze zze = null;
        zzt zzt = null;
        zzp zzp = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    driveId = (DriveId) zzb.zza(parcel, readInt, DriveId.CREATOR);
                    break;
                case 3:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 4:
                    zze = (zze) zzb.zza(parcel, readInt, zze.CREATOR);
                    break;
                case 5:
                    zzt = (zzt) zzb.zza(parcel, readInt, zzt.CREATOR);
                    break;
                case 6:
                    zzp = (zzp) zzb.zza(parcel, readInt, zzp.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbhg(driveId, i, zze, zzt, zzp);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbhg[i];
    }
}
