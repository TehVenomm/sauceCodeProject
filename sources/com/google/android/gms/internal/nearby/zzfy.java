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
import com.google.android.gms.nearby.connection.AdvertisingOptions;

@Class(creator = "StartAdvertisingParamsCreator")
@Reserved({1000})
public final class zzfy extends AbstractSafeParcelable {
    public static final Creator<zzfy> CREATOR = new zzgb();
    /* access modifiers changed from: private */
    @Field(getter = "getDurationMillis", mo13990id = 5)
    public long durationMillis;
    /* access modifiers changed from: private */
    @Field(getter = "getName", mo13990id = 3)
    public String name;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getConnectionLifecycleListenerAsBinder", mo13990id = 7, type = "android.os.IBinder")
    public zzdj zzec;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getResultListenerAsBinder", mo13990id = 1, type = "android.os.IBinder")
    public zzec zzeh;
    /* access modifiers changed from: private */
    @Nullable
    @Field(getter = "getCallbackAsBinder", mo13990id = 2, type = "android.os.IBinder")
    public zzdd zzei;
    /* access modifiers changed from: private */
    @Field(getter = "getOptions", mo13990id = 6)
    public AdvertisingOptions zzej;
    /* access modifiers changed from: private */
    @Field(getter = "getServiceId", mo13990id = 4)
    public String zzu;

    private zzfy() {
    }

    @Constructor
    zzfy(@Nullable @Param(mo13993id = 1) IBinder iBinder, @Nullable @Param(mo13993id = 2) IBinder iBinder2, @Param(mo13993id = 3) String str, @Param(mo13993id = 4) String str2, @Param(mo13993id = 5) long j, @Param(mo13993id = 6) AdvertisingOptions advertisingOptions, @Nullable @Param(mo13993id = 7) IBinder iBinder3) {
        zzec zzee;
        zzdd zzdf;
        zzdj zzdl;
        if (iBinder == null) {
            zzee = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IStartAdvertisingResultListener");
            zzee = queryLocalInterface instanceof zzec ? (zzec) queryLocalInterface : new zzee(iBinder);
        }
        if (iBinder2 == null) {
            zzdf = null;
        } else {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IAdvertisingCallback");
            zzdf = queryLocalInterface2 instanceof zzdd ? (zzdd) queryLocalInterface2 : new zzdf(iBinder2);
        }
        if (iBinder3 == null) {
            zzdl = null;
        } else {
            IInterface queryLocalInterface3 = iBinder3.queryLocalInterface("com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener");
            zzdl = queryLocalInterface3 instanceof zzdj ? (zzdj) queryLocalInterface3 : new zzdl(iBinder3);
        }
        this(zzee, zzdf, str, str2, j, advertisingOptions, zzdl);
    }

    private zzfy(@Nullable zzec zzec2, @Nullable zzdd zzdd, String str, String str2, long j, AdvertisingOptions advertisingOptions, @Nullable zzdj zzdj) {
        this.zzeh = zzec2;
        this.zzei = zzdd;
        this.name = str;
        this.zzu = str2;
        this.durationMillis = j;
        this.zzej = advertisingOptions;
        this.zzec = zzdj;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzfy)) {
            return false;
        }
        zzfy zzfy = (zzfy) obj;
        return Objects.equal(this.zzeh, zzfy.zzeh) && Objects.equal(this.zzei, zzfy.zzei) && Objects.equal(this.name, zzfy.name) && Objects.equal(this.zzu, zzfy.zzu) && Objects.equal(Long.valueOf(this.durationMillis), Long.valueOf(zzfy.durationMillis)) && Objects.equal(this.zzej, zzfy.zzej) && Objects.equal(this.zzec, zzfy.zzec);
    }

    public final int hashCode() {
        return Objects.hashCode(this.zzeh, this.zzei, this.name, this.zzu, Long.valueOf(this.durationMillis), this.zzej, this.zzec);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeIBinder(parcel, 1, this.zzeh == null ? null : this.zzeh.asBinder(), false);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zzei == null ? null : this.zzei.asBinder(), false);
        SafeParcelWriter.writeString(parcel, 3, this.name, false);
        SafeParcelWriter.writeString(parcel, 4, this.zzu, false);
        SafeParcelWriter.writeLong(parcel, 5, this.durationMillis);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzej, i, false);
        if (this.zzec != null) {
            iBinder = this.zzec.asBinder();
        }
        SafeParcelWriter.writeIBinder(parcel, 7, iBinder, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
