package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzr extends zzee implements zzp {
    zzr(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.messages.internal.INearbyMessagesCallback");
    }

    public final void zzag(Status status) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) status);
        zzc(1, zzax);
    }
}
