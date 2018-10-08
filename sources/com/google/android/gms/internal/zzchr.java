package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.nearby.connection.Payload;
import java.util.List;

final class zzchr extends zzcim {
    private /* synthetic */ List zzjbu;
    private /* synthetic */ Payload zzjbv;

    zzchr(zzchp zzchp, GoogleApiClient googleApiClient, List list, Payload payload) {
        this.zzjbu = list;
        this.zzjbv = payload;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzcgp) zzb).zza(this, (String[]) this.zzjbu.toArray(new String[0]), this.zzjbv, false);
    }
}
