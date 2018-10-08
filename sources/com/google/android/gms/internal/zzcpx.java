package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbq;

public final class zzcpx extends zza {
    public static final Creator<zzcpx> CREATOR = new zzcpy();
    private int zzdxt;
    private zzbq zzjnm;

    zzcpx(int i, zzbq zzbq) {
        this.zzdxt = i;
        this.zzjnm = zzbq;
    }

    public zzcpx(zzbq zzbq) {
        this(1, zzbq);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjnm, i, false);
        zzd.zzai(parcel, zze);
    }
}
