package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzciw extends zzef implements zzciv {
    public zzciw() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IConnectionLifecycleListener");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 2:
                zza((zzcjr) zzeg.zza(parcel, zzcjr.CREATOR));
                break;
            case 3:
                zza((zzcjx) zzeg.zza(parcel, zzcjx.CREATOR));
                break;
            case 4:
                zza((zzcjz) zzeg.zza(parcel, zzcjz.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
