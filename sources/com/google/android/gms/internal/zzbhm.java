package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.zzc;

public final class zzbhm implements Creator<zzbhl> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        DriveId driveId = null;
        boolean z = false;
        int zzd = zzb.zzd(parcel);
        boolean z2 = true;
        MetadataBundle metadataBundle = null;
        zzc zzc = null;
        String str = null;
        boolean z3 = false;
        int i = 0;
        int i2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    driveId = (DriveId) zzb.zza(parcel, readInt, DriveId.CREATOR);
                    break;
                case 3:
                    metadataBundle = (MetadataBundle) zzb.zza(parcel, readInt, MetadataBundle.CREATOR);
                    break;
                case 4:
                    zzc = (zzc) zzb.zza(parcel, readInt, zzc.CREATOR);
                    break;
                case 5:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 6:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 7:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 8:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 9:
                    z3 = zzb.zzc(parcel, readInt);
                    break;
                case 10:
                    z2 = zzb.zzc(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbhl(driveId, metadataBundle, zzc, z, str, i, i2, z3, z2);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbhl[i];
    }
}
