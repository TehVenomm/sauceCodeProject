package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.auth.api.credentials.CredentialRequest;
import com.google.android.gms.auth.api.credentials.CredentialRequestResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

final class zzash extends zzasn<CredentialRequestResult> {
    private /* synthetic */ CredentialRequest zzebi;

    zzash(zzasg zzasg, GoogleApiClient googleApiClient, CredentialRequest credentialRequest) {
        this.zzebi = credentialRequest;
        super(googleApiClient);
    }

    protected final void zza(Context context, zzast zzast) throws RemoteException {
        zzast.zza(new zzasi(this), this.zzebi);
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return zzasf.zzf(status);
    }
}
