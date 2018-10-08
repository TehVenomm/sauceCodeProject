package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbdf extends zza {
    public static final Creator<zzbdf> CREATOR = new zzbdc();
    final String key;
    private int versionCode;
    final zzbcy<?, ?> zzfwy;

    zzbdf(int i, String str, zzbcy<?, ?> zzbcy) {
        this.versionCode = i;
        this.key = str;
        this.zzfwy = zzbcy;
    }

    zzbdf(String str, zzbcy<?, ?> zzbcy) {
        this.versionCode = 1;
        this.key = str;
        this.zzfwy = zzbcy;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.key, false);
        zzd.zza(parcel, 3, this.zzfwy, i, false);
        zzd.zzai(parcel, zze);
    }
}
