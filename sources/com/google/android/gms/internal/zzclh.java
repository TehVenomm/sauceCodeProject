package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzclh implements Creator<zzclg> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int zzd = zzb.zzd(parcel);
        while (parcel.dataPosition() < zzd) {
            zzb.zzb(parcel, parcel.readInt());
        }
        zzb.zzaf(parcel, zzd);
        return new zzclg();
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzclg[i];
    }
}
