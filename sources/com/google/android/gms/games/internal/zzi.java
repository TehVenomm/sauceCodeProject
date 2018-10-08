package com.google.android.gms.games.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;

public abstract class zzi extends zzef implements zzh {
    public zzi() {
        attachInterface(this, "com.google.android.gms.games.internal.IGamesClient");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1001) {
            return false;
        }
        Parcelable zzapu = zzapu();
        parcel2.writeNoException();
        zzeg.zzb(parcel2, zzapu);
        return true;
    }
}
