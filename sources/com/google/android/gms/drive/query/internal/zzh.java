package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzh implements Creator<FilterHolder> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        zzb zzb = null;
        int zzd = zzb.zzd(parcel);
        zzd zzd2 = null;
        zzr zzr = null;
        zzv zzv = null;
        zzp zzp = null;
        zzt zzt = null;
        zzn zzn = null;
        zzl zzl = null;
        zzz zzz = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    zzb = (zzb) zzb.zza(parcel, readInt, zzb.CREATOR);
                    break;
                case 2:
                    zzd2 = (zzd) zzb.zza(parcel, readInt, zzd.CREATOR);
                    break;
                case 3:
                    zzr = (zzr) zzb.zza(parcel, readInt, zzr.CREATOR);
                    break;
                case 4:
                    zzv = (zzv) zzb.zza(parcel, readInt, zzv.CREATOR);
                    break;
                case 5:
                    zzp = (zzp) zzb.zza(parcel, readInt, zzp.CREATOR);
                    break;
                case 6:
                    zzt = (zzt) zzb.zza(parcel, readInt, zzt.CREATOR);
                    break;
                case 7:
                    zzn = (zzn) zzb.zza(parcel, readInt, zzn.CREATOR);
                    break;
                case 8:
                    zzl = (zzl) zzb.zza(parcel, readInt, zzl.CREATOR);
                    break;
                case 9:
                    zzz = (zzz) zzb.zza(parcel, readInt, zzz.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new FilterHolder(zzb, zzd2, zzr, zzv, zzp, zzt, zzn, zzl, zzz);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new FilterHolder[i];
    }
}
