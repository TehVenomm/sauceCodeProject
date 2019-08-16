package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;

@Class(creator = "DeviceMetaDataCreator")
public class DeviceMetaData extends AbstractSafeParcelable {
    public static final Creator<DeviceMetaData> CREATOR = new zzv();
    @Field(getter = "isLockScreenSolved", mo13990id = 2)
    private boolean zzbs;
    @Field(getter = "getMinAgeOfLockScreen", mo13990id = 3)
    private long zzbt;
    @Field(getter = "isChallengeAllowed", mo13990id = 4)
    private final boolean zzbu;
    @VersionField(mo13996id = 1)
    private final int zzv;

    @Constructor
    DeviceMetaData(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) boolean z, @Param(mo13993id = 3) long j, @Param(mo13993id = 4) boolean z2) {
        this.zzv = i;
        this.zzbs = z;
        this.zzbt = j;
        this.zzbu = z2;
    }

    public long getMinAgeOfLockScreen() {
        return this.zzbt;
    }

    public boolean isChallengeAllowed() {
        return this.zzbu;
    }

    public boolean isLockScreenSolved() {
        return this.zzbs;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zzv);
        SafeParcelWriter.writeBoolean(parcel, 2, isLockScreenSolved());
        SafeParcelWriter.writeLong(parcel, 3, getMinAgeOfLockScreen());
        SafeParcelWriter.writeBoolean(parcel, 4, isChallengeAllowed());
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
