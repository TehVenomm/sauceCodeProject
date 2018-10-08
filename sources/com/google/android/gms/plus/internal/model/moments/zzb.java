package com.google.android.gms.plus.internal.model.moments;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import java.util.HashSet;
import java.util.Set;

public class zzb implements Creator<MomentEntity> {
    static void zza(MomentEntity momentEntity, Parcel parcel, int i) {
        int zzM = com.google.android.gms.common.internal.safeparcel.zzb.zzM(parcel);
        Set set = momentEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, 1, momentEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(2))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, 2, momentEntity.zzGM, true);
        }
        if (set.contains(Integer.valueOf(4))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, 4, momentEntity.zzaAy, i, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, 5, momentEntity.zzaAq, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, 6, momentEntity.zzaAz, i, true);
        }
        if (set.contains(Integer.valueOf(7))) {
            com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, 7, momentEntity.zzAV, true);
        }
        com.google.android.gms.common.internal.safeparcel.zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeT(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhm(i);
    }

    public MomentEntity zzeT(Parcel parcel) {
        ItemScopeEntity itemScopeEntity = null;
        int zzL = zza.zzL(parcel);
        Set hashSet = new HashSet();
        int i = 0;
        String str = null;
        ItemScopeEntity itemScopeEntity2 = null;
        String str2 = null;
        String str3 = null;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            ItemScopeEntity itemScopeEntity3;
            switch (zza.zzaV(zzK)) {
                case 1:
                    i = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 2:
                    str = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(2));
                    break;
                case 4:
                    itemScopeEntity3 = (ItemScopeEntity) zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(4));
                    itemScopeEntity = itemScopeEntity3;
                    break;
                case 5:
                    str2 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(5));
                    break;
                case 6:
                    itemScopeEntity3 = (ItemScopeEntity) zza.zza(parcel, zzK, ItemScopeEntity.CREATOR);
                    hashSet.add(Integer.valueOf(6));
                    itemScopeEntity2 = itemScopeEntity3;
                    break;
                case 7:
                    str3 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(7));
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new MomentEntity(hashSet, i, str, itemScopeEntity, str2, itemScopeEntity2, str3);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public MomentEntity[] zzhm(int i) {
        return new MomentEntity[i];
    }
}
