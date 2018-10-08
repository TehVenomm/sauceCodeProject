package com.google.android.gms.drive.query;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.drive.DriveSpace;
import com.google.android.gms.drive.query.internal.zzr;
import java.util.List;

public final class zzb implements Creator<Query> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        boolean z = false;
        zzr zzr = null;
        int zzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        String str = null;
        SortOrder sortOrder = null;
        List list = null;
        List list2 = null;
        boolean z2 = false;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    zzr = (zzr) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, readInt, zzr.CREATOR);
                    break;
                case 3:
                    str = com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, readInt);
                    break;
                case 4:
                    sortOrder = (SortOrder) com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, readInt, SortOrder.CREATOR);
                    break;
                case 5:
                    list = com.google.android.gms.common.internal.safeparcel.zzb.zzac(parcel, readInt);
                    break;
                case 6:
                    z = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, readInt);
                    break;
                case 7:
                    list2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, readInt, DriveSpace.CREATOR);
                    break;
                case 8:
                    z2 = com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, readInt);
                    break;
                default:
                    com.google.android.gms.common.internal.safeparcel.zzb.zzb(parcel, readInt);
                    break;
            }
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzaf(parcel, zzd);
        return new Query(zzr, str, sortOrder, list, z, list2, z2);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new Query[i];
    }
}
