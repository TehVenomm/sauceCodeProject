package com.google.android.gms.auth.api.signin.internal;

import android.os.RemoteException;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

final class zzf extends zzl<GoogleSignInResult> {
    final /* synthetic */ zzy zzecv;
    final /* synthetic */ GoogleSignInOptions zzecw;

    zzf(GoogleApiClient googleApiClient, zzy zzy, GoogleSignInOptions googleSignInOptions) {
        this.zzecv = zzy;
        this.zzecw = googleSignInOptions;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzt) ((zzd) zzb).zzajj()).zza(new zzg(this), this.zzecw);
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return new GoogleSignInResult(null, status);
    }
}
