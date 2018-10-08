package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.nearby.connection.Payload;
import java.util.List;

final class zzcia extends zzcim {
    private /* synthetic */ List zzjbu;
    private /* synthetic */ byte[] zzjce;

    zzcia(zzchp zzchp, GoogleApiClient googleApiClient, List list, byte[] bArr) {
        this.zzjbu = list;
        this.zzjce = bArr;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzcgp) zzb).zza(this, (String[]) this.zzjbu.toArray(new String[0]), Payload.fromBytes(this.zzjce), true);
    }
}
