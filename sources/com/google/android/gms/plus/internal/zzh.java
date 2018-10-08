package com.google.android.gms.plus.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzb;

public class zzh implements Creator<PlusSession> {
    static void zza(PlusSession plusSession, Parcel parcel, int i) {
        int zzM = zzb.zzM(parcel);
        zzb.zza(parcel, 1, plusSession.getAccountName(), false);
        zzb.zzc(parcel, 1000, plusSession.getVersionCode());
        zzb.zza(parcel, 2, plusSession.zzvC(), false);
        zzb.zza(parcel, 3, plusSession.zzvD(), false);
        zzb.zza(parcel, 4, plusSession.zzvE(), false);
        zzb.zza(parcel, 5, plusSession.zzvF(), false);
        zzb.zza(parcel, 6, plusSession.zzvG(), false);
        zzb.zza(parcel, 7, plusSession.zzvH(), false);
        zzb.zza(parcel, 8, plusSession.zzvI(), false);
        zzb.zza(parcel, 9, plusSession.zzvJ(), i, false);
        zzb.zzH(parcel, zzM);
    }

    public /* synthetic */ Object createFromParcel(Parcel parcel) {
        return zzeR(parcel);
    }

    public /* synthetic */ Object[] newArray(int i) {
        return zzhk(i);
    }

    public PlusSession zzeR(Parcel parcel) {
        String str = null;
        int zzL = zza.zzL(parcel);
        int i = 0;
        String[] strArr = null;
        String[] strArr2 = null;
        String[] strArr3 = null;
        String str2 = null;
        String str3 = null;
        String str4 = null;
        String str5 = null;
        PlusCommonExtras plusCommonExtras = null;
        while (parcel.dataPosition() < zzL) {
            int zzK = zza.zzK(parcel);
            switch (zza.zzaV(zzK)) {
                case 1:
                    str = zza.zzo(parcel, zzK);
                    break;
                case 2:
                    strArr = zza.zzA(parcel, zzK);
                    break;
                case 3:
                    strArr2 = zza.zzA(parcel, zzK);
                    break;
                case 4:
                    strArr3 = zza.zzA(parcel, zzK);
                    break;
                case 5:
                    str2 = zza.zzo(parcel, zzK);
                    break;
                case 6:
                    str3 = zza.zzo(parcel, zzK);
                    break;
                case 7:
                    str4 = zza.zzo(parcel, zzK);
                    break;
                case 8:
                    str5 = zza.zzo(parcel, zzK);
                    break;
                case 9:
                    plusCommonExtras = (PlusCommonExtras) zza.zza(parcel, zzK, PlusCommonExtras.CREATOR);
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
            return new PlusSession(i, str, strArr, strArr2, strArr3, str2, str3, str4, str5, plusCommonExtras);
        }
        throw new zza.zza("Overread allowed size end=" + zzL, parcel);
    }

    public PlusSession[] zzhk(int i) {
        return new PlusSession[i];
    }
}
