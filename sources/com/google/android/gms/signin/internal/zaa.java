package com.google.android.gms.signin.internal;

import android.content.Intent;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;

@Class(creator = "AuthAccountResultCreator")
public final class zaa extends AbstractSafeParcelable implements Result {
    public static final Creator<zaa> CREATOR = new zab();
    @VersionField(mo13996id = 1)
    private final int zale;
    @Field(getter = "getConnectionResultCode", mo13990id = 2)
    private int zarw;
    @Field(getter = "getRawAuthResolutionIntent", mo13990id = 3)
    private Intent zarx;

    public zaa() {
        this(0, null);
    }

    @Constructor
    zaa(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) int i2, @Param(mo13993id = 3) Intent intent) {
        this.zale = i;
        this.zarw = i2;
        this.zarx = intent;
    }

    private zaa(int i, Intent intent) {
        this(2, 0, null);
    }

    public final Status getStatus() {
        return this.zarw == 0 ? Status.RESULT_SUCCESS : Status.RESULT_CANCELED;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.zale);
        SafeParcelWriter.writeInt(parcel, 2, this.zarw);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zarx, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
