package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.auth.api.credentials.Credential;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

final class zzasj extends zzasn<Status> {
    private /* synthetic */ Credential zzebk;

    zzasj(zzasg zzasg, GoogleApiClient googleApiClient, Credential credential) {
        this.zzebk = credential;
        super(googleApiClient);
    }

    protected final void zza(Context context, zzast zzast) throws RemoteException {
        zzast.zza(new zzasm(this), new zzasv(this.zzebk));
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return status;
    }
}
