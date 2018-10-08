package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;

public final class zzt extends zzee implements zzs {
    zzt(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.messages.internal.INearbyMessagesService");
    }

    public final void zza(SubscribeRequest subscribeRequest) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) subscribeRequest);
        zzc(3, zzax);
    }

    public final void zza(zzax zzax) throws RemoteException {
        Parcel zzax2 = zzax();
        zzeg.zza(zzax2, (Parcelable) zzax);
        zzc(1, zzax2);
    }

    public final void zza(zzaz zzaz) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzaz);
        zzc(8, zzax);
    }

    public final void zza(zzbc zzbc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbc);
        zzc(2, zzax);
    }

    public final void zza(zzbe zzbe) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbe);
        zzc(4, zzax);
    }

    public final void zza(zzh zzh) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzh);
        zzc(7, zzax);
    }

    public final void zza(zzj zzj) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzj);
        zzc(9, zzax);
    }
}
