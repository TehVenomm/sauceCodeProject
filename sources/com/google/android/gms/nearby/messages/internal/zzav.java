package com.google.android.gms.nearby.messages.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzcj;
import com.google.android.gms.common.api.internal.zzm;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.nearby.Nearby;

abstract class zzav extends zzm<Status, zzah> {
    private final zzcj<zzn<Status>> zzjgj;

    public zzav(GoogleApiClient googleApiClient) {
        super(Nearby.MESSAGES_API, googleApiClient);
        this.zzjgj = googleApiClient.zzp(this);
    }

    public final /* bridge */ /* synthetic */ void setResult(Object obj) {
        super.setResult((Status) obj);
    }

    public final /* synthetic */ Result zzb(Status status) {
        return status;
    }

    final zzcj<zzn<Status>> zzbbb() {
        return this.zzjgj;
    }
}
