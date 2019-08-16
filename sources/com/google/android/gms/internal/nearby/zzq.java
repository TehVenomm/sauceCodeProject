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

@Class(creator = "CancelPayloadParamsCreator")
public final class zzq extends AbstractSafeParcelable {
    public static final Creator<zzq> CREATOR = new zzt();
    /* access modifiers changed from: private */
    @Field(getter = "getPayloadId", mo13990id = 2)
    public long zzaf;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getResultListenerAsBinder", mo13990id = 1, type = "android.os.IBinder")
    public zzdz zzar;

    private zzq() {
    }

    @Constructor
    zzq(@Nullable @Param(mo13993id = 1) IBinder iBinder, @Param(mo13993id = 2) long j) {
        zzdz zzeb;
        if (iBinder == null) {
            zzeb = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzeb = queryLocalInterface instanceof zzdz ? (zzdz) queryLocalInterface : new zzeb(iBinder);
        }
        this(zzeb, j);
    }

    private zzq(@Nullable zzdz zzdz, long j) {
        this.zzar = zzdz;
        this.zzaf = j;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzq)) {
            return false;
        }
        zzq zzq = (zzq) obj;
        return Objects.equal(this.zzar, zzq.zzar) && Objects.equal(Long.valueOf(this.zzaf), Long.valueOf(zzq.zzaf));
    }

    public final int hashCode() {
        return Objects.hashCode(this.zzar, Long.valueOf(this.zzaf));
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeIBinder(parcel, 1, this.zzar == null ? null : this.zzar.asBinder(), false);
        SafeParcelWriter.writeLong(parcel, 2, this.zzaf);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
