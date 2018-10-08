package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbc extends zza {
    public static final Creator<zzbc> CREATOR = new zzbd();
    private int zzdxt;
    @Deprecated
    private String zzjdz;
    @Deprecated
    private boolean zzjea;
    @Deprecated
    private String zzjfr;
    private zzp zzjfv;
    @Deprecated
    private ClientAppContext zzjfw;
    private zzaf zzjgk;

    zzbc(int i, zzaf zzaf, IBinder iBinder, String str, String str2, boolean z, ClientAppContext clientAppContext) {
        zzp zzp;
        this.zzdxt = i;
        this.zzjgk = zzaf;
        if (iBinder == null) {
            zzp = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzjfv = zzp;
        this.zzjdz = str;
        this.zzjfr = str2;
        this.zzjea = z;
        this.zzjfw = ClientAppContext.zza(clientAppContext, str2, str, z);
    }

    public zzbc(zzaf zzaf, IBinder iBinder, ClientAppContext clientAppContext) {
        this(1, zzaf, iBinder, null, null, false, clientAppContext);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjgk, i, false);
        zzd.zza(parcel, 3, this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 4, this.zzjdz, false);
        zzd.zza(parcel, 5, this.zzjfr, false);
        zzd.zza(parcel, 6, this.zzjea);
        zzd.zza(parcel, 7, this.zzjfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
