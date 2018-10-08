package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.common.internal.safeparcel.zzc;
import java.util.HashSet;
import java.util.Set;

public final class zzt implements Creator<zzs> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        zzu zzu = null;
        int zzd = zzb.zzd(parcel);
        Set hashSet = new HashSet();
        int i = 0;
        String str = null;
        String str2 = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    zzu zzu2 = (zzu) zzb.zza(parcel, readInt, zzu.CREATOR);
                    hashSet.add(Integer.valueOf(2));
                    zzu = zzu2;
                    break;
                case 3:
                    str = zzb.zzq(parcel, readInt);
                    hashSet.add(Integer.valueOf(3));
                    break;
                case 4:
                    str2 = zzb.zzq(parcel, readInt);
                    hashSet.add(Integer.valueOf(4));
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        if (parcel.dataPosition() == zzd) {
            return new zzs(hashSet, i, zzu, str, str2);
        }
        throw new zzc("Overread allowed size end=" + zzd, parcel);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzs[i];
    }
}
