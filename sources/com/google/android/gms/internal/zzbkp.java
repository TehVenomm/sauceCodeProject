package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbkp extends zza {
    public static final Creator<zzbkp> CREATOR = new zzbkq();
    final IBinder zzgij;

    zzbkp(IBinder iBinder) {
        this.zzgij = iBinder;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgij, false);
        zzd.zzai(parcel, zze);
    }
}
