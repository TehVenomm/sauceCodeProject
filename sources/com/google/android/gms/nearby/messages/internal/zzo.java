package com.google.android.gms.nearby.messages.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;
import java.util.List;

public final class zzo extends zzee implements zzm {
    zzo(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.messages.internal.IMessageListener");
    }

    public final void zza(zzaf zzaf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzaf);
        zzc(1, zzax);
    }

    public final void zzaf(List<Update> list) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeTypedList(list);
        zzc(4, zzax);
    }

    public final void zzb(zzaf zzaf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzaf);
        zzc(2, zzax);
    }
}
