package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzcan extends zza {
    public static final Creator<zzcan> CREATOR = new zzcao();
    public String packageName;
    private int versionCode;
    public String zzilz;
    public zzcfl zzima;
    public long zzimb;
    public boolean zzimc;
    public String zzimd;
    public zzcbc zzime;
    public long zzimf;
    public zzcbc zzimg;
    public long zzimh;
    public zzcbc zzimi;

    zzcan(int i, String str, String str2, zzcfl zzcfl, long j, boolean z, String str3, zzcbc zzcbc, long j2, zzcbc zzcbc2, long j3, zzcbc zzcbc3) {
        this.versionCode = i;
        this.packageName = str;
        this.zzilz = str2;
        this.zzima = zzcfl;
        this.zzimb = j;
        this.zzimc = z;
        this.zzimd = str3;
        this.zzime = zzcbc;
        this.zzimf = j2;
        this.zzimg = zzcbc2;
        this.zzimh = j3;
        this.zzimi = zzcbc3;
    }

    zzcan(zzcan zzcan) {
        this.versionCode = 1;
        zzbp.zzu(zzcan);
        this.packageName = zzcan.packageName;
        this.zzilz = zzcan.zzilz;
        this.zzima = zzcan.zzima;
        this.zzimb = zzcan.zzimb;
        this.zzimc = zzcan.zzimc;
        this.zzimd = zzcan.zzimd;
        this.zzime = zzcan.zzime;
        this.zzimf = zzcan.zzimf;
        this.zzimg = zzcan.zzimg;
        this.zzimh = zzcan.zzimh;
        this.zzimi = zzcan.zzimi;
    }

    zzcan(String str, String str2, zzcfl zzcfl, long j, boolean z, String str3, zzcbc zzcbc, long j2, zzcbc zzcbc2, long j3, zzcbc zzcbc3) {
        this.versionCode = 1;
        this.packageName = str;
        this.zzilz = str2;
        this.zzima = zzcfl;
        this.zzimb = j;
        this.zzimc = z;
        this.zzimd = str3;
        this.zzime = zzcbc;
        this.zzimf = j2;
        this.zzimg = zzcbc2;
        this.zzimh = j3;
        this.zzimi = zzcbc3;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.packageName, false);
        zzd.zza(parcel, 3, this.zzilz, false);
        zzd.zza(parcel, 4, this.zzima, i, false);
        zzd.zza(parcel, 5, this.zzimb);
        zzd.zza(parcel, 6, this.zzimc);
        zzd.zza(parcel, 7, this.zzimd, false);
        zzd.zza(parcel, 8, this.zzime, i, false);
        zzd.zza(parcel, 9, this.zzimf);
        zzd.zza(parcel, 10, this.zzimg, i, false);
        zzd.zza(parcel, 11, this.zzimh);
        zzd.zza(parcel, 12, this.zzimi, i, false);
        zzd.zzai(parcel, zze);
    }
}
