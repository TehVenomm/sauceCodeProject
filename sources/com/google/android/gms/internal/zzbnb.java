package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbnb extends zza {
    public static final Creator<zzbnb> CREATOR = new zzbnc();
    private zzbkv zzgiz;

    public zzbnb(zzbkv zzbkv) {
        this.zzgiz = zzbkv;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgiz, i, false);
        zzd.zzai(parcel, zze);
    }
}
