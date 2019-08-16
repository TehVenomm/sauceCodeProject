package com.google.android.gms.internal.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.drive.FileUploadPreferences;
import com.google.android.gms.drive.TransferPreferences;

@Class(creator = "FileUploadPreferencesImplCreator")
@Reserved({1})
@Deprecated
public final class zzei extends AbstractSafeParcelable implements FileUploadPreferences {
    public static final Creator<zzei> CREATOR = new zzej();
    @Field(mo13990id = 3)
    private int zzbl;
    @Field(mo13990id = 2)
    private int zzgw;
    @Field(mo13990id = 4)
    private boolean zzgx;

    @Constructor
    public zzei(@Param(mo13993id = 2) int i, @Param(mo13993id = 3) int i2, @Param(mo13993id = 4) boolean z) {
        this.zzgw = i;
        this.zzbl = i2;
        this.zzgx = z;
    }

    public zzei(TransferPreferences transferPreferences) {
        this(transferPreferences.getNetworkPreference(), transferPreferences.getBatteryUsagePreference(), transferPreferences.isRoamingAllowed());
    }

    private static boolean zzh(int i) {
        switch (i) {
            case 1:
            case 2:
                return true;
            default:
                return false;
        }
    }

    private static boolean zzi(int i) {
        switch (i) {
            case 256:
            case 257:
                return true;
            default:
                return false;
        }
    }

    public final int getBatteryUsagePreference() {
        if (!zzi(this.zzbl)) {
            return 0;
        }
        return this.zzbl;
    }

    public final int getNetworkTypePreference() {
        if (!zzh(this.zzgw)) {
            return 0;
        }
        return this.zzgw;
    }

    public final boolean isRoamingAllowed() {
        return this.zzgx;
    }

    public final void setBatteryUsagePreference(int i) {
        if (!zzi(i)) {
            throw new IllegalArgumentException("Invalid battery usage preference value.");
        }
        this.zzbl = i;
    }

    public final void setNetworkTypePreference(int i) {
        if (!zzh(i)) {
            throw new IllegalArgumentException("Invalid data connection preference value.");
        }
        this.zzgw = i;
    }

    public final void setRoamingAllowed(boolean z) {
        this.zzgx = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 2, this.zzgw);
        SafeParcelWriter.writeInt(parcel, 3, this.zzbl);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzgx);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
