package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.auth.api.proxy.ProxyRequest;

public final class zzatc extends zzee implements zzatb {
    zzatc(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.api.internal.IAuthService");
    }

    public final void zza(zzasz zzasz, ProxyRequest proxyRequest) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzasz);
        zzeg.zza(zzax, (Parcelable) proxyRequest);
        zzb(1, zzax);
    }
}
