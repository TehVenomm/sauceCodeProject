package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzcbc extends zza {
    public static final Creator<zzcbc> CREATOR = new zzcbd();
    public final String name;
    public final String zzilz;
    public final zzcaz zzinj;
    public final long zzink;

    zzcbc(zzcbc zzcbc, long j) {
        zzbp.zzu(zzcbc);
        this.name = zzcbc.name;
        this.zzinj = zzcbc.zzinj;
        this.zzilz = zzcbc.zzilz;
        this.zzink = j;
    }

    public zzcbc(String str, zzcaz zzcaz, String str2, long j) {
        this.name = str;
        this.zzinj = zzcaz;
        this.zzilz = str2;
        this.zzink = j;
    }

    public final String toString() {
        String str = this.zzilz;
        String str2 = this.name;
        String valueOf = String.valueOf(this.zzinj);
        return new StringBuilder(((String.valueOf(str).length() + 21) + String.valueOf(str2).length()) + String.valueOf(valueOf).length()).append("origin=").append(str).append(",name=").append(str2).append(",params=").append(valueOf).toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.name, false);
        zzd.zza(parcel, 3, this.zzinj, i, false);
        zzd.zza(parcel, 4, this.zzilz, false);
        zzd.zza(parcel, 5, this.zzink);
        zzd.zzai(parcel, zze);
    }
}
