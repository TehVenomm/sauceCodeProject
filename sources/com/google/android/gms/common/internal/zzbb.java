package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.common.zzm;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzbb extends zzee implements zzaz {
    zzbb(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.common.internal.IGoogleCertificatesApi");
    }

    public final boolean zza(zzm zzm, IObjectWrapper iObjectWrapper) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzm);
        zzeg.zza(zzax, (IInterface) iObjectWrapper);
        zzax = zza(5, zzax);
        boolean zza = zzeg.zza(zzax);
        zzax.recycle();
        return zza;
    }
}
