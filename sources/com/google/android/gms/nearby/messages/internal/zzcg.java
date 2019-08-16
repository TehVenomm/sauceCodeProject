package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.support.annotation.VisibleForTesting;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;

@Class(creator = "UnsubscribeRequestCreator")
public final class zzcg extends AbstractSafeParcelable {
    public static final Creator<zzcg> CREATOR = new zzch();
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Nullable
    @Field(mo13990id = 6)
    @Deprecated
    private final String zzff;
    @Field(mo13990id = 8)
    @Deprecated
    private final boolean zzfg;
    @Nullable
    @Field(mo13990id = 7)
    @Deprecated
    private final String zzfj;
    @Field(getter = "getCallbackAsBinder", mo13990id = 3, type = "android.os.IBinder")
    private final zzp zzhh;
    @Nullable
    @Field(mo13990id = 9)
    @Deprecated
    private final ClientAppContext zzhi;
    @Nullable
    @Field(getter = "getMessageListenerAsBinder", mo13990id = 2, type = "android.os.IBinder")
    private final zzm zziy;
    @Nullable
    @Field(mo13990id = 4)
    private final PendingIntent zzja;
    @Field(mo13990id = 5)
    @Deprecated
    private final int zzjb;

    @VisibleForTesting
    @Constructor
    public zzcg(@Param(mo13993id = 1) int i, @Nullable @Param(mo13993id = 2) IBinder iBinder, @Param(mo13993id = 3) IBinder iBinder2, @Nullable @Param(mo13993id = 4) PendingIntent pendingIntent, @Param(mo13993id = 5) int i2, @Nullable @Param(mo13993id = 6) String str, @Nullable @Param(mo13993id = 7) String str2, @Param(mo13993id = 8) boolean z, @Nullable @Param(mo13993id = 9) ClientAppContext clientAppContext) {
        zzm zzo;
        zzp zzp = null;
        this.versionCode = i;
        if (iBinder == null) {
            zzo = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IMessageListener");
            zzo = queryLocalInterface instanceof zzm ? (zzm) queryLocalInterface : new zzo(iBinder);
        }
        this.zziy = zzo;
        if (iBinder2 != null) {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface2 instanceof zzp ? (zzp) queryLocalInterface2 : new zzr(iBinder2);
        }
        this.zzhh = zzp;
        this.zzja = pendingIntent;
        this.zzjb = i2;
        this.zzff = str;
        this.zzfj = str2;
        this.zzfg = z;
        this.zzhi = ClientAppContext.zza(clientAppContext, str2, str, z);
    }

    @VisibleForTesting
    public zzcg(IBinder iBinder, IBinder iBinder2, @Nullable PendingIntent pendingIntent) {
        this(1, iBinder, iBinder2, pendingIntent, 0, null, null, false, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zziy == null ? null : this.zziy.asBinder(), false);
        SafeParcelWriter.writeIBinder(parcel, 3, this.zzhh.asBinder(), false);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzja, i, false);
        SafeParcelWriter.writeInt(parcel, 5, this.zzjb);
        SafeParcelWriter.writeString(parcel, 6, this.zzff, false);
        SafeParcelWriter.writeString(parcel, 7, this.zzfj, false);
        SafeParcelWriter.writeBoolean(parcel, 8, this.zzfg);
        SafeParcelWriter.writeParcelable(parcel, 9, this.zzhi, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
