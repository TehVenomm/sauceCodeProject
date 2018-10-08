package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.internal.zzclm;

public final class zza implements Creator<Message> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        byte[] bArr = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        long j = 0;
        String str = null;
        String str2 = null;
        zzclm[] zzclmArr = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    bArr = zzb.zzt(parcel, readInt);
                    break;
                case 2:
                    str2 = zzb.zzq(parcel, readInt);
                    break;
                case 3:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 4:
                    zzclmArr = (zzclm[]) zzb.zzb(parcel, readInt, zzclm.CREATOR);
                    break;
                case 5:
                    j = zzb.zzi(parcel, readInt);
                    break;
                case 1000:
                    i = zzb.zzg(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new Message(i, bArr, str, str2, zzclmArr, j);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new Message[i];
    }
}
