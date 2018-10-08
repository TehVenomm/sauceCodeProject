package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzcir extends zzee implements zzcip {
    zzcir(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IAdvertisingCallback");
    }

    public final void zza(zzcjt zzcjt) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcjt);
        zzc(2, zzax);
    }

    public final void zza(zzckl zzckl) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckl);
        zzc(3, zzax);
    }
}
