package com.google.firebase.iid;

import com.google.firebase.events.Event;
import com.google.firebase.events.EventHandler;

final /* synthetic */ class zzq implements EventHandler {
    private final zza zzbm;

    zzq(zza zza) {
        this.zzbm = zza;
    }

    public final void handle(Event event) {
        zza zza = this.zzbm;
        synchronized (zza) {
            if (zza.isEnabled()) {
                FirebaseInstanceId.this.zzh();
            }
        }
    }
}
