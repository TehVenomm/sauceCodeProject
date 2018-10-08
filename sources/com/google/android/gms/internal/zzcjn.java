package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

public final class zzcjn extends zzee implements zzcjl {
    zzcjn(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.IResultListener");
    }

    public final void zzdw(int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzc(2, zzax);
    }
}
