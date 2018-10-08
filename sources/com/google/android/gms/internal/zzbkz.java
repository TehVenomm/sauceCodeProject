package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzs;
import java.util.List;

public final class zzbkz extends zza {
    public static final Creator<zzbkz> CREATOR = new zzbla();
    private int zzbvs;
    private List<zzs> zzgis;

    public zzbkz(List<zzs> list, int i) {
        this.zzgis = list;
        this.zzbvs = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgis, false);
        zzd.zzc(parcel, 3, this.zzbvs);
        zzd.zzai(parcel, zze);
    }
}
