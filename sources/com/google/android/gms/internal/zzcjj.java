package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzcjj extends zzef implements zzcji {
    public zzcjj() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IPayloadListener");
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
                zza((zzckh) zzeg.zza(parcel, zzckh.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
