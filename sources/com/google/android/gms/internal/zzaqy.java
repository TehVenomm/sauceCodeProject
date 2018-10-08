package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.auth.account.zzc;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzm;

final class zzaqy extends zzm<Result, zzarh> {
    private /* synthetic */ boolean val$enabled;

    zzaqy(zzaqx zzaqx, Api api, GoogleApiClient googleApiClient, boolean z) {
        this.val$enabled = z;
        super(api, googleApiClient);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzc) ((zzarh) zzb).zzajj()).zzap(this.val$enabled);
        setResult(new zzarf(Status.zzfhp));
    }

    protected final Result zzb(Status status) {
        return new zzarf(status);
    }
}
