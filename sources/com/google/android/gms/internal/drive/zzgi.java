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
import com.google.android.gms.drive.TransferPreferences;

@Class(creator = "ParcelableTransferPreferencesCreator")
@Reserved({1})
public final class zzgi extends AbstractSafeParcelable implements TransferPreferences {
    public static final Creator<zzgi> CREATOR = new zzgj();
    @Field(mo13990id = 4)
    private final boolean zzbk;
    @Field(mo13990id = 3)
    private final int zzbl;
    @Field(mo13990id = 2)
    private final int zzgw;

    @Constructor
    zzgi(@Param(mo13993id = 2) int i, @Param(mo13993id = 3) int i2, @Param(mo13993id = 4) boolean z) {
        this.zzgw = i;
        this.zzbl = i2;
        this.zzbk = z;
    }

    public final int getBatteryUsagePreference() {
        return this.zzbl;
    }

    public final int getNetworkPreference() {
        return this.zzgw;
    }

    public final boolean isRoamingAllowed() {
        return this.zzbk;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 2, this.zzgw);
        SafeParcelWriter.writeInt(parcel, 3, this.zzbl);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzbk);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
