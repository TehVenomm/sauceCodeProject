package com.google.android.gms.nearby.messages.internal;

import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.internal.nearby.zzb;
import com.google.android.gms.internal.nearby.zzc;
import java.util.List;

public abstract class zzn extends zzb implements zzm {
    public zzn() {
        super("com.google.android.gms.nearby.messages.internal.IMessageListener");
    }

    /* access modifiers changed from: protected */
    public final boolean dispatchTransaction(int i, Parcel parcel, Parcel parcel2, int i2) throws RemoteException {
        switch (i) {
            case 1:
                zza((zzaf) zzc.zza(parcel, zzaf.CREATOR));
                break;
            case 2:
                zzb((zzaf) zzc.zza(parcel, zzaf.CREATOR));
                break;
            case 4:
                zza((List<Update>) parcel.createTypedArrayList(Update.CREATOR));
                break;
            default:
                return false;
        }
        return true;
    }
}
