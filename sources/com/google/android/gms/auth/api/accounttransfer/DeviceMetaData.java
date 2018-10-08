package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public class DeviceMetaData extends zza {
    public static final Creator<DeviceMetaData> CREATOR = new zzw();
    private int zzdxt;
    private boolean zzdzv;
    private long zzdzw;
    private final boolean zzdzx;

    DeviceMetaData(int i, boolean z, long j, boolean z2) {
        this.zzdxt = i;
        this.zzdzv = z;
        this.zzdzw = j;
        this.zzdzx = z2;
    }

    public long getMinAgeOfLockScreen() {
        return this.zzdzw;
    }

    public boolean isChallengeAllowed() {
        return this.zzdzx;
    }

    public boolean isLockScreenSolved() {
        return this.zzdzv;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, isLockScreenSolved());
        zzd.zza(parcel, 3, getMinAgeOfLockScreen());
        zzd.zza(parcel, 4, isChallengeAllowed());
        zzd.zzai(parcel, zze);
    }
}
