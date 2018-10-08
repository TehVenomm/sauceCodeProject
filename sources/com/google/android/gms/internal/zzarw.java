package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzarw extends zza {
    public static final Creator<zzarw> CREATOR = new zzarx();
    private String accountType;
    private int zzdxt;
    private int zzdzz;

    zzarw(int i, String str, int i2) {
        this.zzdxt = 1;
        this.accountType = (String) zzbp.zzu(str);
        this.zzdzz = i2;
    }

    public zzarw(String str, int i) {
        this(1, str, i);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.accountType, false);
        zzd.zzc(parcel, 3, this.zzdzz);
        zzd.zzai(parcel, zze);
    }
}
