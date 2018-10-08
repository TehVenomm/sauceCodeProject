package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbcu extends zza {
    public static final Creator<zzbcu> CREATOR = new zzbcw();
    private int versionCode;
    final String zzfwi;
    final int zzfwj;

    zzbcu(int i, String str, int i2) {
        this.versionCode = i;
        this.zzfwi = str;
        this.zzfwj = i2;
    }

    zzbcu(String str, int i) {
        this.versionCode = 1;
        this.zzfwi = str;
        this.zzfwj = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.zzfwi, false);
        zzd.zzc(parcel, 3, this.zzfwj);
        zzd.zzai(parcel, zze);
    }
}
