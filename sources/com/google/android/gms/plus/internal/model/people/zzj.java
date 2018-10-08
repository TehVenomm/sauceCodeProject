package com.google.android.gms.plus.internal.model.people;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.plus.internal.model.people.PersonEntity.UrlsEntity;
import java.util.HashSet;
import java.util.Set;

public class zzj implements Creator<UrlsEntity> {
    static void zza(UrlsEntity urlsEntity, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        Set set = urlsEntity.zzazD;
        if (set.contains(Integer.valueOf(1))) {
            zzb.zzc(parcel, 1, urlsEntity.zzzH);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzb.zzc(parcel, 3, urlsEntity.zzvY());
        }
        if (set.contains(Integer.valueOf(4))) {
            zzb.zza(parcel, 4, urlsEntity.mValue, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            zzb.zza(parcel, 5, urlsEntity.zzawK, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            zzb.zzc(parcel, 6, urlsEntity.zzMG);
        }
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzfd(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhw(i);
    }

    public UrlsEntity zzfd(Parcel parcel) {
        String str = null;
        int i = 0;
        int zzL = zza.zzL(parcel);
        Set hashSet = new HashSet();
        String str2 = null;
        int i2 = 0;
        int i3 = 0;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            switch (zza.zzaV(zzK)) {
                case 1:
                    i = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(1));
                    break;
                case 3:
                    i3 = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(3));
                    break;
                case 4:
                    str2 = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(4));
                    break;
                case 5:
                    str = zza.zzo(parcel, zzK);
                    hashSet.add(Integer.valueOf(5));
                    break;
                case 6:
                    i2 = zza.zzg(parcel, zzK);
                    hashSet.add(Integer.valueOf(6));
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new UrlsEntity(hashSet, i, str, i2, str2, i3);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public UrlsEntity[] zzhw(int i) {
        return new UrlsEntity[i];
    }
}
