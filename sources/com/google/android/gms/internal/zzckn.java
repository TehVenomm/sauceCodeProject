package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzckn extends zza {
    public static final Creator<zzckn> CREATOR = new zzcko();
    private final int zzjcp;

    public zzckn(int i) {
        this.zzjcp = i;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzckn)) {
            return false;
        }
        return zzbf.equal(Integer.valueOf(this.zzjcp), Integer.valueOf(((zzckn) obj).zzjcp));
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzjcp)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzjcp);
        zzd.zzai(parcel, zze);
    }
}
