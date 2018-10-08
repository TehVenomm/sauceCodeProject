package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.plus.internal.model.people.PersonEntity.NameEntity;
import java.util.HashSet;
import java.util.Set;

public class zzg implements Creator<NameEntity> {
    static void zza(NameEntity nameEntity, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        Set set = nameEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            zzb.zzc(parcel, 1, nameEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzb.zza(parcel, 2, nameEntity.zzaAb, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzb.zza(parcel, 3, nameEntity.zzaBb, true);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzb.zza(parcel, 4, nameEntity.zzaAe, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            zzb.zza(parcel, 5, nameEntity.zzaBc, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            zzb.zza(parcel, 6, nameEntity.zzaBd, true);
        }
        if (set.contains(Integer.valueOf(7))) {
            zzb.zza(parcel, 7, nameEntity.zzaBe, true);
        }
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzfa(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzht(i);
    }

    public NameEntity zzfa(Parcel parcel) {
        String str = null;
        int zzL = zza.zzL(parcel);
        Set hashSet = new HashSet();
        int i = 0;
        String str2 = null;
        String str3 = null;
        String str4 = null;
        String str5 = null;
        String str6 = null;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            switch (zza.zzaV(zzK)) {
                case 1:
                    i = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    str = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(2));
                    break;
                case 3:
                    str2 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(3));
                    break;
                case 4:
                    str3 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(4));
                    break;
                case 5:
                    str4 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(5));
                    break;
                case 6:
                    str5 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(6));
                    break;
                case 7:
                    str6 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(7));
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new NameEntity(hashSet, i, str, str2, str3, str4, str5, str6);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public NameEntity[] zzht(int i) {
        return new NameEntity[i];
    }
}
