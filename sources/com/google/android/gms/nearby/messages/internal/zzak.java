package com.google.android.gms.nearby.messages.internal;

import android.app.PendingIntent;
import android.content.Intent;
import android.os.Bundle;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzclm;
import com.google.android.gms.nearby.messages.Message;
import com.google.android.gms.nearby.messages.MessageListener;
import com.google.android.gms.nearby.messages.Messages;
import com.google.android.gms.nearby.messages.MessagesOptions;
import com.google.android.gms.nearby.messages.PublishOptions;
import com.google.android.gms.nearby.messages.StatusCallback;
import com.google.android.gms.nearby.messages.SubscribeOptions;
import java.util.Collections;

public final class zzak implements Messages {
    public static final zzf<zzah> zzdwq = new zzf();
    public static final zza<zzah, MessagesOptions> zzdwr = new zzal();
    public static final zzak zzjgd = new zzak();

    private zzak() {
    }

    public static void zza(Iterable<Update> iterable, MessageListener messageListener) {
        for (Update update : iterable) {
            if (update.zzdz(1)) {
                messageListener.onFound(update.zzjfy);
            }
            if (update.zzdz(2)) {
                messageListener.onLost(update.zzjfy);
            }
            if (update.zzdz(4)) {
                messageListener.onDistanceChanged(update.zzjfy, update.zzjgw);
            }
            if (update.zzdz(8)) {
                messageListener.onBleSignalChanged(update.zzjfy, update.zzjgx);
            }
            if (update.zzdz(16)) {
                Message message = update.zzjfy;
                zzclm zzclm = update.zzjgy;
            }
        }
    }

    private static zzah zzh(GoogleApiClient googleApiClient) {
        return (zzah) googleApiClient.zza(zzdwq);
    }

    public final PendingResult<Status> getPermissionStatus(GoogleApiClient googleApiClient) {
        return googleApiClient.zze(new zzat(this, googleApiClient));
    }

    public final void handleIntent(Intent intent, MessageListener messageListener) {
        Bundle bundleExtra = intent.getBundleExtra("com.google.android.gms.nearby.messages.UPDATES");
        zza(bundleExtra == null ? Collections.emptyList() : bundleExtra.getParcelableArrayList("com.google.android.gms.nearby.messages.UPDATES"), messageListener);
    }

    public final PendingResult<Status> publish(GoogleApiClient googleApiClient, Message message) {
        return publish(googleApiClient, message, PublishOptions.DEFAULT);
    }

    public final PendingResult<Status> publish(GoogleApiClient googleApiClient, Message message, PublishOptions publishOptions) {
        zzbp.zzu(message);
        zzbp.zzu(publishOptions);
        return googleApiClient.zze(new zzan(this, googleApiClient, message, publishOptions.getCallback() == null ? null : googleApiClient.zzp(publishOptions.getCallback()), publishOptions));
    }

    public final PendingResult<Status> registerStatusCallback(GoogleApiClient googleApiClient, StatusCallback statusCallback) {
        zzbp.zzu(statusCallback);
        return googleApiClient.zze(new zzau(this, googleApiClient, zzh(googleApiClient).zza(googleApiClient, statusCallback)));
    }

    public final PendingResult<Status> subscribe(GoogleApiClient googleApiClient, PendingIntent pendingIntent) {
        return subscribe(googleApiClient, pendingIntent, SubscribeOptions.DEFAULT);
    }

    public final PendingResult<Status> subscribe(GoogleApiClient googleApiClient, PendingIntent pendingIntent, SubscribeOptions subscribeOptions) {
        zzbp.zzu(pendingIntent);
        zzbp.zzu(subscribeOptions);
        return googleApiClient.zze(new zzaq(this, googleApiClient, pendingIntent, subscribeOptions.getCallback() == null ? null : googleApiClient.zzp(subscribeOptions.getCallback()), subscribeOptions));
    }

    public final PendingResult<Status> subscribe(GoogleApiClient googleApiClient, MessageListener messageListener) {
        return subscribe(googleApiClient, messageListener, SubscribeOptions.DEFAULT);
    }

    public final PendingResult<Status> subscribe(GoogleApiClient googleApiClient, MessageListener messageListener, SubscribeOptions subscribeOptions) {
        zzbp.zzu(messageListener);
        zzbp.zzu(subscribeOptions);
        zzbp.zzb(subscribeOptions.getStrategy().zzbay() == 0, (Object) "Strategy.setBackgroundScanMode() is only supported by background subscribe (the version which takes a PendingIntent).");
        return googleApiClient.zze(new zzap(this, googleApiClient, zzh(googleApiClient).zza(googleApiClient, messageListener), subscribeOptions.getCallback() == null ? null : googleApiClient.zzp(subscribeOptions.getCallback()), subscribeOptions));
    }

    public final PendingResult<Status> unpublish(GoogleApiClient googleApiClient, Message message) {
        zzbp.zzu(message);
        return googleApiClient.zze(new zzao(this, googleApiClient, message));
    }

    public final PendingResult<Status> unregisterStatusCallback(GoogleApiClient googleApiClient, StatusCallback statusCallback) {
        zzbp.zzu(statusCallback);
        return googleApiClient.zze(new zzam(this, googleApiClient, zzh(googleApiClient).zza(statusCallback)));
    }

    public final PendingResult<Status> unsubscribe(GoogleApiClient googleApiClient, PendingIntent pendingIntent) {
        zzbp.zzu(pendingIntent);
        return googleApiClient.zze(new zzas(this, googleApiClient, pendingIntent));
    }

    public final PendingResult<Status> unsubscribe(GoogleApiClient googleApiClient, MessageListener messageListener) {
        zzbp.zzu(messageListener);
        return googleApiClient.zze(new zzar(this, googleApiClient, zzh(googleApiClient).zza(messageListener)));
    }
}
