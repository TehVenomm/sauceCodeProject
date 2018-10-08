package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.zzp;

final class zzbje extends zzbiv {
    private /* synthetic */ zzbjc zzghm;
    private /* synthetic */ MetadataChangeSet zzghn;
    private /* synthetic */ zzp zzgho;

    zzbje(zzbjc zzbjc, GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, zzp zzp) {
        this.zzghm = zzbjc;
        this.zzghn = metadataChangeSet;
        this.zzgho = zzp;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzbiw zzbiw = (zzbiw) zzb;
        this.zzghn.zzana().setContext(zzbiw.getContext());
        ((zzblb) zzbiw.zzajj()).zza(new zzbhl(this.zzghm.zzghj.getDriveId(), this.zzghn.zzana(), this.zzghm.zzghj.getRequestId(), this.zzghm.zzghj.zzamp(), this.zzgho), new zzbnf(this));
    }
}
