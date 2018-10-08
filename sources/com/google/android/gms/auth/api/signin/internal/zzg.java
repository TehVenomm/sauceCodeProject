package com.google.android.gms.auth.api.signin.internal;

import android.os.RemoteException;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.Status;

final class zzg extends zza {
    private /* synthetic */ zzf zzecx;

    zzg(zzf zzf) {
        this.zzecx = zzf;
    }

    public final void zza(GoogleSignInAccount googleSignInAccount, Status status) throws RemoteException {
        if (googleSignInAccount != null) {
            this.zzecx.zzecv.zza(googleSignInAccount, this.zzecx.zzecw);
        }
        this.zzecx.setResult(new GoogleSignInResult(googleSignInAccount, status));
    }
}
