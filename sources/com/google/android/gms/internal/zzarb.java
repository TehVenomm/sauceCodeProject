package com.google.android.gms.internal;

import android.accounts.Account;
import android.os.RemoteException;
import com.google.android.gms.auth.account.zzc;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzm;

final class zzarb extends zzm<Result, zzarh> {
    private /* synthetic */ Account zzdxn;

    zzarb(zzaqx zzaqx, Api api, GoogleApiClient googleApiClient, Account account) {
        this.zzdxn = account;
        super(api, googleApiClient);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzc) ((zzarh) zzb).zzajj()).zza(new zzarc(this), this.zzdxn);
    }

    protected final Result zzb(Status status) {
        return new zzarg(status);
    }
}
