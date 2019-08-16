package com.google.firebase.iid;

import android.os.Build.VERSION;
import android.os.IBinder;
import android.os.Message;
import android.os.Messenger;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.os.RemoteException;
import android.util.Log;

public class zzm implements Parcelable {
    public static final Creator<zzm> CREATOR = new zzl();
    private Messenger zzao;
    private zzw zzap;

    public static final class zza extends ClassLoader {
        /* access modifiers changed from: protected */
        public final Class<?> loadClass(String str, boolean z) throws ClassNotFoundException {
            if (!"com.google.android.gms.iid.MessengerCompat".equals(str)) {
                return super.loadClass(str, z);
            }
            if (FirebaseInstanceId.zzm()) {
                Log.d("FirebaseInstanceId", "Using renamed FirebaseIidMessengerCompat class");
            }
            return zzm.class;
        }
    }

    public zzm(IBinder iBinder) {
        if (VERSION.SDK_INT >= 21) {
            this.zzao = new Messenger(iBinder);
        } else {
            this.zzap = new zzv(iBinder);
        }
    }

    private final IBinder getBinder() {
        return this.zzao != null ? this.zzao.getBinder() : this.zzap.asBinder();
    }

    public int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        boolean z = false;
        if (obj == null) {
            return z;
        }
        try {
            return getBinder().equals(((zzm) obj).getBinder());
        } catch (ClassCastException e) {
            return z;
        }
    }

    public int hashCode() {
        return getBinder().hashCode();
    }

    public final void send(Message message) throws RemoteException {
        if (this.zzao != null) {
            this.zzao.send(message);
        } else {
            this.zzap.send(message);
        }
    }

    public void writeToParcel(Parcel parcel, int i) {
        if (this.zzao != null) {
            parcel.writeStrongBinder(this.zzao.getBinder());
        } else {
            parcel.writeStrongBinder(this.zzap.asBinder());
        }
    }
}
