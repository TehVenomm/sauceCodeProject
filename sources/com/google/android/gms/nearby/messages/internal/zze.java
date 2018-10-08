package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.nearby.messages.Distance;
import java.util.Arrays;
import java.util.Locale;

public final class zze extends zza implements Distance {
    public static final Creator<zze> CREATOR = new zzf();
    private int accuracy;
    private int zzdxt;
    private double zzjfu;

    public zze(int i, double d) {
        this(1, 1, Double.NaN);
    }

    zze(int i, int i2, double d) {
        this.zzdxt = i;
        this.accuracy = i2;
        this.zzjfu = d;
    }

    public final int compareTo(@NonNull Distance distance) {
        return (Double.isNaN(getMeters()) && Double.isNaN(distance.getMeters())) ? 0 : Double.compare(getMeters(), distance.getMeters());
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zze)) {
                return false;
            }
            Distance distance = (zze) obj;
            if (getAccuracy() != distance.getAccuracy()) {
                return false;
            }
            if (compareTo(distance) != 0) {
                return false;
            }
        }
        return true;
    }

    public final int getAccuracy() {
        return this.accuracy;
    }

    public final double getMeters() {
        return this.zzjfu;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(getAccuracy()), Double.valueOf(getMeters())});
    }

    public final String toString() {
        String str;
        Locale locale = Locale.US;
        double d = this.zzjfu;
        switch (this.accuracy) {
            case 1:
                str = "LOW";
                break;
            default:
                str = "UNKNOWN";
                break;
        }
        return String.format(locale, "(%.1fm, %s)", new Object[]{Double.valueOf(d), str});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.accuracy);
        zzd.zza(parcel, 3, this.zzjfu);
        zzd.zzai(parcel, zze);
    }
}
