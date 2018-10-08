package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.internal.zzef;

public abstract class zzv extends zzef implements zzu {
    public zzv() {
        attachInterface(this, "com.google.android.gms.nearby.messages.internal.IPublishCallback");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        onExpired();
        return true;
    }
}
