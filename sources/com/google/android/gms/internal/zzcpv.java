package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.common.internal.zzam;

public final class zzcpv extends zzee implements zzcpu {
    zzcpv(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.signin.internal.ISignInService");
    }

    public final void zza(zzam zzam, int i, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzam);
        zzax.writeInt(i);
        zzeg.zza(zzax, z);
        zzb(9, zzax);
    }

    public final void zza(zzcpx zzcpx, zzcps zzcps) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcpx);
        zzeg.zza(zzax, (IInterface) zzcps);
        zzb(12, zzax);
    }

    public final void zzeb(int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzb(7, zzax);
    }
}
