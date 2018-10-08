package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzcin extends zza {
    public static final Creator<zzcin> CREATOR = new zzcio();
    private final String zzjbb;

    public zzcin(String str) {
        this.zzjbb = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzcin)) {
            return false;
        }
        return zzbf.equal(this.zzjbb, ((zzcin) obj).zzjbb);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjbb});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjbb, false);
        zzd.zzai(parcel, zze);
    }
}
