package com.google.android.gms.common.api.internal;

import android.support.annotation.NonNull;
import com.google.android.gms.auth.api.signin.internal.zzy;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.ResultCallback;
import com.google.android.gms.common.api.Status;

final class zzbh implements ResultCallback<Status> {
    private /* synthetic */ GoogleApiClient zzemq;
    private /* synthetic */ zzbd zzfmp;
    private /* synthetic */ zzda zzfmr;
    private /* synthetic */ boolean zzfms;

    zzbh(zzbd zzbd, zzda zzda, boolean z, GoogleApiClient googleApiClient) {
        this.zzfmp = zzbd;
        this.zzfmr = zzda;
        this.zzfms = z;
        this.zzemq = googleApiClient;
    }

    public final /* synthetic */ void onResult(@NonNull Result result) {
        Status status = (Status) result;
        zzy.zzbm(this.zzfmp.mContext).zzaat();
        if (status.isSuccess() && this.zzfmp.isConnected()) {
            this.zzfmp.reconnect();
        }
        this.zzfmr.setResult(status);
        if (this.zzfms) {
            this.zzemq.disconnect();
        }
    }
}
