package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.common.internal.safeparcel.zzc;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;

public final class zzn implements Creator<zzm> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        ArrayList arrayList = null;
        int i = 0;
        int zzd = zzb.zzd(parcel);
        Set hashSet = new HashSet();
        zzp zzp = null;
        int i2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    arrayList = zzb.zzc(parcel, readInt, zzs.CREATOR);
                    hashSet.add(Integer.valueOf(2));
                    break;
                case 3:
                    i2 = zzb.zzg(parcel, readInt);
                    hashSet.add(Integer.valueOf(3));
                    break;
                case 4:
                    zzp zzp2 = (zzp) zzb.zza(parcel, readInt, zzp.CREATOR);
                    hashSet.add(Integer.valueOf(4));
                    zzp = zzp2;
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        if (parcel.dataPosition() == zzd) {
            return new zzm(hashSet, i, arrayList, i2, zzp);
        }
        throw new zzc("Overread allowed size end=" + zzd, parcel);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzm[i];
    }
}
