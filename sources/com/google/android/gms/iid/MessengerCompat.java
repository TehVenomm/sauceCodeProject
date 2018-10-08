package com.google.android.gms.iid;

import android.os.Build.VERSION;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Message;
import android.os.Messenger;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.os.RemoteException;
import com.google.android.gms.common.internal.ReflectedParcelable;

public class MessengerCompat implements ReflectedParcelable {
    public static final Creator<MessengerCompat> CREATOR = new zzd();
    private Messenger zzhtm;
    private zzb zzhtn;

    public MessengerCompat(IBinder iBinder) {
        if (VERSION.SDK_INT >= 21) {
            this.zzhtm = new Messenger(iBinder);
            return;
        }
        zzb zzb;
        if (iBinder == null) {
            zzb = null;
        } else {
            IInterface queryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.iid.IMessengerCompat");
            zzb = queryLocalInterface instanceof zzb ? (zzb) queryLocalInterface : new zzc(iBinder);
        }
        this.zzhtn = zzb;
    }

    private final IBinder getBinder() {
        return this.zzhtm != null ? this.zzhtm.getBinder() : this.zzhtn.asBinder();
    }

    public int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        boolean z = false;
        if (obj != null) {
            try {
                z = getBinder().equals(((MessengerCompat) obj).getBinder());
            } catch (ClassCastException e) {
            }
        }
        return z;
    }

    public int hashCode() {
        return getBinder().hashCode();
    }

    public final void send(Message message) throws RemoteException {
        if (this.zzhtm != null) {
            this.zzhtm.send(message);
        } else {
            this.zzhtn.send(message);
        }
    }

    public void writeToParcel(Parcel parcel, int i) {
        if (this.zzhtm != null) {
            parcel.writeStrongBinder(this.zzhtm.getBinder());
        } else {
            parcel.writeStrongBinder(this.zzhtn.asBinder());
        }
    }
}
