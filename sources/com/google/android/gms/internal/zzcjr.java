package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzcjr extends zza {
    public static final Creator<zzcjr> CREATOR = new zzcjs();
    private final String zzjal;
    private final boolean zzjam;
    private final String zzjbb;
    @Nullable
    private final byte[] zzjbc;
    private final String zzjck;

    public zzcjr(String str, String str2, String str3, boolean z, @Nullable byte[] bArr) {
        this.zzjbb = str;
        this.zzjck = str2;
        this.zzjal = str3;
        this.zzjam = z;
        this.zzjbc = bArr;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcjr)) {
            return false;
        }
        zzcjr zzcjr = (zzcjr) obj;
        return zzbf.equal(this.zzjbb, zzcjr.zzjbb) && zzbf.equal(this.zzjck, zzcjr.zzjck) && zzbf.equal(this.zzjal, zzcjr.zzjal) && zzbf.equal(Boolean.valueOf(this.zzjam), Boolean.valueOf(zzcjr.zzjam)) && Arrays.equals(this.zzjbc, zzcjr.zzjbc);
    }

    public final String getAuthenticationToken() {
        return this.zzjal;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb, this.zzjck, this.zzjal, Boolean.valueOf(this.zzjam), this.zzjbc});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zza(parcel, 2, this.zzjck, false);
        zzd.zza(parcel, 3, this.zzjal, false);
        zzd.zza(parcel, 4, this.zzjam);
        zzd.zza(parcel, 5, this.zzjbc, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzbaj() {
        return this.zzjbb;
    }

    public final String zzbak() {
        return this.zzjck;
    }

    public final boolean zzbal() {
        return this.zzjam;
    }
}
