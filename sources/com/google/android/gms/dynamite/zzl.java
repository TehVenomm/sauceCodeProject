package com.google.android.gms.dynamite;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.IObjectWrapper.zza;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzl extends zzee implements zzk {
    zzl(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.dynamite.IDynamiteLoader");
    }

    public final int zza(IObjectWrapper iObjectWrapper, String str, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) iObjectWrapper);
        zzax.writeString(str);
        zzeg.zza(zzax, z);
        zzax = zza(3, zzax);
        int readInt = zzax.readInt();
        zzax.recycle();
        return readInt;
    }

    public final IObjectWrapper zza(IObjectWrapper iObjectWrapper, String str, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) iObjectWrapper);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax = zza(2, zzax);
        IObjectWrapper zzao = zza.zzao(zzax.readStrongBinder());
        zzax.recycle();
        return zzao;
    }
}
