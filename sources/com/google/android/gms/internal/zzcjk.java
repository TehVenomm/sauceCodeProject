package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzcjk extends zzee implements zzcji {
    zzcjk(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IPayloadListener");
    }

    public final void zza(zzckf zzckf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckf);
        zzc(2, zzax);
    }

    public final void zza(zzckh zzckh) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckh);
        zzc(3, zzax);
    }
}
