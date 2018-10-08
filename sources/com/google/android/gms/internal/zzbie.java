package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.query.Query;

final class zzbie extends zzbir {
    private /* synthetic */ Query zzggn;

    zzbie(zzbid zzbid, GoogleApiClient googleApiClient, Query query) {
        this.zzggn = query;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzblb) ((zzbiw) zzb).zzajj()).zza(new zzbmx(this.zzggn), new zzbis(this));
    }
}
