package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzad extends zza {
    public static final Creator<zzad> CREATOR = new zzae();
    private String type;
    private int zzdxt;
    private String zzjdp;

    zzad(int i, String str, String str2) {
        this.zzdxt = i;
        this.zzjdp = str;
        this.type = str2;
    }

    public zzad(String str, String str2) {
        this(1, str, str2);
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzad) || hashCode() != obj.hashCode()) {
            return false;
        }
        zzad zzad = (zzad) obj;
        return zzbf.equal(this.zzjdp, zzad.zzjdp) && zzbf.equal(this.type, zzad.type);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjdp, this.type});
    }

    public final String toString() {
        String str = this.zzjdp;
        String str2 = this.type;
        return new StringBuilder((String.valueOf(str).length() + 17) + String.valueOf(str2).length()).append("namespace=").append(str).append(", type=").append(str2).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzjdp, false);
        zzd.zza(parcel, 2, this.type, false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
    }
}
