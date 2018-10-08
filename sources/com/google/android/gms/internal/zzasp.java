package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.auth.api.credentials.Credential;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzasp extends zza {
    public static final Creator<zzasp> CREATOR = new zzasq();
    private final Credential zzebh;

    public zzasp(Credential credential) {
        this.zzebh = credential;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzebh, i, false);
        zzd.zzai(parcel, zze);
    }
}
