package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import com.google.android.gms.nearby.messages.Distance;
import java.util.Locale;

@Class(creator = "DistanceImplCreator")
public final class zze extends AbstractSafeParcelable implements Distance {
    public static final Creator<zze> CREATOR = new zzf();
    @Field(mo13990id = 2)
    private final int accuracy;
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Field(mo13990id = 3)
    private final double zzhg;

    public zze(int i, double d) {
        this(1, 1, Double.NaN);
    }

    @Constructor
    zze(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) int i2, @Param(mo13993id = 3) double d) {
        this.versionCode = i;
        this.accuracy = i2;
        this.zzhg = d;
    }

    public final int compareTo(@NonNull Distance distance) {
        if (!Double.isNaN(getMeters()) || !Double.isNaN(distance.getMeters())) {
            return Double.compare(getMeters(), distance.getMeters());
        }
        return 0;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zze)) {
                return false;
            }
            zze zze = (zze) obj;
            if (!(getAccuracy() == zze.getAccuracy() && compareTo((Distance) zze) == 0)) {
                return false;
            }
        }
        return true;
    }

    public final int getAccuracy() {
        return this.accuracy;
    }

    public final double getMeters() {
        return this.zzhg;
    }

    public final int hashCode() {
        return Objects.hashCode(Integer.valueOf(getAccuracy()), Double.valueOf(getMeters()));
    }

    public final String toString() {
        String str;
        Locale locale = Locale.US;
        double d = this.zzhg;
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
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeInt(parcel, 2, this.accuracy);
        SafeParcelWriter.writeDouble(parcel, 3, this.zzhg);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
