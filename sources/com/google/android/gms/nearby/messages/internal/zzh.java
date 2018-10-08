package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

@Deprecated
public final class zzh extends zza {
    public static final Creator<zzh> CREATOR = new zzi();
    private int zzdxt;
    @Deprecated
    private String zzjdz;
    private zzp zzjfv;
    @Deprecated
    private ClientAppContext zzjfw;

    zzh(int i, IBinder iBinder, String str, ClientAppContext clientAppContext) {
        zzp zzp;
        this.zzdxt = i;
        if (iBinder == null) {
            zzp = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzjfv = zzp;
        this.zzjdz = str;
        this.zzjfw = ClientAppContext.zza(clientAppContext, null, str, false);
    }

    zzh(IBinder iBinder) {
        this(1, iBinder, null, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 3, this.zzjdz, false);
        zzd.zza(parcel, 4, this.zzjfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
