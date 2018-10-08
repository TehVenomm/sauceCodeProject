package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzcjt extends zza {
    public static final Creator<zzcjt> CREATOR = new zzcju();
    private final String zzjbb;
    @Nullable
    private final byte[] zzjbc;
    private final String zzjck;

    public zzcjt(String str, String str2, @Nullable byte[] bArr) {
        this.zzjbb = str;
        this.zzjck = str2;
        this.zzjbc = bArr;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcjt)) {
            return false;
        }
        zzcjt zzcjt = (zzcjt) obj;
        return zzbf.equal(this.zzjbb, zzcjt.zzjbb) && zzbf.equal(this.zzjck, zzcjt.zzjck) && zzbf.equal(this.zzjbc, zzcjt.zzjbc);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb, this.zzjck, this.zzjbc});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zza(parcel, 2, this.zzjck, false);
        zzd.zza(parcel, 3, this.zzjbc, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzbaj() {
        return this.zzjbb;
    }

    public final String zzbak() {
        return this.zzjck;
    }

    @Nullable
    public final byte[] zzbam() {
        return this.zzjbc;
    }
}
