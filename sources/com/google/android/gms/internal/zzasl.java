package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;

final class zzasl extends zzasn<Status> {
    zzasl(zzasg zzasg, GoogleApiClient googleApiClient) {
        super(googleApiClient);
    }

    protected final void zza(Context context, zzast zzast) throws RemoteException {
        zzast.zza(new zzasm(this));
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return status;
    }
}
