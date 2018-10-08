package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.List;

public final class zzbhj extends zza {
    public static final Creator<zzbhj> CREATOR = new zzbhk();
    private List<String> zzgfb;

    public zzbhj(List<String> list) {
        this.zzgfb = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzb(parcel, 2, this.zzgfb, false);
        zzd.zzai(parcel, zze);
    }
}
