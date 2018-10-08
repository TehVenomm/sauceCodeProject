package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbly extends zza {
    public static final Creator<zzbly> CREATOR = new zzblz();
    private ParcelFileDescriptor zzgjk;

    public zzbly(ParcelFileDescriptor parcelFileDescriptor) {
        this.zzgjk = parcelFileDescriptor;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjk, i | 1, false);
        zzd.zzai(parcel, zze);
    }
}
