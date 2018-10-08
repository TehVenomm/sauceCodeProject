package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbmv extends zza {
    public static final Creator<zzbmv> CREATOR = new zzbmw();
    private int zzgio;
    private int zzgip;
    private boolean zzgju;

    zzbmv(int i, int i2, boolean z) {
        this.zzgio = i;
        this.zzgip = i2;
        this.zzgju = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgio);
        zzd.zzc(parcel, 3, this.zzgip);
        zzd.zza(parcel, 4, this.zzgju);
        zzd.zzai(parcel, zze);
    }
}
