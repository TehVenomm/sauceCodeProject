package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;

final class zzasm extends zzase {
    private zzn<Status> zzebl;

    zzasm(zzn<Status> zzn) {
        this.zzebl = zzn;
    }

    public final void zze(Status status) {
        this.zzebl.setResult(status);
    }
}
