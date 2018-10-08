package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbme extends zza {
    public static final Creator<zzbme> CREATOR = new zzbmj();
    private boolean zzaqo;

    public zzbme(boolean z) {
        this.zzaqo = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzaqo);
        zzd.zzai(parcel, zze);
    }
}
