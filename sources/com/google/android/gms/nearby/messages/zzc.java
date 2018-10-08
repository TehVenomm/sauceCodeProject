package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.internal.zzclk;
import com.google.android.gms.internal.zzclo;
import com.google.android.gms.nearby.messages.internal.zzad;
import java.util.List;

public final class zzc implements Creator<MessageFilter> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        List list = null;
        boolean z = false;
        int zzd = zzb.zzd(parcel);
        List list2 = null;
        List list3 = null;
        int i = 0;
        int i2 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    list = zzb.zzc(parcel, readInt, zzad.CREATOR);
                    break;
                case 2:
                    list2 = zzb.zzc(parcel, readInt, zzclo.CREATOR);
                    break;
                case 3:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 4:
                    list3 = zzb.zzc(parcel, readInt, zzclk.CREATOR);
                    break;
                case 5:
                    i2 = zzb.zzg(parcel, readInt);
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
        return new MessageFilter(i, list, list2, z, list3, i2);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new MessageFilter[i];
    }
}
