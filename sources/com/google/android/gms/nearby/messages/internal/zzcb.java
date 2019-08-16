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

@Class(creator = "RegisterStatusCallbackRequestCreator")
public final class zzcb extends AbstractSafeParcelable {
    public static final Creator<zzcb> CREATOR = new zzcc();
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Nullable
    @Field(mo13990id = 5)
    @Deprecated
    private String zzff;
    @Field(getter = "getCallbackAsBinder", mo13990id = 2, type = "android.os.IBinder")
    private final zzp zzhh;
    @Nullable
    @Field(mo13990id = 6)
    @Deprecated
    private final ClientAppContext zzhi;
    @Field(getter = "getStatusCallbackAsBinder", mo13990id = 3, type = "android.os.IBinder")
    private final zzx zziw;
    @Field(mo13990id = 4)
    public boolean zzix;

    @Constructor
    zzcb(@Param(mo13993id = 1) int i, @Param(mo13993id = 2) IBinder iBinder, @Param(mo13993id = 3) IBinder iBinder2, @Param(mo13993id = 4) boolean z, @Nullable @Param(mo13993id = 5) String str, @Nullable @Param(mo13993id = 6) ClientAppContext clientAppContext) {
        zzp zzr;
        zzx zzz;
        this.versionCode = i;
        if (iBinder == null) {
            zzr = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzr = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzhh = zzr;
        if (iBinder2 == null) {
            zzz = null;
        } else {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IStatusCallback");
            zzz = queryLocalInterface2 instanceof zzx ? (zzx) queryLocalInterface2 : new zzz(iBinder2);
        }
        this.zziw = zzz;
        this.zzix = z;
        this.zzff = str;
        this.zzhi = ClientAppContext.zza(clientAppContext, null, str, false);
    }

    @VisibleForTesting
    public zzcb(IBinder iBinder, IBinder iBinder2) {
        this(1, iBinder, iBinder2, false, null, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zzhh.asBinder(), false);
        SafeParcelWriter.writeIBinder(parcel, 3, this.zziw.asBinder(), false);
        SafeParcelWriter.writeBoolean(parcel, 4, this.zzix);
        SafeParcelWriter.writeString(parcel, 5, this.zzff, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzhi, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
