package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzcix extends zzee implements zzciv {
    zzcix(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener");
    }

    public final void zza(zzcjr zzcjr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcjr);
        zzc(2, zzax);
    }

    public final void zza(zzcjx zzcjx) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcjx);
        zzc(3, zzax);
    }

    public final void zza(zzcjz zzcjz) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcjz);
        zzc(4, zzax);
    }
}
