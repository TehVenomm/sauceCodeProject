package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckb extends zza {
    public static final Creator<zzckb> CREATOR = new zzckc();
    private final String zzjak;
    private final String zzjan;
    private final String zzjcl;

    public zzckb(String str, String str2, String str3) {
        this.zzjcl = str;
        this.zzjan = str2;
        this.zzjak = str3;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckb)) {
            return false;
        }
        zzckb zzckb = (zzckb) obj;
        return zzbf.equal(this.zzjcl, zzckb.zzjcl) && zzbf.equal(this.zzjan, zzckb.zzjan) && zzbf.equal(this.zzjak, zzckb.zzjak);
    }

    public final String getEndpointName() {
        return this.zzjak;
    }

    public final String getServiceId() {
        return this.zzjan;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjcl, this.zzjan, this.zzjak});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjcl, false);
        zzd.zza(parcel, 2, this.zzjan, false);
        zzd.zza(parcel, 3, this.zzjak, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzban() {
        return this.zzjcl;
    }
}
