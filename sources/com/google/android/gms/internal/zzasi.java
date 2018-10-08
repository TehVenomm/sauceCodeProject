package com.google.android.gms.internal;

import com.google.android.gms.auth.api.credentials.Credential;
import com.google.android.gms.common.api.Status;

final class zzasi extends zzase {
    private /* synthetic */ zzash zzebj;

    zzasi(zzash zzash) {
        this.zzebj = zzash;
    }

    public final void zza(Status status, Credential credential) {
        this.zzebj.setResult(new zzasf(status, credential));
    }

    public final void zze(Status status) {
        this.zzebj.setResult(zzasf.zzf(status));
    }
}
