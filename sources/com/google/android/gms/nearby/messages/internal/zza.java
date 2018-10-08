package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.nearby.messages.BleSignal;
import java.util.Arrays;

public final class zza extends com.google.android.gms.common.internal.safeparcel.zza implements BleSignal {
    public static final Creator<zza> CREATOR = new zzb();
    private int zzdxt;
    private int zzjfn;
    private int zzjfo;

    zza(int i, int i2, int i3) {
        this.zzdxt = i;
        this.zzjfn = i2;
        if (-169 >= i3 || i3 >= 87) {
            i3 = Integer.MIN_VALUE;
        }
        this.zzjfo = i3;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof BleSignal)) {
                return false;
            }
            BleSignal bleSignal = (BleSignal) obj;
            if (this.zzjfn != bleSignal.getRssi()) {
                return false;
            }
            if (this.zzjfo != bleSignal.getTxPower()) {
                return false;
            }
        }
        return true;
    }

    public final int getRssi() {
        return this.zzjfn;
    }

    public final int getTxPower() {
        return this.zzjfo;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzjfn), Integer.valueOf(this.zzjfo)});
    }

    public final String toString() {
        int i = this.zzjfn;
        return "BleSignal{rssi=" + i + ", txPower=" + this.zzjfo + "}";
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzjfn);
        zzd.zzc(parcel, 3, this.zzjfo);
        zzd.zzai(parcel, zze);
    }
}
