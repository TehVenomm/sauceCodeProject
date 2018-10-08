package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzblg extends zzef implements zzblf {
    public zzblg() {
        attachInterface(this, "com.google.android.gms.drive.internal.IEventCallback");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        zzc((zzblw) zzeg.zza(parcel, zzblw.CREATOR));
        parcel2.writeNoException();
        return true;
    }
}
