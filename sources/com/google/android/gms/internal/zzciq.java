package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzciq extends zzef implements zzcip {
    public zzciq() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IAdvertisingCallback");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 2:
                zza((zzcjt) zzeg.zza(parcel, zzcjt.CREATOR));
                break;
            case 3:
                zza((zzckl) zzeg.zza(parcel, zzckl.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
