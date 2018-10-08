package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzv;

public final class zzbmc extends zzv {
    public static final Creator<zzbmc> CREATOR = new zzbmd();
    final DataHolder zzgjm;

    public zzbmc(DataHolder dataHolder) {
        this.zzgjm = dataHolder;
    }

    protected final void zzaj(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjm, i, false);
        zzd.zzai(parcel, zze);
    }
}
