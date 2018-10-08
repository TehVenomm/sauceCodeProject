package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.MetadataChangeSet;

final class zzbkg extends zzbkn {
    private /* synthetic */ MetadataChangeSet zzghu;
    private /* synthetic */ zzbkc zzgig;

    zzbkg(zzbkc zzbkc, GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet) {
        this.zzgig = zzbkc;
        this.zzghu = metadataChangeSet;
        super(zzbkc, googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzbiw zzbiw = (zzbiw) zzb;
        this.zzghu.zzana().setContext(zzbiw.getContext());
        ((zzblb) zzbiw.zzajj()).zza(new zzbnm(this.zzgig.zzgcx, this.zzghu.zzana()), new zzbkl(this));
    }
}
