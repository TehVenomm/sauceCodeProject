package com.google.android.gms.common.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.api.Scope;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbu extends zza {
    public static final Creator<zzbu> CREATOR = new zzbv();
    private int zzdxt;
    private final int zzfvv;
    private final int zzfvw;
    @Deprecated
    private final Scope[] zzfvx;

    zzbu(int i, int i2, int i3, Scope[] scopeArr) {
        this.zzdxt = i;
        this.zzfvv = i2;
        this.zzfvw = i3;
        this.zzfvx = scopeArr;
    }

    public zzbu(int i, int i2, Scope[] scopeArr) {
        this(1, i, i2, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzfvv);
        zzd.zzc(parcel, 3, this.zzfvw);
        zzd.zza(parcel, 4, this.zzfvx, i, false);
        zzd.zzai(parcel, zze);
    }
}
