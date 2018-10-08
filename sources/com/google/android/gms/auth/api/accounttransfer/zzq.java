package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import java.util.List;

public final class zzq implements Creator<zzp> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        List list = null;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        List list2 = null;
        List list3 = null;
        List list4 = null;
        List list5 = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    list = zzb.zzac(parcel, readInt);
                    break;
                case 3:
                    list2 = zzb.zzac(parcel, readInt);
                    break;
                case 4:
                    list3 = zzb.zzac(parcel, readInt);
                    break;
                case 5:
                    list4 = zzb.zzac(parcel, readInt);
                    break;
                case 6:
                    list5 = zzb.zzac(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzp(i, list, list2, list3, list4, list5);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzp[i];
    }
}
