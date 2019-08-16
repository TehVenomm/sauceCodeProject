package com.google.firebase.iid;

import android.os.IBinder;
import android.os.Message;
import android.os.Parcel;
import android.os.RemoteException;

public final class zzv implements zzw {
    private final IBinder zzbt;

    zzv(IBinder iBinder) {
        this.zzbt = iBinder;
    }

    public final IBinder asBinder() {
        return this.zzbt;
    }

    public final void send(Message message) throws RemoteException {
        Parcel obtain = Parcel.obtain();
        obtain.writeInterfaceToken("com.google.android.gms.iid.IMessengerCompat");
        obtain.writeInt(1);
        message.writeToParcel(obtain, 0);
        try {
            this.zzbt.transact(1, obtain, null, 1);
        } finally {
            obtain.recycle();
        }
    }
}
