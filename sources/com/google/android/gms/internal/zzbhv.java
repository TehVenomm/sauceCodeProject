package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.zzc;

public final class zzbhv implements Creator<zzbhu> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        boolean z = false;
        DriveId driveId = null;
        int zzd = zzb.zzd(parcel);
        MetadataBundle metadataBundle = null;
        zzc zzc = null;
        Integer num = null;
        String str = null;
        String str2 = null;
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
                    num = zzb.zzh(parcel, readInt);
                    break;
                case 6:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 7:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 8:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 9:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 10:
                    str2 = zzb.zzq(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbhu(driveId, metadataBundle, zzc, num, z, str, i, i2, str2);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbhu[i];
    }
}
