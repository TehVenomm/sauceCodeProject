package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.zzg;
import java.util.List;

public final class zzblt implements Creator<zzbls> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        long j = 0;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        List list = null;
        long j2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    j = zzb.zzi(parcel, readInt);
                    break;
                case 3:
                    j2 = zzb.zzi(parcel, readInt);
                    break;
                case 4:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 5:
                    list = zzb.zzc(parcel, readInt, zzg.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbls(j, j2, i, list);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbls[i];
    }
}
