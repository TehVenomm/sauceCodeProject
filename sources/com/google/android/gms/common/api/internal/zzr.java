package com.google.android.gms.common.api.internal;

import android.app.Dialog;

final class zzr extends zzbz {
    private /* synthetic */ Dialog zzfiw;
    private /* synthetic */ zzq zzfix;

    zzr(zzq zzq, Dialog dialog) {
        this.zzfix = zzq;
        this.zzfiw = dialog;
    }

    public final void zzagd() {
        this.zzfix.zzfiv.zzaga();
        if (this.zzfiw.isShowing()) {
            this.zzfiw.dismiss();
        }
    }
}
