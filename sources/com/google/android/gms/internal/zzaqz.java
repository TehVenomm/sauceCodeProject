package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.auth.account.WorkAccountApi.AddAccountResult;
import com.google.android.gms.auth.account.zzc;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzm;

final class zzaqz extends zzm<AddAccountResult, zzarh> {
    private /* synthetic */ String zzdxp;

    zzaqz(zzaqx zzaqx, Api api, GoogleApiClient googleApiClient, String str) {
        this.zzdxp = str;
        super(api, googleApiClient);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((AddAccountResult) obj);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((zzc) ((zzarh) zzb).zzajj()).zza(new zzara(this), this.zzdxp);
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return new zzare(status, null);
    }
}
