package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzc;

public final class zzbhn extends zza {
    public static final Creator<zzbhn> CREATOR = new zzbho();
    private zzc zzgfy;
    private int zzgga;
    private Boolean zzggc;

    public zzbhn(int i, boolean z) {
        this(null, Boolean.valueOf(false), i);
    }

    zzbhn(zzc zzc, Boolean bool, int i) {
        this.zzgfy = zzc;
        this.zzggc = bool;
        this.zzgga = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfy, i, false);
        zzd.zza(parcel, 3, this.zzggc, false);
        zzd.zzc(parcel, 4, this.zzgga);
        zzd.zzai(parcel, zze);
    }
}
