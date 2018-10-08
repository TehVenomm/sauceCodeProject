package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelUuid;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzcll implements Creator<zzclk> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int i = 0;
        ParcelUuid parcelUuid = null;
        int zzd = zzb.zzd(parcel);
        ParcelUuid parcelUuid2 = null;
        ParcelUuid parcelUuid3 = null;
        byte[] bArr = null;
        byte[] bArr2 = null;
        byte[] bArr3 = null;
        byte[] bArr4 = null;
        int i2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 4:
                    parcelUuid = (ParcelUuid) zzb.zza(parcel, readInt, ParcelUuid.CREATOR);
                    break;
                case 5:
                    parcelUuid2 = (ParcelUuid) zzb.zza(parcel, readInt, ParcelUuid.CREATOR);
                    break;
                case 6:
                    parcelUuid3 = (ParcelUuid) zzb.zza(parcel, readInt, ParcelUuid.CREATOR);
                    break;
                case 7:
                    bArr = zzb.zzt(parcel, readInt);
                    break;
                case 8:
                    bArr2 = zzb.zzt(parcel, readInt);
                    break;
                case 9:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 10:
                    bArr3 = zzb.zzt(parcel, readInt);
                    break;
                case 11:
                    bArr4 = zzb.zzt(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzclk(i, parcelUuid, parcelUuid2, parcelUuid3, bArr, bArr2, i2, bArr3, bArr4);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzclk[i];
    }
}
