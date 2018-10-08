package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.auth.api.credentials.CredentialRequest;

public final class zzasu extends zzee implements zzast {
    zzasu(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.auth.api.credentials.internal.ICredentialsService");
    }

    public final void zza(zzasr zzasr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzasr);
        zzb(4, zzax);
    }

    public final void zza(zzasr zzasr, CredentialRequest credentialRequest) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzasr);
        zzeg.zza(zzax, (Parcelable) credentialRequest);
        zzb(1, zzax);
    }

    public final void zza(zzasr zzasr, zzasp zzasp) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzasr);
        zzeg.zza(zzax, (Parcelable) zzasp);
        zzb(3, zzax);
    }

    public final void zza(zzasr zzasr, zzasv zzasv) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzasr);
        zzeg.zza(zzax, (Parcelable) zzasv);
        zzb(2, zzax);
    }
}
