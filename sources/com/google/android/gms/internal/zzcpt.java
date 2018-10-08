package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Status;

public abstract class zzcpt extends zzef implements zzcps {
    public zzcpt() {
        attachInterface(this, "com.google.android.gms.signin.internal.ISignInCallbacks");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 3:
                zzeg.zza(parcel, ConnectionResult.CREATOR);
                zzeg.zza(parcel, zzcpp.CREATOR);
                break;
            case 4:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 6:
                zzeg.zza(parcel, Status.CREATOR);
                break;
            case 7:
                zzeg.zza(parcel, Status.CREATOR);
                zzeg.zza(parcel, GoogleSignInAccount.CREATOR);
                break;
            case 8:
                zzb((zzcpz) zzeg.zza(parcel, zzcpz.CREATOR));
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
