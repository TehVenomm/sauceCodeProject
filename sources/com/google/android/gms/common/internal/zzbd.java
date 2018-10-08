package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.IObjectWrapper.zza;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzbd extends zzee implements zzbc {
    zzbd(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.common.internal.ISignInButtonCreator");
    }

    public final IObjectWrapper zza(IObjectWrapper iObjectWrapper, zzbu zzbu) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) iObjectWrapper);
        zzeg.zza(zzax, (Parcelable) zzbu);
        zzax = zza(2, zzax);
        IObjectWrapper zzao = zza.zzao(zzax.readStrongBinder());
        zzax.recycle();
        return zzao;
    }
}
