package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.nearby.connection.Connections.StartAdvertisingResult;

final class zzcil implements StartAdvertisingResult {
    private /* synthetic */ Status zzeik;

    zzcil(zzcik zzcik, Status status) {
        this.zzeik = status;
    }

    public final String getLocalEndpointName() {
        return null;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
