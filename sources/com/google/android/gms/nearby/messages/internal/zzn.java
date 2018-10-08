package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;

public abstract class zzn extends zzef implements zzm {
    public zzn() {
        attachInterface(this, "com.google.android.gms.nearby.messages.internal.IMessageListener");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zza((zzaf) zzeg.zza(parcel, zzaf.CREATOR));
                break;
            case 2:
                zzb((zzaf) zzeg.zza(parcel, zzaf.CREATOR));
                break;
            case 4:
                zzaf(parcel.createTypedArrayList(Update.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
