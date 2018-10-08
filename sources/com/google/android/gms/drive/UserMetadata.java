package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public class UserMetadata extends zza implements ReflectedParcelable {
    public static final Creator<UserMetadata> CREATOR = new zzu();
    private String zzeby;
    private String zzgen;
    private String zzgeo;
    private boolean zzgep;
    private String zzgeq;

    public UserMetadata(String str, String str2, String str3, boolean z, String str4) {
        this.zzgen = str;
        this.zzeby = str2;
        this.zzgeo = str3;
        this.zzgep = z;
        this.zzgeq = str4;
    }

    public String toString() {
        return String.format("Permission ID: '%s', Display Name: '%s', Picture URL: '%s', Authenticated User: %b, Email: '%s'", new Object[]{this.zzgen, this.zzeby, this.zzgeo, Boolean.valueOf(this.zzgep), this.zzgeq});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgen, false);
        zzd.zza(parcel, 3, this.zzeby, false);
        zzd.zza(parcel, 4, this.zzgeo, false);
        zzd.zza(parcel, 5, this.zzgep);
        zzd.zza(parcel, 6, this.zzgeq, false);
        zzd.zzai(parcel, zze);
    }
}
