package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.plus.internal.model.people.PersonEntity.CoverEntity.CoverInfoEntity;
import java.util.HashSet;
import java.util.Set;

public class zzd implements Creator<CoverInfoEntity> {
    static void zza(CoverInfoEntity coverInfoEntity, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        Set set = coverInfoEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            zzb.zzc(parcel, 1, coverInfoEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzb.zzc(parcel, 2, coverInfoEntity.zzaAZ);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzb.zzc(parcel, 3, coverInfoEntity.zzaBa);
        }
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeX(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhq(i);
    }

    public CoverInfoEntity zzeX(Parcel parcel) {
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
            return new CoverInfoEntity(hashSet, i2, i3, i);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public CoverInfoEntity[] zzhq(int i) {
        return new CoverInfoEntity[i];
    }
}
