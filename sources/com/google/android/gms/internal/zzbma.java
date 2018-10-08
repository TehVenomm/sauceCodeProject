package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.zzv;

public final class zzbma extends zzv {
    public static final Creator<zzbma> CREATOR = new zzbmb();
    final boolean zzggs;
    final DataHolder zzgjl;

    public zzbma(DataHolder dataHolder, boolean z) {
        this.zzgjl = dataHolder;
        this.zzggs = z;
    }

    protected final void zzaj(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgjl, i, false);
        zzd.zza(parcel, 3, this.zzggs);
        zzd.zzai(parcel, zze);
    }
}
