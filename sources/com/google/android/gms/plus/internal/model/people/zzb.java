package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.plus.internal.model.people.PersonEntity.AgeRangeEntity;
import java.util.HashSet;
import java.util.Set;

public class zzb implements Creator<AgeRangeEntity> {
    static void zza(AgeRangeEntity ageRangeEntity, Parcel parcel, int i) {
        int zzM = com.google.android.gms.common.internal.safeparcel.zzb.zzM(parcel);
        Set set = ageRangeEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, 1, ageRangeEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, 2, ageRangeEntity.zzaAU);
        }
        if (set.contains(Integer.valueOf(3))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, 3, ageRangeEntity.zzaAV);
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeV(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzho(i);
    }

    public AgeRangeEntity zzeV(Parcel parcel) {
        int i = 0;
        int zzL = zza.zzL(parcel);
        Set hashSet = new HashSet();
        int i2 = 0;
        int i3 = 0;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            switch (zza.zzaV(zzK)) {
                case 1:
                    i2 = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    i3 = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(2));
                    break;
                case 3:
                    i = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(3));
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new AgeRangeEntity(hashSet, i2, i3, i);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public AgeRangeEntity[] zzho(int i) {
        return new AgeRangeEntity[i];
    }
}
