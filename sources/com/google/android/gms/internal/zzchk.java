package com.google.android.gms.internal;

import android.util.Log;
import com.google.android.gms.nearby.connection.Payload;
import com.google.android.gms.nearby.connection.PayloadCallback;

final class zzchk extends zzchi<PayloadCallback> {
    private /* synthetic */ zzckf zzjbq;

    zzchk(zzchj zzchj, zzckf zzckf) {
        this.zzjbq = zzckf;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        PayloadCallback payloadCallback = (PayloadCallback) obj;
        Payload zza = zzckt.zza(this.zzjbq.zzbao());
        if (zza == null) {
            Log.w("NearbyConnectionsClient", String.format("Failed to convert incoming ParcelablePayload %d to Payload.", new Object[]{Long.valueOf(this.zzjbq.zzbao().getId())}));
            return;
        }
        payloadCallback.onPayloadReceived(this.zzjbq.zzbaj(), zza);
    }
}
