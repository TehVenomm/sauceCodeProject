package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzblq extends zza {
    public static final Creator<zzblq> CREATOR = new zzblr();
    final zzbkv zzgiz;

    public zzblq(zzbkv zzbkv) {
        this.zzgiz = zzbkv;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgiz, i, false);
        zzd.zzai(parcel, zze);
    }
}
