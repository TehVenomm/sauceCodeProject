package com.google.android.gms.nearby.connection;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.Arrays;

public final class Strategy extends zza {
    public static final Creator<Strategy> CREATOR = new zzf();
    public static final Strategy P2P_CLUSTER = new Strategy(1, 3);
    public static final Strategy P2P_STAR = new Strategy(1, 2);
    private final int zzjax;
    private final int zzjay;

    Strategy(int i, int i2) {
        this.zzjax = i;
        this.zzjay = i2;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof Strategy)) {
                return false;
            }
            Strategy strategy = (Strategy) obj;
            if (this.zzjax != strategy.zzjax) {
                return false;
            }
            if (this.zzjay != strategy.zzjay) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzjax), Integer.valueOf(this.zzjay)});
    }

    public final String toString() {
        String str = P2P_CLUSTER.equals(this) ? "P2P_CLUSTER" : P2P_STAR.equals(this) ? "P2P_STAR" : "UNKNOWN";
        return String.format("Strategy(%s){connectionType=%d, topology=%d}", new Object[]{str, Integer.valueOf(this.zzjax), Integer.valueOf(this.zzjay)});
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 3, this.zzjax);
        zzd.zzc(parcel, 4, this.zzjay);
        zzd.zzai(parcel, zze);
    }
}
