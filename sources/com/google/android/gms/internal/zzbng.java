package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.List;

public final class zzbng extends zza {
    public static final Creator<zzbng> CREATOR = new zzbnh();
    private final List<String> zzgjy;

    public zzbng(List<String> list) {
        this.zzgjy = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzb(parcel, 2, this.zzgjy, false);
        zzd.zzai(parcel, zze);
    }
}
