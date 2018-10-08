package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzaz extends zza {
    public static final Creator<zzaz> CREATOR = new zzba();
    private int versionCode;
    @Deprecated
    private String zzjdz;
    private zzp zzjfv;
    @Deprecated
    private ClientAppContext zzjfw;
    private zzx zzjgo;
    public boolean zzjgp;

    zzaz(int i, IBinder iBinder, IBinder iBinder2, boolean z, String str, ClientAppContext clientAppContext) {
        zzp zzp;
        zzx zzx;
        this.versionCode = i;
        if (iBinder == null) {
            zzp = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzjfv = zzp;
        if (iBinder2 == null) {
            zzx = null;
        } else {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IStatusCallback");
            zzx = queryLocalInterface instanceof zzx ? (zzx) queryLocalInterface : new zzz(iBinder2);
        }
        this.zzjgo = zzx;
        this.zzjgp = z;
        this.zzjdz = str;
        this.zzjfw = ClientAppContext.zza(clientAppContext, null, str, false);
    }

    public zzaz(IBinder iBinder, IBinder iBinder2) {
        this(1, iBinder, iBinder2, false, null, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjgo.asBinder(), false);
        zzd.zza(parcel, 4, this.zzjgp);
        zzd.zza(parcel, 5, this.zzjdz, false);
        zzd.zza(parcel, 6, this.zzjfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
