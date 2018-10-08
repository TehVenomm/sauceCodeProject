package com.google.android.gms.plus.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;

public class zzf implements Creator<PlusCommonExtras> {
    static void zza(PlusCommonExtras plusCommonExtras, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        zzb.zza(parcel, 1, plusCommonExtras.zzvA(), false);
        zzb.zzc(parcel, 1000, plusCommonExtras.getVersionCode());
        zzb.zza(parcel, 2, plusCommonExtras.zzvB(), false);
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeQ(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhj(i);
    }

    public PlusCommonExtras zzeQ(Parcel parcel) {
        String str = null;
        int zzL = zza.zzL(parcel);
        int i = 0;
        String str2 = null;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            switch (zza.zzaV(zzK)) {
                case 1:
                    str2 = zza.zzo(parcel, zzK);
                    break;
                case 2:
                    str = zza.zzo(parcel, zzK);
                    break;
                case 1000:
                    i = zza.zzg(parcel, zzK);
                    break;
                default:
                    zza.zzb(parcel, zzK);
                    break;
            }
        }
        if (parcel.dataPosition() == zzL) {
            return new PlusCommonExtras(i, str2, str);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public PlusCommonExtras[] zzhj(int i) {
        return new PlusCommonExtras[i];
    }
}
