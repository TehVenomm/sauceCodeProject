package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbmm extends zza {
    public static final Creator<zzbmm> CREATOR = new zzbmn();
    private String zzaxy;
    private ParcelFileDescriptor zzgjp;
    private IBinder zzgjq;

    zzbmm(ParcelFileDescriptor parcelFileDescriptor, IBinder iBinder, String str) {
        this.zzgjp = parcelFileDescriptor;
        this.zzgjq = iBinder;
        this.zzaxy = str;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjp, i | 1, false);
        zzd.zza(parcel, 3, this.zzgjq, false);
        zzd.zza(parcel, 4, this.zzaxy, false);
        zzd.zzai(parcel, zze);
    }
}
