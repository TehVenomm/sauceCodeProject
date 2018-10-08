package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzfi extends zzef implements zzfh {
    public static zzfh zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface queryLocalInterface = iBinder.queryLocalInterface(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
        return queryLocalInterface instanceof zzfh ? (zzfh) queryLocalInterface : new zzfj(iBinder);
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        throw new NoSuchMethodError();
    }
}
