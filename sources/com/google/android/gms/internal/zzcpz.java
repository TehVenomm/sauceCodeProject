package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbs;

public final class zzcpz extends zza {
    public static final Creator<zzcpz> CREATOR = new zzcqa();
    private int zzdxt;
    private final ConnectionResult zzfit;
    private final zzbs zzjnn;

    public zzcpz(int i) {
        this(new ConnectionResult(8, null), null);
    }

    zzcpz(int i, ConnectionResult connectionResult, zzbs zzbs) {
        this.zzdxt = i;
        this.zzfit = connectionResult;
        this.zzjnn = zzbs;
    }

    private zzcpz(ConnectionResult connectionResult, zzbs zzbs) {
        this(1, connectionResult, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzfit, i, false);
        zzd.zza(parcel, 3, this.zzjnn, i, false);
        zzd.zzai(parcel, zze);
    }

    public final ConnectionResult zzagc() {
        return this.zzfit;
    }

    public final zzbs zzbca() {
        return this.zzjnn;
    }
}
