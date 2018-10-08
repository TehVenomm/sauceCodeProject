package com.google.android.gms.internal;

import android.util.Log;
import com.google.android.gms.nearby.connection.Connections.MessageListener;
import com.google.android.gms.nearby.connection.Payload;

final class zzchg extends zzchi<MessageListener> {
    private /* synthetic */ zzckf zzjbq;

    zzchg(zzchf zzchf, zzckf zzckf) {
        this.zzjbq = zzckf;
        super();
    }

    public final /* synthetic */ void zzq(Object obj) {
        MessageListener messageListener = (MessageListener) obj;
        Payload zza = zzckt.zza(this.zzjbq.zzbao());
        if (zza == null) {
            Log.w("NearbyConnectionsClient", String.format("Failed to convert incoming ParcelablePayload %d to Payload.", new Object[]{Long.valueOf(this.zzjbq.zzbao().getId())}));
        } else if (zza.getType() == 1) {
            messageListener.onMessageReceived(this.zzjbq.zzbaj(), zza.asBytes(), this.zzjbq.zzbap());
        }
    }
}
