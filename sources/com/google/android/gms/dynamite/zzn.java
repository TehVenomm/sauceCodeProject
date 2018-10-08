package com.google.android.gms.dynamite;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.IObjectWrapper.zza;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzn extends zzee implements zzm {
    zzn(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.dynamite.IDynamiteLoaderV2");
    }

    public final IObjectWrapper zza(IObjectWrapper iObjectWrapper, String str, int i, IObjectWrapper iObjectWrapper2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) iObjectWrapper);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzeg.zza(zzax, (IInterface) iObjectWrapper2);
        zzax = zza(2, zzax);
        IObjectWrapper zzao = zza.zzao(zzax.readStrongBinder());
        zzax.recycle();
        return zzao;
    }
}
