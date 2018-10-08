package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;

public final class zzbht implements Creator<zzbhs> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        MetadataBundle metadataBundle = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        String str = null;
        DriveId driveId = null;
        Integer num = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    metadataBundle = (MetadataBundle) zzb.zza(parcel, readInt, MetadataBundle.CREATOR);
                    break;
                case 3:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 4:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 5:
                    driveId = (DriveId) zzb.zza(parcel, readInt, DriveId.CREATOR);
                    break;
                case 6:
                    num = zzb.zzh(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbhs(metadataBundle, i, str, driveId, num);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbhs[i];
    }
}
