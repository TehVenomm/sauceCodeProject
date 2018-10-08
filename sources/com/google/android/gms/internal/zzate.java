package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;

public final class zzate extends zzee implements zzatd {
    zzate(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.api.phone.internal.ISmsRetrieverApiService");
    }

    public final void zza(zzatf zzatf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzatf);
        zzb(1, zzax);
    }
}
