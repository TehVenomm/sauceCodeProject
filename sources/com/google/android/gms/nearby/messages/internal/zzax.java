package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.nearby.messages.Strategy;

public final class zzax extends zza {
    public static final Creator<zzax> CREATOR = new zzay();
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
    private Strategy zzjgl;
    @Deprecated
    private boolean zzjgm;
    private zzu zzjgn;

    zzax(int i, zzaf zzaf, Strategy strategy, IBinder iBinder, String str, String str2, boolean z, IBinder iBinder2, boolean z2, ClientAppContext clientAppContext) {
        zzp zzp;
        zzu zzu = null;
        this.zzdxt = i;
        this.zzjgk = zzaf;
        this.zzjgl = strategy;
        if (iBinder == null) {
            zzp = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
            zzp = queryLocalInterface instanceof zzp ? (zzp) queryLocalInterface : new zzr(iBinder);
        }
        this.zzjfv = zzp;
        this.zzjdz = str;
        this.zzjfr = str2;
        this.zzjgm = z;
        if (!(iBinder2 == null || iBinder2 == null)) {
            queryLocalInterface = iBinder2.queryLocalInterface("com.google.android.gms.nearby.messages.internal.IPublishCallback");
            zzu = queryLocalInterface instanceof zzu ? (zzu) queryLocalInterface : new zzw(iBinder2);
        }
        this.zzjgn = zzu;
        this.zzjea = z2;
        this.zzjfw = ClientAppContext.zza(clientAppContext, str2, str, z2);
    }

    public zzax(zzaf zzaf, Strategy strategy, IBinder iBinder, IBinder iBinder2) {
        this(2, zzaf, strategy, iBinder, null, null, false, iBinder2, false, null);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.zzjgk, i, false);
        zzd.zza(parcel, 3, this.zzjgl, i, false);
        zzd.zza(parcel, 4, this.zzjfv.asBinder(), false);
        zzd.zza(parcel, 5, this.zzjdz, false);
        zzd.zza(parcel, 6, this.zzjfr, false);
        zzd.zza(parcel, 7, this.zzjgm);
        zzd.zza(parcel, 8, this.zzjgn == null ? null : this.zzjgn.asBinder(), false);
        zzd.zza(parcel, 9, this.zzjea);
        zzd.zza(parcel, 10, this.zzjfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
