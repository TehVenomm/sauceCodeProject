package com.google.android.gms.auth.api.accounttransfer;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.internal.zzarn;

final class zzk extends zzarn {
    private /* synthetic */ zzc zzdzf;

    zzk(zzc zzc) {
        this.zzdzf = zzc;
    }

    public final void onFailure(Status status) {
        this.zzdzf.zzd(status);
    }

    public final void zzzw() {
        this.zzdzf.setResult(null);
    }
}
