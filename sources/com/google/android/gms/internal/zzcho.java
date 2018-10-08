package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.nearby.connection.Connections.StartAdvertisingResult;

final class zzcho extends zzcjp {
    private final zzn<StartAdvertisingResult> zzgwg;

    zzcho(zzn<StartAdvertisingResult> zzn) {
        this.zzgwg = (zzn) zzbp.zzu(zzn);
    }

    public final void zza(zzckj zzckj) {
        this.zzgwg.setResult(new zzchn(new Status(zzckj.getStatusCode()), zzckj.getLocalEndpointName()));
    }
}
