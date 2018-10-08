package com.google.android.gms.internal;

import android.content.Intent;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzcpp extends zza implements Result {
    public static final Creator<zzcpp> CREATOR = new zzcpq();
    private int zzdxt;
    private int zzjni;
    private Intent zzjnj;

    public zzcpp() {
        this(0, null);
    }

    zzcpp(int i, int i2, Intent intent) {
        this.zzdxt = i;
        this.zzjni = i2;
        this.zzjnj = intent;
    }

    private zzcpp(int i, Intent intent) {
        this(2, 0, null);
    }

    public final Status getStatus() {
        return this.zzjni == 0 ? Status.zzfhp : Status.zzfht;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzjni);
        zzd.zza(parcel, 3, this.zzjnj, i, false);
        zzd.zzai(parcel, zze);
    }
}
