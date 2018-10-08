package com.google.android.gms.internal;

import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.nearby.messages.internal.zzq;

public final class zzclt extends zzq implements zzclq<zzn<Status>> {
    private final zzcj<zzn<Status>> zzjgz;
    private boolean zzjhb = false;

    public zzclt(zzcj<zzn<Status>> zzcj) {
        this.zzjgz = zzcj;
    }

    public final void zzag(Status status) throws RemoteException {
        synchronized (this) {
            if (this.zzjhb) {
                String valueOf = String.valueOf(status);
                Log.wtf("NearbyMessagesCallbackWrapper", new StringBuilder(String.valueOf(valueOf).length() + 28).append("Received multiple statuses: ").append(valueOf).toString(), new Exception());
            } else {
                this.zzjgz.zza(new zzclu(this, status));
                this.zzjhb = true;
            }
        }
    }

    public final zzcj<zzn<Status>> zzbbb() {
        return this.zzjgz;
    }
}
