package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckd extends zza {
    public static final Creator<zzckd> CREATOR = new zzcke();
    private final String zzjcl;

    public zzckd(String str) {
        this.zzjcl = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckd)) {
            return false;
        }
        return zzbf.equal(this.zzjcl, ((zzckd) obj).zzjcl);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjcl});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjcl, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzban() {
        return this.zzjcl;
    }
}
