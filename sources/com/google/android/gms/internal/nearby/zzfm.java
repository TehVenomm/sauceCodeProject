package com.google.android.gms.internal.nearby;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;

@Class(creator = "RejectConnectionRequestParamsCreator")
@Reserved({1000})
public final class zzfm extends AbstractSafeParcelable {
    public static final Creator<zzfm> CREATOR = new zzfp();
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getResultListenerAsBinder", mo13990id = 1, type = "android.os.IBinder")
    public zzdz zzar;
    /* access modifiers changed from: private */
    @Field(getter = "getRemoteEndpointId", mo13990id = 2)
    public String zzat;

    private zzfm() {
    }

    @Constructor
    zzfm(@Nullable @Param(mo13993id = 1) IBinder iBinder, @Param(mo13993id = 2) String str) {
        zzdz zzeb;
        if (iBinder == null) {
            zzeb = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzeb = queryLocalInterface instanceof zzdz ? (zzdz) queryLocalInterface : new zzeb(iBinder);
        }
        this(zzeb, str);
    }

    private zzfm(@Nullable zzdz zzdz, String str) {
        this.zzar = zzdz;
        this.zzat = str;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzfm)) {
            return false;
        }
        zzfm zzfm = (zzfm) obj;
        return Objects.equal(this.zzar, zzfm.zzar) && Objects.equal(this.zzat, zzfm.zzat);
    }

    public final int hashCode() {
        return Objects.hashCode(this.zzar, this.zzat);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeIBinder(parcel, 1, this.zzar == null ? null : this.zzar.asBinder(), false);
        SafeParcelWriter.writeString(parcel, 2, this.zzat, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
