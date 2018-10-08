package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.zza;
import java.util.List;

public final class zzbln implements Creator<zzblm> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int zzd = zzb.zzd(parcel);
        List list = null;
        DataHolder dataHolder = null;
        boolean z = false;
        zza zza = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 2:
                    dataHolder = (DataHolder) zzb.zza(parcel, readInt, DataHolder.CREATOR);
                    break;
                case 3:
                    list = zzb.zzc(parcel, readInt, DriveId.CREATOR);
                    break;
                case 4:
                    zza = (zza) zzb.zza(parcel, readInt, zza.CREATOR);
                    break;
                case 5:
                    z = zzb.zzc(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzblm(dataHolder, list, zza, z);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzblm[i];
    }
}
