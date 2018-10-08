package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.internal.zzm;
import com.google.android.gms.drive.Drive;

public abstract class zzbiu<R extends Result> extends zzm<R, zzbiw> {
    public zzbiu(GoogleApiClient googleApiClient) {
        super(Drive.zzdwq, googleApiClient);
    }

    public /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Result) obj);
    }
}
