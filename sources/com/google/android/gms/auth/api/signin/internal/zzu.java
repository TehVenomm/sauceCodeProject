package com.google.android.gms.auth.api.signin.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzu extends zzee implements zzt {
    zzu(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.api.signin.internal.ISignInService");
    }

    public final void zza(zzr zzr, GoogleSignInOptions googleSignInOptions) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzr);
        zzeg.zza(zzax, (Parcelable) googleSignInOptions);
        zzb(101, zzax);
    }

    public final void zzb(zzr zzr, GoogleSignInOptions googleSignInOptions) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzr);
        zzeg.zza(zzax, (Parcelable) googleSignInOptions);
        zzb(102, zzax);
    }

    public final void zzc(zzr zzr, GoogleSignInOptions googleSignInOptions) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzr);
        zzeg.zza(zzax, (Parcelable) googleSignInOptions);
        zzb(103, zzax);
    }
}
