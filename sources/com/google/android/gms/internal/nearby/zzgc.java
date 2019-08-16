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
import com.google.android.gms.nearby.connection.DiscoveryOptions;

@Class(creator = "StartDiscoveryParamsCreator")
@Reserved({1000})
public final class zzgc extends AbstractSafeParcelable {
    public static final Creator<zzgc> CREATOR = new zzgf();
    /* access modifiers changed from: private */
    @Field(getter = "getDurationMillis", mo13990id = 4)
    public long durationMillis;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getResultListenerAsBinder", mo13990id = 1, type = "android.os.IBinder")
    public zzdz zzar;
    @Nullable
    @Field(getter = "getCallbackAsBinder", mo13990id = 2, type = "android.os.IBinder")
    private zzdp zzel;
    /* access modifiers changed from: private */
    @Field(getter = "getOptions", mo13990id = 5)
    public DiscoveryOptions zzem;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getDiscoveryListenerAsBinder", mo13990id = 6, type = "android.os.IBinder")
    public zzdr zzen;
    /* access modifiers changed from: private */
    @Field(getter = "getServiceId", mo13990id = 3)
    public String zzu;

    private zzgc() {
    }

    @Constructor
    zzgc(@Nullable @Param(mo13993id = 1) IBinder iBinder, @Nullable @Param(mo13993id = 2) IBinder iBinder2, @Param(mo13993id = 3) String str, @Param(mo13993id = 4) long j, @Param(mo13993id = 5) DiscoveryOptions discoveryOptions, @Nullable @Param(mo13993id = 6) IBinder iBinder3) {
        zzdz zzeb;
        zzdp zzdq;
        zzdr zzdr = null;
        if (iBinder == null) {
            zzeb = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IResultListener");
            zzeb = queryLocalInterface instanceof zzdz ? (zzdz) queryLocalInterface : new zzeb(iBinder);
        }
        if (iBinder2 == null) {
            zzdq = null;
        } else {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IDiscoveryCallback");
            zzdq = queryLocalInterface2 instanceof zzdp ? (zzdp) queryLocalInterface2 : new zzdq(iBinder2);
        }
        if (iBinder3 != null) {
            IInterface queryLocalInterface3 = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IDiscoveryListener");
            zzdr = queryLocalInterface3 instanceof zzdr ? (zzdr) queryLocalInterface3 : new zzdt(iBinder3);
        }
        this(zzeb, zzdq, str, j, discoveryOptions, zzdr);
    }

    private zzgc(@Nullable zzdz zzdz, @Nullable zzdp zzdp, String str, long j, DiscoveryOptions discoveryOptions, @Nullable zzdr zzdr) {
        this.zzar = zzdz;
        this.zzel = zzdp;
        this.zzu = str;
        this.durationMillis = j;
        this.zzem = discoveryOptions;
        this.zzen = zzdr;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzgc)) {
            return false;
        }
        zzgc zzgc = (zzgc) obj;
        return Objects.equal(this.zzar, zzgc.zzar) && Objects.equal(this.zzel, zzgc.zzel) && Objects.equal(this.zzu, zzgc.zzu) && Objects.equal(Long.valueOf(this.durationMillis), Long.valueOf(zzgc.durationMillis)) && Objects.equal(this.zzem, zzgc.zzem) && Objects.equal(this.zzen, zzgc.zzen);
    }

    public final int hashCode() {
        return Objects.hashCode(this.zzar, this.zzel, this.zzu, Long.valueOf(this.durationMillis), this.zzem, this.zzen);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeIBinder(parcel, 1, this.zzar == null ? null : this.zzar.asBinder(), false);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zzel == null ? null : this.zzel.asBinder(), false);
        SafeParcelWriter.writeString(parcel, 3, this.zzu, false);
        SafeParcelWriter.writeLong(parcel, 4, this.durationMillis);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzem, i, false);
        if (this.zzen != null) {
            iBinder = this.zzen.asBinder();
        }
        SafeParcelWriter.writeIBinder(parcel, 6, iBinder, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
