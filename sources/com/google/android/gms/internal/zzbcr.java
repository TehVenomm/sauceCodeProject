package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbcr extends zza {
    public static final Creator<zzbcr> CREATOR = new zzbcs();
    private int zzdxt;
    private final zzbct zzfwe;

    zzbcr(int i, zzbct zzbct) {
        this.zzdxt = i;
        this.zzfwe = zzbct;
    }

    private zzbcr(zzbct zzbct) {
        this.zzdxt = 1;
        this.zzfwe = zzbct;
    }

    public static zzbcr zza(zzbcz<?, ?> zzbcz) {
        if (zzbcz instanceof zzbct) {
            return new zzbcr((zzbct) zzbcz);
        }
        throw new IllegalArgumentException("Unsupported safe parcelable field converter class.");
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzfwe, i, false);
        zzd.zzai(parcel, zze);
    }

    public final zzbcz<?, ?> zzakp() {
        if (this.zzfwe != null) {
            return this.zzfwe;
        }
        throw new IllegalStateException("There was no converter wrapped in this ConverterWrapper.");
    }
}
