package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzc;

public final class zzblo extends zza {
    public static final Creator<zzblo> CREATOR = new zzblp();
    final zzc zzghj;
    final boolean zzgiy;

    public zzblo(zzc zzc, boolean z) {
        this.zzghj = zzc;
        this.zzgiy = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzghj, i, false);
        zzd.zza(parcel, 3, this.zzgiy);
        zzd.zzai(parcel, zze);
    }
}
