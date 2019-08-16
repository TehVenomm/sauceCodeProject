package com.google.firebase.iid;

import android.os.IBinder;
import android.os.Message;
import android.os.Messenger;
import android.os.RemoteException;
import android.util.Log;

final class zzah {
    private final Messenger zzao;
    private final zzm zzci;

    zzah(IBinder iBinder) throws RemoteException {
        String interfaceDescriptor = iBinder.getInterfaceDescriptor();
        if ("android.os.IMessenger".equals(interfaceDescriptor)) {
            this.zzao = new Messenger(iBinder);
            this.zzci = null;
        } else if ("com.google.android.gms.iid.IMessengerCompat".equals(interfaceDescriptor)) {
            this.zzci = new zzm(iBinder);
            this.zzao = null;
        } else {
            String valueOf = String.valueOf(interfaceDescriptor);
            Log.w("MessengerIpcClient", valueOf.length() != 0 ? "Invalid interface descriptor: ".concat(valueOf) : new String("Invalid interface descriptor: "));
            throw new RemoteException();
        }
    }

    /* access modifiers changed from: 0000 */
    public final void send(Message message) throws RemoteException {
        if (this.zzao != null) {
            this.zzao.send(message);
        } else if (this.zzci != null) {
            this.zzci.send(message);
        } else {
            throw new IllegalStateException("Both messengers are null");
        }
    }
}
