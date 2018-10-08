package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.FileUploadPreferences;

public final class zzbkv extends zza implements FileUploadPreferences {
    public static final Creator<zzbkv> CREATOR = new zzbkw();
    private int zzgio;
    private int zzgip;
    private boolean zzgiq;

    public zzbkv(int i, int i2, boolean z) {
        this.zzgio = i;
        this.zzgip = i2;
        this.zzgiq = z;
    }

    private static boolean zzcs(int i) {
        switch (i) {
            case 1:
            case 2:
                return true;
            default:
                return false;
        }
    }

    private static boolean zzct(int i) {
        switch (i) {
            case 256:
            case 257:
                return true;
            default:
                return false;
        }
    }

    public final int getBatteryUsagePreference() {
        return !zzct(this.zzgip) ? 0 : this.zzgip;
    }

    public final int getNetworkTypePreference() {
        return !zzcs(this.zzgio) ? 0 : this.zzgio;
    }

    public final boolean isRoamingAllowed() {
        return this.zzgiq;
    }

    public final void setBatteryUsagePreference(int i) {
        if (zzct(i)) {
            this.zzgip = i;
            return;
        }
        throw new IllegalArgumentException("Invalid battery usage preference value.");
    }

    public final void setNetworkTypePreference(int i) {
        if (zzcs(i)) {
            this.zzgio = i;
            return;
        }
        throw new IllegalArgumentException("Invalid data connection preference value.");
    }

    public final void setRoamingAllowed(boolean z) {
        this.zzgiq = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 2, this.zzgio);
        zzd.zzc(parcel, 3, this.zzgip);
        zzd.zza(parcel, 4, this.zzgiq);
        zzd.zzai(parcel, zze);
    }
}
