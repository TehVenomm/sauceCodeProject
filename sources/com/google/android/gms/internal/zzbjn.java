package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.drive.MetadataChangeSet;
import com.google.android.gms.drive.zzm;

final class zzbjn extends zzbjs {
    private /* synthetic */ MetadataChangeSet zzghu;
    private /* synthetic */ int zzghv;
    private /* synthetic */ int zzghw;
    private /* synthetic */ zzm zzghx;
    private /* synthetic */ zzbjm zzghy;

    zzbjn(zzbjm zzbjm, GoogleApiClient googleApiClient, MetadataChangeSet metadataChangeSet, int i, int i2, zzm zzm) {
        this.zzghy = zzbjm;
        this.zzghu = metadataChangeSet;
        this.zzghv = i;
        this.zzghw = i2;
        this.zzghx = zzm;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        zzbiw zzbiw = (zzbiw) zzb;
        this.zzghu.zzana().setContext(zzbiw.getContext());
        ((zzblb) zzbiw.zzajj()).zza(new zzbhu(this.zzghy.getDriveId(), this.zzghu.zzana(), this.zzghv, this.zzghw, this.zzghx), new zzbjp(this));
    }
}
