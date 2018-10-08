package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzciu extends zzee implements zzcis {
    zzciu(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IConnectionEventListener");
    }

    public final void zza(zzcjz zzcjz) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcjz);
        zzc(3, zzax);
    }

    public final void zza(zzckf zzckf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckf);
        zzc(2, zzax);
    }

    public final void zza(zzckh zzckh) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckh);
        zzc(4, zzax);
    }
}
