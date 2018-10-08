package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.List;

public final class zzbmk extends zza {
    public static final Creator<zzbmk> CREATOR = new zzbml();
    private final List<String> zzgjo;

    zzbmk(List<String> list) {
        this.zzgjo = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzb(parcel, 2, this.zzgjo, false);
        zzd.zzai(parcel, zze);
    }
}
