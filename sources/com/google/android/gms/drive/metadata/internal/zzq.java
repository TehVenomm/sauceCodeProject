package com.google.android.gms.drive.metadata.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzq extends zza {
    public static final Creator<zzq> CREATOR = new zzr();
    final String zzgdj;
    final long zzgdk;
    final int zzgdl;

    public zzq(String str, long j, int i) {
        this.zzgdj = str;
        this.zzgdk = j;
        this.zzgdl = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgdj, false);
        zzd.zza(parcel, 3, this.zzgdk);
        zzd.zzc(parcel, 4, this.zzgdl);
        zzd.zzai(parcel, zze);
    }
}
