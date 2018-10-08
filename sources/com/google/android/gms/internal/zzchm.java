package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.common.internal.zzbp;

final class zzchm extends zzcjm {
    private final zzn<Status> zzgwg;

    zzchm(zzn<Status> zzn) {
        this.zzgwg = (zzn) zzbp.zzu(zzn);
    }

    public final void zzdw(int i) {
        this.zzgwg.setResult(new Status(i));
    }
}
