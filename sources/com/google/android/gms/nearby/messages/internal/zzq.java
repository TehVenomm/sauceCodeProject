package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzef;
import com.google.android.gms.internal.zzeg;

public abstract class zzq extends zzef implements zzp {
    public zzq() {
        attachInterface(this, "com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        if (i != 1) {
            return false;
        }
        zzag((Status) zzeg.zza(parcel, Status.CREATOR));
        return true;
    }
}
