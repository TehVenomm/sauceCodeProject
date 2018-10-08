package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzg;
import java.util.Collections;
import java.util.List;

public final class zzbls extends zza {
    public static final Creator<zzbls> CREATOR = new zzblt();
    private static final List<zzg> zzgja = Collections.emptyList();
    private int zzbyx;
    final long zzgjb;
    final long zzgjc;
    private List<zzg> zzgjd;

    public zzbls(long j, long j2, int i, List<zzg> list) {
        this.zzgjb = j;
        this.zzgjc = j2;
        this.zzbyx = i;
        this.zzgjd = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjb);
        zzd.zza(parcel, 3, this.zzgjc);
        zzd.zzc(parcel, 4, this.zzbyx);
        zzd.zzc(parcel, 5, this.zzgjd, false);
        zzd.zzai(parcel, zze);
    }
}
