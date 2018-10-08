package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzcje extends zzef implements zzcjd {
    public zzcje() {
        attachInterface(this, "com.google.android.gms.nearby.internal.connection.IDiscoveryListener");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 2:
                zza((zzckb) zzeg.zza(parcel, zzckb.CREATOR));
                break;
            case 3:
                zza((zzckd) zzeg.zza(parcel, zzckd.CREATOR));
                break;
            case 4:
                zza((zzckn) zzeg.zza(parcel, zzckn.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
