package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.nearby.messages.MessageListener;
import com.google.android.gms.nearby.messages.internal.Update;
import com.google.android.gms.nearby.messages.internal.zzaf;
import com.google.android.gms.nearby.messages.internal.zzn;
import java.util.List;

public final class zzclr extends zzn implements zzclq<MessageListener> {
    private final zzcj<MessageListener> zzjgz;

    zzclr(zzcj<MessageListener> zzcj) {
        this.zzjgz = zzcj;
    }

    public final void zza(zzaf zzaf) {
    }

    public final void zzaf(List<Update> list) throws RemoteException {
        this.zzjgz.zza(new zzcls(this, list));
    }

    public final void zzb(zzaf zzaf) {
    }

    public final zzcj<MessageListener> zzbbb() {
        return this.zzjgz;
    }
}
