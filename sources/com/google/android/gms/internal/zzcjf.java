package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzcjf extends zzee implements zzcjd {
    zzcjf(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IDiscoveryListener");
    }

    public final void zza(zzckb zzckb) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckb);
        zzc(2, zzax);
    }

    public final void zza(zzckd zzckd) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckd);
        zzc(3, zzax);
    }

    public final void zza(zzckn zzckn) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckn);
        zzc(4, zzax);
    }
}
