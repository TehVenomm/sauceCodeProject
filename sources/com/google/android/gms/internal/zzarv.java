package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzarv extends zzee implements zzaru {
    zzarv(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.api.accounttransfer.internal.IAccountTransferService");
    }

    public final void zza(zzars zzars, zzarq zzarq) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzars);
        zzeg.zza(zzax, (Parcelable) zzarq);
        zzb(7, zzax);
    }

    public final void zza(zzars zzars, zzarw zzarw) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzars);
        zzeg.zza(zzax, (Parcelable) zzarw);
        zzb(9, zzax);
    }

    public final void zza(zzars zzars, zzary zzary) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzars);
        zzeg.zza(zzax, (Parcelable) zzary);
        zzb(6, zzax);
    }

    public final void zza(zzars zzars, zzasa zzasa) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzars);
        zzeg.zza(zzax, (Parcelable) zzasa);
        zzb(5, zzax);
    }

    public final void zza(zzars zzars, zzasc zzasc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzars);
        zzeg.zza(zzax, (Parcelable) zzasc);
        zzb(8, zzax);
    }
}
