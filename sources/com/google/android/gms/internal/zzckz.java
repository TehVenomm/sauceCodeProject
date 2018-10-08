package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzckz implements Creator<zzcky> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        zzckr zzckr = null;
        int zzd = zzb.zzd(parcel);
        IBinder iBinder = null;
        boolean z = false;
        String[] strArr = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    iBinder = zzb.zzr(parcel, readInt);
                    break;
                case 2:
                    strArr = zzb.zzaa(parcel, readInt);
                    break;
                case 3:
                    zzckr = (zzckr) zzb.zza(parcel, readInt, zzckr.CREATOR);
                    break;
                case 4:
                    z = zzb.zzc(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzcky(iBinder, strArr, zzckr, z);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzcky[i];
    }
}
