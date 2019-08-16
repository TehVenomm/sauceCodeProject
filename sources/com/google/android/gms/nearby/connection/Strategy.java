package com.google.android.gms.nearby.connection;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import java.util.Locale;

@Class(creator = "StrategyCreator")
@Reserved({1000})
public final class Strategy extends AbstractSafeParcelable {
    public static final Creator<Strategy> CREATOR = new zzj();
    public static final Strategy P2P_CLUSTER = new Strategy(1, 3);
    public static final Strategy P2P_POINT_TO_POINT = new Strategy(1, 1);
    public static final Strategy P2P_STAR = new Strategy(1, 2);
    @Field(getter = "getConnectionType", mo13990id = 3)
    private final int zzaj;
    @Field(getter = "getTopology", mo13990id = 4)
    private final int zzak;

    @Constructor
    Strategy(@Param(mo13993id = 3) int i, @Param(mo13993id = 4) int i2) {
        this.zzaj = i;
        this.zzak = i2;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Strategy)) {
                return false;
            }
            Strategy strategy = (Strategy) obj;
            if (!(this.zzaj == strategy.zzaj && this.zzak == strategy.zzak)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return Objects.hashCode(Integer.valueOf(this.zzaj), Integer.valueOf(this.zzak));
    }

    public final String toString() {
        Locale locale = Locale.US;
        String str = P2P_CLUSTER.equals(this) ? "P2P_CLUSTER" : P2P_STAR.equals(this) ? "P2P_STAR" : P2P_POINT_TO_POINT.equals(this) ? "P2P_POINT_TO_POINT" : "UNKNOWN";
        return String.format(locale, "Strategy(%s){connectionType=%d, topology=%d}", new Object[]{str, Integer.valueOf(this.zzaj), Integer.valueOf(this.zzak)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 3, this.zzaj);
        SafeParcelWriter.writeInt(parcel, 4, this.zzak);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
