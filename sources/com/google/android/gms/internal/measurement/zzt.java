package com.google.android.gms.internal.measurement;

import android.os.Bundle;
import android.os.Parcel;
import android.os.RemoteException;

public abstract class zzt extends zza implements zzq {
    public zzt() {
        super("com.google.android.gms.measurement.api.internal.IEventHandlerProxy");
    }

    /* access modifiers changed from: protected */
    public final boolean zza(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                onEvent(parcel.readString(), parcel.readString(), (Bundle) zzd.zza(parcel, Bundle.CREATOR), parcel.readLong());
                parcel2.writeNoException();
                break;
            case 2:
                int id = mo17478id();
                parcel2.writeNoException();
                parcel2.writeInt(id);
                break;
            default:
                return false;
        }
        return true;
    }
}
