package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

final class zzay implements zzax {
    private final IBinder zzakc;

    zzay(IBinder iBinder) {
        this.zzakc = iBinder;
    }

    public final IBinder asBinder() {
        return this.zzakc;
    }

    public final void zza(zzav zzav, zzy zzy) throws RemoteException {
        Parcel obtain = Parcel.obtain();
        Parcel obtain2 = Parcel.obtain();
        try {
            obtain.writeInterfaceToken("com.google.android.gms.common.internal.IGmsServiceBroker");
            obtain.writeStrongBinder(zzav.asBinder());
            obtain.writeInt(1);
            zzy.writeToParcel(obtain, 0);
            this.zzakc.transact(46, obtain, obtain2, 0);
            obtain2.readException();
        } finally {
            obtain2.recycle();
            obtain.recycle();
        }
    }
}
