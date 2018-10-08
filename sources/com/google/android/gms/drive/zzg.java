package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzg extends zza {
    public static final Creator<zzg> CREATOR = new zzh();
    private long zzgdh;
    private long zzgdi;

    public zzg(long j, long j2) {
        this.zzgdh = j;
        this.zzgdi = j2;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgdh);
        zzd.zza(parcel, 3, this.zzgdi);
        zzd.zzai(parcel, zze);
    }
}
