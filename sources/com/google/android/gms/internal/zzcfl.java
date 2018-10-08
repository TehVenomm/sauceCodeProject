package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzcfl extends zza {
    public static final Creator<zzcfl> CREATOR = new zzcfm();
    public final String name;
    private int versionCode;
    private String zzfwi;
    public final String zzilz;
    public final long zziwu;
    private Long zziwv;
    private Float zziww;
    private Double zziwx;

    zzcfl(int i, String str, long j, Long l, Float f, String str2, String str3, Double d) {
        Double d2 = null;
        this.versionCode = i;
        this.name = str;
        this.zziwu = j;
        this.zziwv = l;
        this.zziww = null;
        if (i == 1) {
            if (f != null) {
                d2 = Double.valueOf(f.doubleValue());
            }
            this.zziwx = d2;
        } else {
            this.zziwx = d;
        }
        this.zzfwi = str2;
        this.zzilz = str3;
    }

    zzcfl(zzcfn zzcfn) {
        this(zzcfn.mName, zzcfn.zziwy, zzcfn.mValue, zzcfn.mOrigin);
    }

    zzcfl(String str, long j, Object obj, String str2) {
        zzbp.zzgf(str);
        this.versionCode = 2;
        this.name = str;
        this.zziwu = j;
        this.zzilz = str2;
        if (obj == null) {
            this.zziwv = null;
            this.zziww = null;
            this.zziwx = null;
            this.zzfwi = null;
        } else if (obj instanceof Long) {
            this.zziwv = (Long) obj;
            this.zziww = null;
            this.zziwx = null;
            this.zzfwi = null;
        } else if (obj instanceof String) {
            this.zziwv = null;
            this.zziww = null;
            this.zziwx = null;
            this.zzfwi = (String) obj;
        } else if (obj instanceof Double) {
            this.zziwv = null;
            this.zziww = null;
            this.zziwx = (Double) obj;
            this.zzfwi = null;
        } else {
            throw new IllegalArgumentException("User attribute given of un-supported type");
        }
    }

    public final Object getValue() {
        return this.zziwv != null ? this.zziwv : this.zziwx != null ? this.zziwx : this.zzfwi != null ? this.zzfwi : null;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.name, false);
        zzd.zza(parcel, 3, this.zziwu);
        zzd.zza(parcel, 4, this.zziwv, false);
        zzd.zza(parcel, 5, null, false);
        zzd.zza(parcel, 6, this.zzfwi, false);
        zzd.zza(parcel, 7, this.zzilz, false);
        zzd.zza(parcel, 8, this.zziwx, false);
        zzd.zzai(parcel, zze);
    }
}
