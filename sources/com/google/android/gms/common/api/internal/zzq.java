package com.google.android.gms.common.api.internal;

import android.support.annotation.MainThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.GoogleApiActivity;

final class zzq implements Runnable {
    private final zzp zzfiu;
    final /* synthetic */ zzo zzfiv;

    zzq(zzo zzo, zzp zzp) {
        this.zzfiv = zzo;
        this.zzfiu = zzp;
    }

    @MainThread
    public final void run() {
        if (this.zzfiv.mStarted) {
            ConnectionResult zzagc = this.zzfiu.zzagc();
            if (zzagc.hasResolution()) {
                this.zzfiv.zzfoi.startActivityForResult(GoogleApiActivity.zza(this.zzfiv.getActivity(), zzagc.getResolution(), this.zzfiu.zzagb(), false), 1);
            } else if (this.zzfiv.zzfhf.isUserResolvableError(zzagc.getErrorCode())) {
                this.zzfiv.zzfhf.zza(this.zzfiv.getActivity(), this.zzfiv.zzfoi, zzagc.getErrorCode(), 2, this.zzfiv);
            } else if (zzagc.getErrorCode() == 18) {
                GoogleApiAvailability.zza(this.zzfiv.getActivity().getApplicationContext(), new zzr(this, GoogleApiAvailability.zza(this.zzfiv.getActivity(), this.zzfiv)));
            } else {
                this.zzfiv.zza(zzagc, this.zzfiu.zzagb());
            }
        }
    }
}
