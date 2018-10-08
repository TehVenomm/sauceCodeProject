package com.google.android.gms.auth.account;

import android.accounts.Account;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zze extends zzee implements zzc {
    zze(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.account.IWorkAccountService");
    }

    public final void zza(zza zza, Account account) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zza);
        zzeg.zza(zzax, (Parcelable) account);
        zzb(3, zzax);
    }

    public final void zza(zza zza, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zza);
        zzax.writeString(str);
        zzb(2, zzax);
    }

    public final void zzap(boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, z);
        zzb(1, zzax);
    }
}
