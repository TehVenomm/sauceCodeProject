package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzbdj implements Creator<zzbdi> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        zzbdd zzbdd = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        Parcel parcel2 = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    parcel2 = zzb.zzad(parcel, readInt);
                    break;
                case 3:
                    zzbdd = (zzbdd) zzb.zza(parcel, readInt, zzbdd.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzbdi(i, parcel2, zzbdd);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzbdi[i];
    }
}
