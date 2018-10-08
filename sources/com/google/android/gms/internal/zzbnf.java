package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;

public final class zzbnf extends zzbhi {
    private final zzn<Status> zzfwc;

    public zzbnf(zzn<Status> zzn) {
        this.zzfwc = zzn;
    }

    public final void onError(Status status) throws RemoteException {
        this.zzfwc.setResult(status);
    }

    public final void onSuccess() throws RemoteException {
        this.zzfwc.setResult(Status.zzfhp);
    }
}
