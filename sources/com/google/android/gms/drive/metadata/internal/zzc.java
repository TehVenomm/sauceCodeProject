package com.google.android.gms.drive.metadata.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.metadata.CustomPropertyKey;
import java.util.Arrays;

public final class zzc extends zza {
    public static final Creator<zzc> CREATOR = new zzd();
    final String mValue;
    final CustomPropertyKey zzgkm;

    public zzc(CustomPropertyKey customPropertyKey, String str) {
        zzbp.zzb((Object) customPropertyKey, (Object) "key");
        this.zzgkm = customPropertyKey;
        this.mValue = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        zzc zzc = (zzc) obj;
        return zzbf.equal(this.zzgkm, zzc.zzgkm) && zzbf.equal(this.mValue, zzc.mValue);
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgkm, this.mValue});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgkm, i, false);
        zzd.zza(parcel, 3, this.mValue, false);
        zzd.zzai(parcel, zze);
    }
}
