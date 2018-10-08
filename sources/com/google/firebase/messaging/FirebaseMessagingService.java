package com.google.firebase.messaging;

import android.app.PendingIntent;
import android.app.PendingIntent.CanceledException;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.firebase.iid.zzb;
import com.google.firebase.iid.zzq;
import java.util.Iterator;

public class FirebaseMessagingService extends zzb {
    static boolean zzaf(Bundle bundle) {
        return bundle == null ? false : AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(bundle.getString("google.c.a.e"));
    }

    static void zzp(Bundle bundle) {
        Iterator it = bundle.keySet().iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            if (str != null && str.startsWith("google.c.")) {
                it.remove();
            }
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void handleIntent(android.content.Intent r6) {
        /*
        r5 = this;
        r3 = 1;
        r2 = 0;
        r1 = -1;
        r0 = r6.getAction();
        if (r0 != 0) goto L_0x000b;
    L_0x0009:
        r0 = "";
    L_0x000b:
        r4 = r0.hashCode();
        switch(r4) {
            case 75300319: goto L_0x003a;
            case 366519424: goto L_0x0030;
            default: goto L_0x0012;
        };
    L_0x0012:
        r0 = r1;
    L_0x0013:
        switch(r0) {
            case 0: goto L_0x0044;
            case 1: goto L_0x010d;
            default: goto L_0x0016;
        };
    L_0x0016:
        r0 = r6.getAction();
        r0 = java.lang.String.valueOf(r0);
        r1 = r0.length();
        if (r1 == 0) goto L_0x011c;
    L_0x0024:
        r1 = "Unknown intent action: ";
        r0 = r1.concat(r0);
    L_0x002a:
        r1 = "FirebaseMessaging";
        android.util.Log.d(r1, r0);
    L_0x002f:
        return;
    L_0x0030:
        r4 = "com.google.android.c2dm.intent.RECEIVE";
        r0 = r0.equals(r4);
        if (r0 == 0) goto L_0x0012;
    L_0x0038:
        r0 = r2;
        goto L_0x0013;
    L_0x003a:
        r4 = "com.google.firebase.messaging.NOTIFICATION_DISMISS";
        r0 = r0.equals(r4);
        if (r0 == 0) goto L_0x0012;
    L_0x0042:
        r0 = r3;
        goto L_0x0013;
    L_0x0044:
        r0 = "message_type";
        r0 = r6.getStringExtra(r0);
        if (r0 != 0) goto L_0x004e;
    L_0x004c:
        r0 = "gcm";
    L_0x004e:
        r4 = r0.hashCode();
        switch(r4) {
            case -2062414158: goto L_0x0078;
            case 102161: goto L_0x006e;
            case 814694033: goto L_0x008c;
            case 814800675: goto L_0x0082;
            default: goto L_0x0055;
        };
    L_0x0055:
        switch(r1) {
            case 0: goto L_0x0096;
            case 1: goto L_0x00d6;
            case 2: goto L_0x00db;
            case 3: goto L_0x00e6;
            default: goto L_0x0058;
        };
    L_0x0058:
        r0 = java.lang.String.valueOf(r0);
        r1 = r0.length();
        if (r1 == 0) goto L_0x0104;
    L_0x0062:
        r1 = "Received message with unknown type: ";
        r0 = r1.concat(r0);
    L_0x0068:
        r1 = "FirebaseMessaging";
        android.util.Log.w(r1, r0);
        goto L_0x002f;
    L_0x006e:
        r3 = "gcm";
        r3 = r0.equals(r3);
        if (r3 == 0) goto L_0x0055;
    L_0x0076:
        r1 = r2;
        goto L_0x0055;
    L_0x0078:
        r2 = "deleted_messages";
        r2 = r0.equals(r2);
        if (r2 == 0) goto L_0x0055;
    L_0x0080:
        r1 = r3;
        goto L_0x0055;
    L_0x0082:
        r2 = "send_event";
        r2 = r0.equals(r2);
        if (r2 == 0) goto L_0x0055;
    L_0x008a:
        r1 = 2;
        goto L_0x0055;
    L_0x008c:
        r2 = "send_error";
        r2 = r0.equals(r2);
        if (r2 == 0) goto L_0x0055;
    L_0x0094:
        r1 = 3;
        goto L_0x0055;
    L_0x0096:
        r0 = r6.getExtras();
        r0 = zzaf(r0);
        if (r0 == 0) goto L_0x00a3;
    L_0x00a0:
        com.google.firebase.messaging.zzd.zzg(r5, r6);
    L_0x00a3:
        r0 = r6.getExtras();
        if (r0 != 0) goto L_0x00ae;
    L_0x00a9:
        r0 = new android.os.Bundle;
        r0.<init>();
    L_0x00ae:
        r1 = "android.support.content.wakelockid";
        r0.remove(r1);
        r1 = com.google.firebase.messaging.zza.zzac(r0);
        if (r1 == 0) goto L_0x00cc;
    L_0x00b9:
        r1 = com.google.firebase.messaging.zza.zzep(r5);
        r1 = r1.zzr(r0);
        if (r1 != 0) goto L_0x002f;
    L_0x00c3:
        r1 = zzaf(r0);
        if (r1 == 0) goto L_0x00cc;
    L_0x00c9:
        com.google.firebase.messaging.zzd.zzj(r5, r6);
    L_0x00cc:
        r1 = new com.google.firebase.messaging.RemoteMessage;
        r1.<init>(r0);
        r5.onMessageReceived(r1);
        goto L_0x002f;
    L_0x00d6:
        r5.onDeletedMessages();
        goto L_0x002f;
    L_0x00db:
        r0 = "google.message_id";
        r0 = r6.getStringExtra(r0);
        r5.onMessageSent(r0);
        goto L_0x002f;
    L_0x00e6:
        r0 = "google.message_id";
        r0 = r6.getStringExtra(r0);
        if (r0 != 0) goto L_0x00f4;
    L_0x00ee:
        r0 = "message_id";
        r0 = r6.getStringExtra(r0);
    L_0x00f4:
        r1 = new com.google.firebase.messaging.SendException;
        r2 = "error";
        r2 = r6.getStringExtra(r2);
        r1.<init>(r2);
        r5.onSendError(r0, r1);
        goto L_0x002f;
    L_0x0104:
        r0 = new java.lang.String;
        r1 = "Received message with unknown type: ";
        r0.<init>(r1);
        goto L_0x0068;
    L_0x010d:
        r0 = r6.getExtras();
        r0 = zzaf(r0);
        if (r0 == 0) goto L_0x002f;
    L_0x0117:
        com.google.firebase.messaging.zzd.zzi(r5, r6);
        goto L_0x002f;
    L_0x011c:
        r0 = new java.lang.String;
        r1 = "Unknown intent action: ";
        r0.<init>(r1);
        goto L_0x002a;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.messaging.FirebaseMessagingService.handleIntent(android.content.Intent):void");
    }

    @WorkerThread
    public void onDeletedMessages() {
    }

    @WorkerThread
    public void onMessageReceived(RemoteMessage remoteMessage) {
    }

    @WorkerThread
    public void onMessageSent(String str) {
    }

    @WorkerThread
    public void onSendError(String str, Exception exception) {
    }

    protected final Intent zzn(Intent intent) {
        return zzq.zzbyp().zzbyq();
    }

    public final boolean zzo(Intent intent) {
        if (!"com.google.firebase.messaging.NOTIFICATION_OPEN".equals(intent.getAction())) {
            return false;
        }
        PendingIntent pendingIntent = (PendingIntent) intent.getParcelableExtra("pending_intent");
        if (pendingIntent != null) {
            try {
                pendingIntent.send();
            } catch (CanceledException e) {
                Log.e("FirebaseMessaging", "Notification pending intent canceled");
            }
        }
        if (zzaf(intent.getExtras())) {
            zzd.zzh(this, intent);
        }
        return true;
    }
}
