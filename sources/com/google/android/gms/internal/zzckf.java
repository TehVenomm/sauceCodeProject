package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckf extends zza {
    public static final Creator<zzckf> CREATOR = new zzckg();
    private final String zzjbb;
    private final zzckr zzjcm;
    private final boolean zzjcn;

    public zzckf(String str, zzckr zzckr, boolean z) {
        this.zzjbb = str;
        this.zzjcm = zzckr;
        this.zzjcn = z;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckf)) {
            return false;
        }
        zzckf zzckf = (zzckf) obj;
        return zzbf.equal(this.zzjbb, zzckf.zzjbb) && zzbf.equal(this.zzjcm, zzckf.zzjcm) && zzbf.equal(Boolean.valueOf(this.zzjcn), Boolean.valueOf(zzckf.zzjcn));
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb, this.zzjcm, Boolean.valueOf(this.zzjcn)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zza(parcel, 2, this.zzjcm, i, false);
        zzd.zza(parcel, 3, this.zzjcn);
        zzd.zzai(parcel, zze);
    }

    public final String zzbaj() {
        return this.zzjbb;
    }

    public final zzckr zzbao() {
        return this.zzjcm;
    }

    public final boolean zzbap() {
        return this.zzjcn;
    }
}
