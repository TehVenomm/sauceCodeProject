package com.google.android.gms.iid;

import android.os.IBinder;
import android.os.Message;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzc extends zzee implements zzb {
    zzc(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.iid.IMessengerCompat");
    }

    public final void send(Message message) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) message);
        zzc(1, zzax);
    }
}
