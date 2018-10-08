package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzckg implements Creator<zzckf> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        zzckr zzckr = null;
        int zzd = zzb.zzd(parcel);
        boolean z = false;
        String str = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 2:
                    zzckr = (zzckr) zzb.zza(parcel, readInt, zzckr.CREATOR);
                    break;
                case 3:
                    z = zzb.zzc(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzckf(str, zzckr, z);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzckf[i];
    }
}
