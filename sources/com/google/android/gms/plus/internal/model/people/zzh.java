package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.plus.internal.model.people.PersonEntity.OrganizationsEntity;
import java.util.HashSet;
import java.util.Set;

public class zzh implements Creator<OrganizationsEntity> {
    static void zza(OrganizationsEntity organizationsEntity, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        Set set = organizationsEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            zzb.zzc(parcel, 1, organizationsEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzb.zza(parcel, 2, organizationsEntity.zzaBf, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzb.zza(parcel, 3, organizationsEntity.zzadH, true);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzb.zza(parcel, 4, organizationsEntity.zzaAa, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            zzb.zza(parcel, 5, organizationsEntity.zzaBg, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            zzb.zza(parcel, 6, organizationsEntity.mName, true);
        }
        if (set.contains(Integer.valueOf(7))) {
            zzb.zza(parcel, 7, organizationsEntity.zzaBh);
        }
        if (set.contains(Integer.valueOf(8))) {
            zzb.zza(parcel, 8, organizationsEntity.zzaAq, true);
        }
        if (set.contains(Integer.valueOf(9))) {
            zzb.zza(parcel, 9, organizationsEntity.zzWn, true);
        }
        if (set.contains(Integer.valueOf(10))) {
            zzb.zzc(parcel, 10, organizationsEntity.zzMG);
        }
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzfb(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhu(i);
    }

    public OrganizationsEntity zzfb(Parcel parcel) {
        boolean z = false;
        String str = null;
        int zzL = zza.zzL(parcel);
        Set hashSet = new HashSet();
        String str2 = null;
        String str3 = null;
        String str4 = null;
        String str5 = null;
        String str6 = null;
        String str7 = null;
        int i = 0;
        int i2 = 0;
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
                    z = zza.zzc(parcel, zzK);
                    hashSet.add(Integer.valueOf(7));
                    break;
                case 8:
                    str6 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(8));
                    break;
                case 9:
                    str7 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(9));
                    break;
                case 10:
                    i2 = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(10));
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new OrganizationsEntity(hashSet, i, str, str2, str3, str4, str5, z, str6, str7, i2);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public OrganizationsEntity[] zzhu(int i) {
        return new OrganizationsEntity[i];
    }
}
