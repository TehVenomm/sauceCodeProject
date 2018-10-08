package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzcak extends zza {
    public static final Creator<zzcak> CREATOR = new zzcal();
    public final String packageName;
    public final String zzhtl;
    public final String zziln;
    public final String zzilo;
    public final long zzilp;
    public final long zzilq;
    public final String zzilr;
    public final boolean zzils;
    public final boolean zzilt;
    public final long zzilu;
    public final String zzilv;
    public final long zzilw;
    public final long zzilx;
    public final int zzily;

    zzcak(String str, String str2, String str3, long j, String str4, long j2, long j3, String str5, boolean z, boolean z2, String str6, long j4, long j5, int i) {
        zzbp.zzgf(str);
        this.packageName = str;
        if (TextUtils.isEmpty(str2)) {
            str2 = null;
        }
        this.zziln = str2;
        this.zzhtl = str3;
        this.zzilu = j;
        this.zzilo = str4;
        this.zzilp = j2;
        this.zzilq = j3;
        this.zzilr = str5;
        this.zzils = z;
        this.zzilt = z2;
        this.zzilv = str6;
        this.zzilw = j4;
        this.zzilx = j5;
        this.zzily = i;
    }

    zzcak(String str, String str2, String str3, String str4, long j, long j2, String str5, boolean z, boolean z2, long j3, String str6, long j4, long j5, int i) {
        this.packageName = str;
        this.zziln = str2;
        this.zzhtl = str3;
        this.zzilu = j3;
        this.zzilo = str4;
        this.zzilp = j;
        this.zzilq = j2;
        this.zzilr = str5;
        this.zzils = z;
        this.zzilt = z2;
        this.zzilv = str6;
        this.zzilw = j4;
        this.zzilx = j5;
        this.zzily = i;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.packageName, false);
        zzd.zza(parcel, 3, this.zziln, false);
        zzd.zza(parcel, 4, this.zzhtl, false);
        zzd.zza(parcel, 5, this.zzilo, false);
        zzd.zza(parcel, 6, this.zzilp);
        zzd.zza(parcel, 7, this.zzilq);
        zzd.zza(parcel, 8, this.zzilr, false);
        zzd.zza(parcel, 9, this.zzils);
        zzd.zza(parcel, 10, this.zzilt);
        zzd.zza(parcel, 11, this.zzilu);
        zzd.zza(parcel, 12, this.zzilv, false);
        zzd.zza(parcel, 13, this.zzilw);
        zzd.zza(parcel, 14, this.zzilx);
        zzd.zzc(parcel, 15, this.zzily);
        zzd.zzai(parcel, zze);
    }
}
