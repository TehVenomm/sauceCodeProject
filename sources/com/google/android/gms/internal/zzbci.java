package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;

final class zzbci extends zzbcc {
    private final zzn<Status> zzfwc;

    public zzbci(zzn<Status> zzn) {
        this.zzfwc = zzn;
    }

    public final void zzcf(int i) throws RemoteException {
        this.zzfwc.setResult(new Status(i));
    }
}
