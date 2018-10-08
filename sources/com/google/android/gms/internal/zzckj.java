package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckj extends zza {
    public static final Creator<zzckj> CREATOR = new zzckk();
    private final int statusCode;
    private final String zzjbt;

    public zzckj(int i, String str) {
        this.statusCode = i;
        this.zzjbt = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckj)) {
            return false;
        }
        zzckj zzckj = (zzckj) obj;
        return zzbf.equal(Integer.valueOf(this.statusCode), Integer.valueOf(zzckj.statusCode)) && zzbf.equal(this.zzjbt, zzckj.zzjbt);
    }

    public final String getLocalEndpointName() {
        return this.zzjbt;
    }

    public final int getStatusCode() {
        return this.statusCode;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.statusCode), this.zzjbt});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.statusCode);
        zzd.zza(parcel, 2, this.zzjbt, false);
        zzd.zzai(parcel, zze);
    }
}
