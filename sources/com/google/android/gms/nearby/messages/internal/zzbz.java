package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.nearby.messages.Strategy;

@Class(creator = "PublishRequestCreator")
public final class zzbz extends AbstractSafeParcelable {
    public static final Creator<zzbz> CREATOR = new zzca();
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Nullable
    @Field(mo13990id = 5)
    @Deprecated
    private final String zzff;
    @Field(mo13990id = 9)
    @Deprecated
    private final boolean zzfg;
    @Nullable
    @Field(mo13990id = 6)
    @Deprecated
    private final String zzfj;
    @Field(mo13990id = 11)
    private final int zzhf;
    @Field(getter = "getCallbackAsBinder", mo13990id = 4, type = "android.os.IBinder")
    private final zzp zzhh;
    @Field(mo13990id = 10)
    @Deprecated
    private final ClientAppContext zzhi;
    @Field(mo13990id = 2)
    private final zzaf zzis;
    @Field(mo13990id = 3)
    private final Strategy zzit;
    @Field(mo13990id = 7)
    @Deprecated
    private final boolean zziu;
    @Nullable
    @Field(getter = "getPublishCallbackAsBinder", mo13990id = 8, type = "android.os.IBinder")
    private final zzu zziv;

    @Constructor
    zzbz(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) zzaf zzaf, @Param(mo13993id = 3) Strategy strategy, @Param(mo13993id = 4) IBinder iBinder, @Nullable @Param(mo13993id = 5) String str, @Nullable @Param(mo13993id = 6) String str2, @Param(mo13993id = 7) boolean z, @Nullable @Param(mo13993id = 8) IBinder iBinder2, @Param(mo13993id = 9) boolean z2, @Nullable @Param(mo13993id = 10) ClientAppContext clientAppContext, @Param(mo13993id = 11) int i2) {
        zzp zzr;
        zzu zzu = null;
        this.versionCode = i;
        this.zzis = zzaf;
        this.zzit = strategy;
        if (iBinder == null) {
            zzr = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzr = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzhh = zzr;
        this.zzff = str;
        this.zzfj = str2;
        this.zziu = z;
        if (!(iBinder2 == null || iBinder2 == null)) {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IPublishCallback");
            zzu = queryLocalInterface2 instanceof zzu ? (zzu) queryLocalInterface2 : new zzw(iBinder2);
        }
        this.zziv = zzu;
        this.zzfg = z2;
        this.zzhi = ClientAppContext.zza(clientAppContext, str2, str, z2);
        this.zzhf = i2;
    }

    @VisibleForTesting
    public zzbz(zzaf zzaf, Strategy strategy, IBinder iBinder, @Nullable IBinder iBinder2, int i) {
        this(2, zzaf, strategy, iBinder, null, null, false, iBinder2, false, null, i);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeParcelable(parcel, 2, this.zzis, i, false);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzit, i, false);
        SafeParcelWriter.writeIBinder(parcel, 4, this.zzhh.asBinder(), false);
        SafeParcelWriter.writeString(parcel, 5, this.zzff, false);
        SafeParcelWriter.writeString(parcel, 6, this.zzfj, false);
        SafeParcelWriter.writeBoolean(parcel, 7, this.zziu);
        SafeParcelWriter.writeIBinder(parcel, 8, this.zziv == null ? null : this.zziv.asBinder(), false);
        SafeParcelWriter.writeBoolean(parcel, 9, this.zzfg);
        SafeParcelWriter.writeParcelable(parcel, 10, this.zzhi, i, false);
        SafeParcelWriter.writeInt(parcel, 11, this.zzhf);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
