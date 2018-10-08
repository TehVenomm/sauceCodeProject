package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.MetadataChangeSet;

final class zzbjo extends zzbju {
    private /* synthetic */ MetadataChangeSet zzghu;
    private /* synthetic */ zzbjm zzghy;

    zzbjo(zzbjm zzbjm, GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet) {
        this.zzghy = zzbjm;
        this.zzghu = metadataChangeSet;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzbiw zzbiw = (zzbiw) zzb;
        this.zzghu.zzana().setContext(zzbiw.getContext());
        ((zzblb) zzbiw.zzajj()).zza(new zzbhw(this.zzghy.getDriveId(), this.zzghu.zzana()), new zzbjq(this));
    }
}
