package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzcjv extends zza {
    public static final Creator<zzcjv> CREATOR = new zzcjw();
    private final int statusCode;
    private final String zzjbb;
    @Nullable
    private final byte[] zzjbc;

    public zzcjv(String str, int i, @Nullable byte[] bArr) {
        this.zzjbb = str;
        this.statusCode = i;
        this.zzjbc = bArr;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcjv)) {
            return false;
        }
        zzcjv zzcjv = (zzcjv) obj;
        return zzbf.equal(this.zzjbb, zzcjv.zzjbb) && zzbf.equal(Integer.valueOf(this.statusCode), Integer.valueOf(zzcjv.statusCode)) && zzbf.equal(this.zzjbc, zzcjv.zzjbc);
    }

    public final int getStatusCode() {
        return this.statusCode;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb, Integer.valueOf(this.statusCode), this.zzjbc});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zzc(parcel, 2, this.statusCode);
        zzd.zza(parcel, 3, this.zzjbc, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzbaj() {
        return this.zzjbb;
    }

    @Nullable
    public final byte[] zzbam() {
        return this.zzjbc;
    }
}
