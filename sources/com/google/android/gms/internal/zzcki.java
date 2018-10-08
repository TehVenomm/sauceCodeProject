package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;
import com.google.android.gms.nearby.connection.PayloadTransferUpdate;

public final class zzcki implements Creator<zzckh> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        PayloadTransferUpdate payloadTransferUpdate = null;
        int zzd = zzb.zzd(parcel);
        String str = null;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    str = zzb.zzq(parcel, readInt);
                    break;
                case 2:
                    payloadTransferUpdate = (PayloadTransferUpdate) zzb.zza(parcel, readInt, PayloadTransferUpdate.CREATOR);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new zzckh(str, payloadTransferUpdate);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzckh[i];
    }
}
