package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.nearby.connection.Connections.StartAdvertisingResult;

final class zzchn implements StartAdvertisingResult {
    private final Status zzdvk;
    private final String zzjbt;

    zzchn(Status status, String str) {
        this.zzdvk = status;
        this.zzjbt = str;
    }

    public final String getLocalEndpointName() {
        return this.zzjbt;
    }

    public final Status getStatus() {
        return this.zzdvk;
    }
}
