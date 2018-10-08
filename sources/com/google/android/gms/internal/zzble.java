package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.zza;

public abstract class zzble extends zzef implements zzbld {
    public zzble() {
        attachInterface(this, "com.google.android.gms.drive.internal.IDriveServiceCallbacks");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        switch (i) {
            case 1:
                zza((zzbls) zzeg.zza(parcel, zzbls.CREATOR));
                break;
            case 2:
                zza((zzbma) zzeg.zza(parcel, zzbma.CREATOR));
                break;
            case 3:
                zza((zzblu) zzeg.zza(parcel, zzblu.CREATOR));
                break;
            case 4:
                zza((zzbmf) zzeg.zza(parcel, zzbmf.CREATOR));
                break;
            case 5:
                zza((zzblo) zzeg.zza(parcel, zzblo.CREATOR));
                break;
            case 6:
                onError((Status) zzeg.zza(parcel, Status.CREATOR));
                break;
            case 7:
                onSuccess();
                break;
            case 8:
                zza((zzbmc) zzeg.zza(parcel, zzbmc.CREATOR));
                break;
            case 9:
                zzeg.zza(parcel, zzbmo.CREATOR);
                break;
            case 11:
                zzeg.zza(parcel, zzbme.CREATOR);
                zzboo.zzan(parcel.readStrongBinder());
                break;
            case 12:
                zzeg.zza(parcel, zzbmk.CREATOR);
                break;
            case 13:
                zzeg.zza(parcel, zzbmh.CREATOR);
                break;
            case 14:
                zza((zzblq) zzeg.zza(parcel, zzblq.CREATOR));
                break;
            case 15:
                zzbi(zzeg.zza(parcel));
                break;
            case 16:
                zzeg.zza(parcel, zzbly.CREATOR);
                break;
            case 17:
                zzeg.zza(parcel, zza.CREATOR);
                break;
            case 18:
                zzeg.zza(parcel, zzblm.CREATOR);
                break;
            case 20:
                zzeg.zza(parcel, zzbkz.CREATOR);
                break;
            case 21:
                zzeg.zza(parcel, zzbng.CREATOR);
                break;
            case 22:
                zzeg.zza(parcel, zzbmm.CREATOR);
                break;
            default:
                return false;
        }
        parcel2.writeNoException();
        return true;
    }
}
