package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzj extends zza {
    public static final Creator<zzj> CREATOR = new zzk();
    private int versionCode;
    @Deprecated
    private ClientAppContext zzjfw;
    private int zzjfx;

    public zzj(int i) {
        this(1, null, i);
    }

    zzj(int i, ClientAppContext clientAppContext, int i2) {
        this.versionCode = i;
        this.zzjfw = clientAppContext;
        this.zzjfx = i2;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.zzjfw, i, false);
        zzd.zzc(parcel, 3, this.zzjfx);
        zzd.zzai(parcel, zze);
    }
}
