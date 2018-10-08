package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.VisibleForTesting;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbe extends zza {
    public static final Creator<zzbe> CREATOR = new zzbf();
    private int zzdxt;
    private PendingIntent zzeaa;
    @Deprecated
    private String zzjdz;
    @Deprecated
    private boolean zzjea;
    @Deprecated
    private String zzjfr;
    private zzp zzjfv;
    @Deprecated
    private ClientAppContext zzjfw;
    private zzm zzjgq;
    @Deprecated
    private int zzjgs;

    @VisibleForTesting
    public zzbe(int i, IBinder iBinder, IBinder iBinder2, PendingIntent pendingIntent, int i2, String str, String str2, boolean z, ClientAppContext clientAppContext) {
        zzm zzm;
        zzp zzp = null;
        this.zzdxt = i;
        if (iBinder == null) {
            zzm = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IMessageListener");
            zzm = queryLocalInterface instanceof zzm ? (zzm) queryLocalInterface : new zzo(iBinder);
        }
        this.zzjgq = zzm;
        if (iBinder2 != null) {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder2);
        }
        this.zzjfv = zzp;
        this.zzeaa = pendingIntent;
        this.zzjgs = i2;
        this.zzjdz = str;
        this.zzjfr = str2;
        this.zzjea = z;
        this.zzjfw = ClientAppContext.zza(clientAppContext, str2, str, z);
    }

    @VisibleForTesting
    public zzbe(IBinder iBinder, IBinder iBinder2, PendingIntent pendingIntent) {
        this(1, iBinder, iBinder2, pendingIntent, 0, null, null, false, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjgq == null ? null : this.zzjgq.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 4, this.zzeaa, i, false);
        zzd.zzc(parcel, 5, this.zzjgs);
        zzd.zza(parcel, 6, this.zzjdz, false);
        zzd.zza(parcel, 7, this.zzjfr, false);
        zzd.zza(parcel, 8, this.zzjea);
        zzd.zza(parcel, 9, this.zzjfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
