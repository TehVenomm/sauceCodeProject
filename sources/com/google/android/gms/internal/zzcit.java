package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzcit extends zzef implements zzcis {
    public zzcit() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IConnectionEventListener");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 2:
                zza((zzckf) zzeg.zza(parcel, zzckf.CREATOR));
                break;
            case 3:
                zza((zzcjz) zzeg.zza(parcel, zzcjz.CREATOR));
                break;
            case 4:
                zza((zzckh) zzeg.zza(parcel, zzckh.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
