package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzciz extends zzef implements zzciy {
    public zzciz() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IConnectionResponseListener");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 2) {
            return false;
        }
        zza((zzcjv) zzeg.zza(parcel, zzcjv.CREATOR));
        return true;
    }
}
