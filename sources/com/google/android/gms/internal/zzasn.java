package com.google.android.gms.internal;

import android.content.Context;
import android.os.DeadObjectException;
import android.os.RemoteException;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.internal.zzm;

abstract class zzasn<R extends Result> extends zzm<R, zzaso> {
    zzasn(GoogleApiClient googleApiClient) {
        super(Auth.CREDENTIALS_API, googleApiClient);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }

    protected abstract void zza(Context context, zzast zzast) throws DeadObjectException, RemoteException;

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzaso zzaso = (zzaso) zzb;
        zza(zzaso.getContext(), (zzast) zzaso.zzajj());
    }
}
