package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.VersionField;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.nearby.messages.MessageFilter;
import com.google.android.gms.nearby.messages.Strategy;

@Class(creator = "SubscribeRequestCreator")
public final class SubscribeRequest extends AbstractSafeParcelable implements ReflectedParcelable {
    public static final Creator<SubscribeRequest> CREATOR = new zzcd();
    @VersionField(mo13996id = 1)
    private final int versionCode;
    @Nullable
    @Field(mo13990id = 8)
    @Deprecated
    private final String zzff;
    @Field(mo13990id = 13)
    @Deprecated
    private final boolean zzfg;
    @Nullable
    @Field(mo13990id = 9)
    @Deprecated
    private final String zzfj;
    @Field(mo13990id = 15)
    private final boolean zzgb;
    @Field(mo13990id = 16)
    private final int zzgc;
    @Field(mo13990id = 17)
    private final int zzhf;
    @Field(getter = "getCallbackAsBinder", mo13990id = 4, type = "android.os.IBinder")
    private final zzp zzhh;
    @Field(mo13990id = 14)
    @Deprecated
    private final ClientAppContext zzhi;
    @Field(mo13990id = 3)
    private final Strategy zzit;
    @Field(mo13990id = 11)
    @Deprecated
    private final boolean zziu;
    @Nullable
    @Field(getter = "getMessageListenerAsBinder", mo13990id = 2, type = "android.os.IBinder")
    private final zzm zziy;
    @Field(mo13990id = 5)
    private final MessageFilter zziz;
    @Nullable
    @Field(mo13990id = 6)
    private final PendingIntent zzja;
    @Field(mo13990id = 7)
    @Deprecated
    private final int zzjb;
    @Nullable
    @Field(mo13990id = 10)
    private final byte[] zzjc;
    @Nullable
    @Field(getter = "getSubscribeCallbackAsBinder", mo13990id = 12, type = "android.os.IBinder")
    private final zzaa zzjd;

    @Constructor
    @VisibleForTesting
    public SubscribeRequest(@Param(mo13993id = 1) int i, @Nullable @Param(mo13993id = 2) IBinder iBinder, @Param(mo13993id = 3) Strategy strategy, @Param(mo13993id = 4) IBinder iBinder2, @Param(mo13993id = 5) MessageFilter messageFilter, @Nullable @Param(mo13993id = 6) PendingIntent pendingIntent, @Param(mo13993id = 7) int i2, @Nullable @Param(mo13993id = 8) String str, @Nullable @Param(mo13993id = 9) String str2, @Nullable @Param(mo13993id = 10) byte[] bArr, @Param(mo13993id = 11) boolean z, @Nullable @Param(mo13993id = 12) IBinder iBinder3, @Param(mo13993id = 13) boolean z2, @Nullable @Param(mo13993id = 14) ClientAppContext clientAppContext, @Param(mo13993id = 15) boolean z3, @Param(mo13993id = 16) int i3, @Param(mo13993id = 17) int i4) {
        zzm zzo;
        zzp zzr;
        zzaa zzac;
        this.versionCode = i;
        if (iBinder == null) {
            zzo = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IMessageListener");
            zzo = queryLocalInterface instanceof zzm ? (zzm) queryLocalInterface : new zzo(iBinder);
        }
        this.zziy = zzo;
        this.zzit = strategy;
        if (iBinder2 == null) {
            zzr = null;
        } else {
            IInterface queryLocalInterface2 = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzr = queryLocalInterface2 instanceof zzp ? (zzp) queryLocalInterface2 : new zzr(iBinder2);
        }
        this.zzhh = zzr;
        this.zziz = messageFilter;
        this.zzja = pendingIntent;
        this.zzjb = i2;
        this.zzff = str;
        this.zzfj = str2;
        this.zzjc = bArr;
        this.zziu = z;
        if (iBinder3 == null) {
            zzac = null;
        } else if (iBinder3 == null) {
            zzac = null;
        } else {
            IInterface queryLocalInterface3 = iBinder3.queryLocalInterface("com.google.android.gms.nearby.messages.internal.ISubscribeCallback");
            zzac = queryLocalInterface3 instanceof zzaa ? (zzaa) queryLocalInterface3 : new zzac(iBinder3);
        }
        this.zzjd = zzac;
        this.zzfg = z2;
        this.zzhi = ClientAppContext.zza(clientAppContext, str2, str, z2);
        this.zzgb = z3;
        this.zzgc = i3;
        this.zzhf = i4;
    }

    public SubscribeRequest(IBinder iBinder, Strategy strategy, IBinder iBinder2, MessageFilter messageFilter, PendingIntent pendingIntent, @Nullable byte[] bArr, @Nullable IBinder iBinder3, boolean z, int i) {
        this(iBinder, strategy, iBinder2, messageFilter, null, bArr, iBinder3, z, 0, i);
    }

    public SubscribeRequest(IBinder iBinder, Strategy strategy, IBinder iBinder2, MessageFilter messageFilter, PendingIntent pendingIntent, @Nullable byte[] bArr, @Nullable IBinder iBinder3, boolean z, int i, int i2) {
        this(3, iBinder, strategy, iBinder2, messageFilter, pendingIntent, 0, null, null, bArr, false, iBinder3, false, null, z, i, i2);
    }

    public final String toString() {
        String str;
        String valueOf = String.valueOf(this.zziy);
        String valueOf2 = String.valueOf(this.zzit);
        String valueOf3 = String.valueOf(this.zzhh);
        String valueOf4 = String.valueOf(this.zziz);
        String valueOf5 = String.valueOf(this.zzja);
        if (this.zzjc == null) {
            str = null;
        } else {
            str = "<" + this.zzjc.length + " bytes>";
        }
        String valueOf6 = String.valueOf(this.zzjd);
        boolean z = this.zzfg;
        String valueOf7 = String.valueOf(this.zzhi);
        boolean z2 = this.zzgb;
        String str2 = this.zzff;
        String str3 = this.zzfj;
        boolean z3 = this.zziu;
        return new StringBuilder(String.valueOf(valueOf).length() + 291 + String.valueOf(valueOf2).length() + String.valueOf(valueOf3).length() + String.valueOf(valueOf4).length() + String.valueOf(valueOf5).length() + String.valueOf(str).length() + String.valueOf(valueOf6).length() + String.valueOf(valueOf7).length() + String.valueOf(str2).length() + String.valueOf(str3).length()).append("SubscribeRequest{messageListener=").append(valueOf).append(", strategy=").append(valueOf2).append(", callback=").append(valueOf3).append(", filter=").append(valueOf4).append(", pendingIntent=").append(valueOf5).append(", hint=").append(str).append(", subscribeCallback=").append(valueOf6).append(", useRealClientApiKey=").append(z).append(", clientAppContext=").append(valueOf7).append(", isDiscardPendingIntent=").append(z2).append(", zeroPartyPackageName=").append(str2).append(", realClientPackageName=").append(str3).append(", isIgnoreNearbyPermission=").append(z3).append(", callingContext=").append(this.zzhf).append("}").toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeInt(parcel, 1, this.versionCode);
        SafeParcelWriter.writeIBinder(parcel, 2, this.zziy == null ? null : this.zziy.asBinder(), false);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzit, i, false);
        SafeParcelWriter.writeIBinder(parcel, 4, this.zzhh == null ? null : this.zzhh.asBinder(), false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zziz, i, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzja, i, false);
        SafeParcelWriter.writeInt(parcel, 7, this.zzjb);
        SafeParcelWriter.writeString(parcel, 8, this.zzff, false);
        SafeParcelWriter.writeString(parcel, 9, this.zzfj, false);
        SafeParcelWriter.writeByteArray(parcel, 10, this.zzjc, false);
        SafeParcelWriter.writeBoolean(parcel, 11, this.zziu);
        if (this.zzjd != null) {
            iBinder = this.zzjd.asBinder();
        }
        SafeParcelWriter.writeIBinder(parcel, 12, iBinder, false);
        SafeParcelWriter.writeBoolean(parcel, 13, this.zzfg);
        SafeParcelWriter.writeParcelable(parcel, 14, this.zzhi, i, false);
        SafeParcelWriter.writeBoolean(parcel, 15, this.zzgb);
        SafeParcelWriter.writeInt(parcel, 16, this.zzgc);
        SafeParcelWriter.writeInt(parcel, 17, this.zzhf);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
