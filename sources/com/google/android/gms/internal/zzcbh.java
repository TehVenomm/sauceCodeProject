package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.RemoteException;
import java.util.List;

public abstract class zzcbh extends zzef implements zzcbg {
    public zzcbh() {
        attachInterface(this, "com.google.android.gms.measurement.internal.IMeasurementService");
    }

    public boolean onTransact(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        if (zza(i, parcel, parcel2, i2)) {
            return true;
        }
        List zza;
        switch (i) {
            case 1:
                zza((zzcbc) zzeg.zza(parcel, zzcbc.CREATOR), (zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                break;
            case 2:
                zza((zzcfl) zzeg.zza(parcel, zzcfl.CREATOR), (zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                break;
            case 4:
                zza((zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                break;
            case 5:
                zza((zzcbc) zzeg.zza(parcel, zzcbc.CREATOR), parcel.readString(), parcel.readString());
                parcel2.writeNoException();
                break;
            case 6:
                zzb((zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                break;
            case 7:
                zza = zza((zzcak) zzeg.zza(parcel, zzcak.CREATOR), zzeg.zza(parcel));
                parcel2.writeNoException();
                parcel2.writeTypedList(zza);
                break;
            case 9:
                byte[] zza2 = zza((zzcbc) zzeg.zza(parcel, zzcbc.CREATOR), parcel.readString());
                parcel2.writeNoException();
                parcel2.writeByteArray(zza2);
                break;
            case 10:
                zza(parcel.readLong(), parcel.readString(), parcel.readString(), parcel.readString());
                parcel2.writeNoException();
                break;
            case 11:
                String zzc = zzc((zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                parcel2.writeString(zzc);
                break;
            case 12:
                zza((zzcan) zzeg.zza(parcel, zzcan.CREATOR), (zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                break;
            case 13:
                zzb((zzcan) zzeg.zza(parcel, zzcan.CREATOR));
                parcel2.writeNoException();
                break;
            case 14:
                zza = zza(parcel.readString(), parcel.readString(), zzeg.zza(parcel), (zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                parcel2.writeTypedList(zza);
                break;
            case 15:
                zza = zza(parcel.readString(), parcel.readString(), parcel.readString(), zzeg.zza(parcel));
                parcel2.writeNoException();
                parcel2.writeTypedList(zza);
                break;
            case 16:
                zza = zza(parcel.readString(), parcel.readString(), (zzcak) zzeg.zza(parcel, zzcak.CREATOR));
                parcel2.writeNoException();
                parcel2.writeTypedList(zza);
                break;
            case 17:
                zza = zzj(parcel.readString(), parcel.readString(), parcel.readString());
                parcel2.writeNoException();
                parcel2.writeTypedList(zza);
                break;
            default:
                return false;
        }
        return true;
    }
}
