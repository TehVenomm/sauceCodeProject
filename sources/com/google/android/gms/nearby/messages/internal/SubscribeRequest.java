package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.nearby.messages.MessageFilter;
import com.google.android.gms.nearby.messages.Strategy;

public final class SubscribeRequest extends zza implements ReflectedParcelable {
    public static final Creator<SubscribeRequest> CREATOR = new zzbb();
    private int zzdxt;
    private PendingIntent zzeaa;
    @Deprecated
    private String zzjdz;
    @Deprecated
    private boolean zzjea;
    private boolean zzjev;
    private int zzjew;
    @Deprecated
    private String zzjfr;
    private zzp zzjfv;
    @Deprecated
    private ClientAppContext zzjfw;
    private Strategy zzjgl;
    @Deprecated
    private boolean zzjgm;
    private zzm zzjgq;
    private MessageFilter zzjgr;
    @Deprecated
    private int zzjgs;
    private byte[] zzjgt;
    private zzaa zzjgu;

    public SubscribeRequest(int i, IBinder iBinder, Strategy strategy, IBinder iBinder2, MessageFilter messageFilter, PendingIntent pendingIntent, int i2, String str, String str2, byte[] bArr, boolean z, IBinder iBinder3, boolean z2, ClientAppContext clientAppContext, boolean z3, int i3) {
        zzm zzm;
        zzp zzp;
        zzaa zzaa;
        this.zzdxt = i;
        if (iBinder == null) {
            zzm = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IMessageListener");
            zzm = queryLocalInterface instanceof zzm ? (zzm) queryLocalInterface : new zzo(iBinder);
        }
        this.zzjgq = zzm;
        this.zzjgl = strategy;
        if (iBinder2 == null) {
            zzp = null;
        } else {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder2);
        }
        this.zzjfv = zzp;
        this.zzjgr = messageFilter;
        this.zzeaa = pendingIntent;
        this.zzjgs = i2;
        this.zzjdz = str;
        this.zzjfr = str2;
        this.zzjgt = bArr;
        this.zzjgm = z;
        if (iBinder3 == null) {
            zzaa = null;
        } else if (iBinder3 == null) {
            zzaa = null;
        } else {
            queryLocalInterface = iBinder3.queryLocalInterface("com.google.android.gms.nearby.messages.internal.ISubscribeCallback");
            zzaa = queryLocalInterface instanceof zzaa ? (zzaa) queryLocalInterface : new zzac(iBinder3);
        }
        this.zzjgu = zzaa;
        this.zzjea = z2;
        this.zzjfw = ClientAppContext.zza(clientAppContext, str2, str, z2);
        this.zzjev = z3;
        this.zzjew = i3;
    }

    public SubscribeRequest(IBinder iBinder, Strategy strategy, IBinder iBinder2, MessageFilter messageFilter, PendingIntent pendingIntent, byte[] bArr, IBinder iBinder3, boolean z) {
        this(iBinder, strategy, iBinder2, messageFilter, null, null, iBinder3, z, 0);
    }

    public SubscribeRequest(IBinder iBinder, Strategy strategy, IBinder iBinder2, MessageFilter messageFilter, PendingIntent pendingIntent, byte[] bArr, IBinder iBinder3, boolean z, int i) {
        this(3, iBinder, strategy, iBinder2, messageFilter, pendingIntent, 0, null, null, bArr, false, iBinder3, false, null, z, i);
    }

    public final String toString() {
        String str;
        String valueOf = String.valueOf(this.zzjgq);
        String valueOf2 = String.valueOf(this.zzjgl);
        String valueOf3 = String.valueOf(this.zzjfv);
        String valueOf4 = String.valueOf(this.zzjgr);
        String valueOf5 = String.valueOf(this.zzeaa);
        if (this.zzjgt == null) {
            str = null;
        } else {
            str = "<" + this.zzjgt.length + " bytes>";
        }
        String valueOf6 = String.valueOf(this.zzjgu);
        boolean z = this.zzjea;
        String valueOf7 = String.valueOf(this.zzjfw);
        boolean z2 = this.zzjev;
        String str2 = this.zzjdz;
        String str3 = this.zzjfr;
        return new StringBuilder((((((((((String.valueOf(valueOf).length() + 263) + String.valueOf(valueOf2).length()) + String.valueOf(valueOf3).length()) + String.valueOf(valueOf4).length()) + String.valueOf(valueOf5).length()) + String.valueOf(str).length()) + String.valueOf(valueOf6).length()) + String.valueOf(valueOf7).length()) + String.valueOf(str2).length()) + String.valueOf(str3).length()).append("SubscribeRequest{messageListener=").append(valueOf).append(", strategy=").append(valueOf2).append(", callback=").append(valueOf3).append(", filter=").append(valueOf4).append(", pendingIntent=").append(valueOf5).append(", hint=").append(str).append(", subscribeCallback=").append(valueOf6).append(", useRealClientApiKey=").append(z).append(", clientAppContext=").append(valueOf7).append(", isDiscardPendingIntent=").append(z2).append(", zeroPartyPackageName=").append(str2).append(", realClientPackageName=").append(str3).append(", isIgnoreNearbyPermission=").append(this.zzjgm).append("}").toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder = null;
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjgq == null ? null : this.zzjgq.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjgl, i, false);
        zzd.zza(parcel, 4, this.zzjfv == null ? null : this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 5, this.zzjgr, i, false);
        zzd.zza(parcel, 6, this.zzeaa, i, false);
        zzd.zzc(parcel, 7, this.zzjgs);
        zzd.zza(parcel, 8, this.zzjdz, false);
        zzd.zza(parcel, 9, this.zzjfr, false);
        zzd.zza(parcel, 10, this.zzjgt, false);
        zzd.zza(parcel, 11, this.zzjgm);
        if (this.zzjgu != null) {
            iBinder = this.zzjgu.asBinder();
        }
        zzd.zza(parcel, 12, iBinder, false);
        zzd.zza(parcel, 13, this.zzjea);
        zzd.zza(parcel, 14, this.zzjfw, i, false);
        zzd.zza(parcel, 15, this.zzjev);
        zzd.zzc(parcel, 16, this.zzjew);
        zzd.zzai(parcel, zze);
    }
}
