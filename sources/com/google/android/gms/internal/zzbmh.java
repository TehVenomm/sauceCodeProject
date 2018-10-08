package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbmh extends zza {
    public static final Creator<zzbmh> CREATOR = new zzbmi();
    private zzbmv zzgjn;

    zzbmh(zzbmv zzbmv) {
        this.zzgjn = zzbmv;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjn, i, false);
        zzd.zzai(parcel, zze);
    }
}
