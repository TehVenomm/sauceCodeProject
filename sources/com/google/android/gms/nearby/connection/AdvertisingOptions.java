package com.google.android.gms.nearby.connection;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class AdvertisingOptions extends zza {
    public static final Creator<AdvertisingOptions> CREATOR = new zza();
    private final Strategy zzjah;
    @Nullable
    private final boolean zzjai;

    public AdvertisingOptions(Strategy strategy) {
        this(strategy, true);
    }

    public AdvertisingOptions(Strategy strategy, @Nullable boolean z) {
        this.zzjah = strategy;
        this.zzjai = z;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof AdvertisingOptions)) {
            return false;
        }
        AdvertisingOptions advertisingOptions = (AdvertisingOptions) obj;
        return zzbf.equal(this.zzjah, advertisingOptions.zzjah) && zzbf.equal(Boolean.valueOf(this.zzjai), Boolean.valueOf(advertisingOptions.zzjai));
    }

    public final Strategy getStrategy() {
        return this.zzjah;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzjah, Boolean.valueOf(this.zzjai)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, getStrategy(), i, false);
        zzd.zza(parcel, 2, this.zzjai);
        zzd.zzai(parcel, zze);
    }
}
