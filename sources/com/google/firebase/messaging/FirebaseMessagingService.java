package com.google.firebase.messaging;

import android.app.PendingIntent;
import android.app.PendingIntent.CanceledException;
import android.content.Intent;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.firebase.iid.zzaw;
import com.google.firebase.iid.zzc;
import java.util.ArrayDeque;
import java.util.Queue;

public class FirebaseMessagingService extends zzc {
    private static final Queue<String> zzec = new ArrayDeque(10);

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
    public void onNewToken(String str) {
    }

    @WorkerThread
    public void onSendError(String str, Exception exc) {
    }

    /* access modifiers changed from: protected */
    public final Intent zzb(Intent intent) {
        return zzaw.zzak().zzal();
    }

    public final boolean zzc(Intent intent) {
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
        if (MessagingAnalytics.shouldUploadMetrics(intent)) {
            MessagingAnalytics.logNotificationOpen(intent);
        }
        return true;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:41:0x00c3, code lost:
        if (r1.equals("gcm") != false) goto L_0x0044;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zzd(android.content.Intent r9) {
        /*
            r8 = this;
            r5 = 3
            r4 = 2
            r3 = 1
            r2 = 0
            java.lang.String r0 = r9.getAction()
            java.lang.String r1 = "com.google.android.c2dm.intent.RECEIVE"
            boolean r1 = r1.equals(r0)
            if (r1 != 0) goto L_0x0018
            java.lang.String r1 = "com.google.firebase.messaging.RECEIVE_DIRECT_BOOT"
            boolean r1 = r1.equals(r0)
            if (r1 == 0) goto L_0x0196
        L_0x0018:
            java.lang.String r0 = "google.message_id"
            java.lang.String r1 = r9.getStringExtra(r0)
            boolean r0 = android.text.TextUtils.isEmpty(r1)
            if (r0 == 0) goto L_0x0064
            r0 = 0
            com.google.android.gms.tasks.Task r0 = com.google.android.gms.tasks.Tasks.forResult(r0)
        L_0x0029:
            boolean r6 = android.text.TextUtils.isEmpty(r1)
            if (r6 == 0) goto L_0x0077
            r1 = r2
        L_0x0030:
            if (r1 != 0) goto L_0x005c
            java.lang.String r1 = "message_type"
            java.lang.String r1 = r9.getStringExtra(r1)
            if (r1 != 0) goto L_0x003c
            java.lang.String r1 = "gcm"
        L_0x003c:
            int r6 = r1.hashCode()
            switch(r6) {
                case -2062414158: goto L_0x00c7;
                case 102161: goto L_0x00bd;
                case 814694033: goto L_0x00dd;
                case 814800675: goto L_0x00d2;
                default: goto L_0x0043;
            }
        L_0x0043:
            r2 = -1
        L_0x0044:
            switch(r2) {
                case 0: goto L_0x00e8;
                case 1: goto L_0x0136;
                case 2: goto L_0x013b;
                case 3: goto L_0x0146;
                default: goto L_0x0047;
            }
        L_0x0047:
            java.lang.String r1 = java.lang.String.valueOf(r1)
            int r2 = r1.length()
            if (r2 == 0) goto L_0x0164
            java.lang.String r2 = "Received message with unknown type: "
            java.lang.String r1 = r2.concat(r1)
        L_0x0057:
            java.lang.String r2 = "FirebaseMessaging"
            android.util.Log.w(r2, r1)
        L_0x005c:
            r2 = 1
            java.util.concurrent.TimeUnit r1 = java.util.concurrent.TimeUnit.SECONDS     // Catch:{ ExecutionException -> 0x01df, InterruptedException -> 0x01e1, TimeoutException -> 0x016d }
            com.google.android.gms.tasks.Tasks.await(r0, r2, r1)     // Catch:{ ExecutionException -> 0x01df, InterruptedException -> 0x01e1, TimeoutException -> 0x016d }
        L_0x0063:
            return
        L_0x0064:
            android.os.Bundle r0 = new android.os.Bundle
            r0.<init>()
            java.lang.String r6 = "google.message_id"
            r0.putString(r6, r1)
            com.google.firebase.iid.zzac r6 = com.google.firebase.iid.zzac.zzc(r8)
            com.google.android.gms.tasks.Task r0 = r6.zza(r4, r0)
            goto L_0x0029
        L_0x0077:
            java.util.Queue<java.lang.String> r6 = zzec
            boolean r6 = r6.contains(r1)
            if (r6 == 0) goto L_0x00a6
            java.lang.String r6 = "FirebaseMessaging"
            boolean r6 = android.util.Log.isLoggable(r6, r5)
            if (r6 == 0) goto L_0x009c
            java.lang.String r1 = java.lang.String.valueOf(r1)
            int r6 = r1.length()
            if (r6 == 0) goto L_0x009e
            java.lang.String r6 = "Received duplicate message: "
            java.lang.String r1 = r6.concat(r1)
        L_0x0097:
            java.lang.String r6 = "FirebaseMessaging"
            android.util.Log.d(r6, r1)
        L_0x009c:
            r1 = r3
            goto L_0x0030
        L_0x009e:
            java.lang.String r1 = new java.lang.String
            java.lang.String r6 = "Received duplicate message: "
            r1.<init>(r6)
            goto L_0x0097
        L_0x00a6:
            java.util.Queue<java.lang.String> r6 = zzec
            int r6 = r6.size()
            r7 = 10
            if (r6 < r7) goto L_0x00b5
            java.util.Queue<java.lang.String> r6 = zzec
            r6.remove()
        L_0x00b5:
            java.util.Queue<java.lang.String> r6 = zzec
            r6.add(r1)
            r1 = r2
            goto L_0x0030
        L_0x00bd:
            java.lang.String r3 = "gcm"
            boolean r3 = r1.equals(r3)
            if (r3 == 0) goto L_0x0043
            goto L_0x0044
        L_0x00c7:
            java.lang.String r2 = "deleted_messages"
            boolean r2 = r1.equals(r2)
            if (r2 == 0) goto L_0x0043
            r2 = r3
            goto L_0x0044
        L_0x00d2:
            java.lang.String r2 = "send_event"
            boolean r2 = r1.equals(r2)
            if (r2 == 0) goto L_0x0043
            r2 = r4
            goto L_0x0044
        L_0x00dd:
            java.lang.String r2 = "send_error"
            boolean r2 = r1.equals(r2)
            if (r2 == 0) goto L_0x0043
            r2 = r5
            goto L_0x0044
        L_0x00e8:
            boolean r1 = com.google.firebase.messaging.MessagingAnalytics.shouldUploadMetrics(r9)
            if (r1 == 0) goto L_0x00f1
            com.google.firebase.messaging.MessagingAnalytics.logNotificationReceived(r9)
        L_0x00f1:
            android.os.Bundle r1 = r9.getExtras()
            if (r1 != 0) goto L_0x00fc
            android.os.Bundle r1 = new android.os.Bundle
            r1.<init>()
        L_0x00fc:
            java.lang.String r2 = "android.support.content.wakelockid"
            r1.remove(r2)
            boolean r2 = com.google.firebase.messaging.zzb.zzh(r1)
            if (r2 == 0) goto L_0x0127
            java.util.concurrent.ExecutorService r2 = java.util.concurrent.Executors.newSingleThreadExecutor()
            com.google.firebase.messaging.zzc r3 = new com.google.firebase.messaging.zzc
            r3.<init>(r8, r1, r2)
            boolean r3 = r3.zzas()     // Catch:{ all -> 0x0131 }
            if (r3 == 0) goto L_0x011b
            r2.shutdown()
            goto L_0x005c
        L_0x011b:
            r2.shutdown()
            boolean r2 = com.google.firebase.messaging.MessagingAnalytics.shouldUploadMetrics(r9)
            if (r2 == 0) goto L_0x0127
            com.google.firebase.messaging.MessagingAnalytics.logNotificationForeground(r9)
        L_0x0127:
            com.google.firebase.messaging.RemoteMessage r2 = new com.google.firebase.messaging.RemoteMessage
            r2.<init>(r1)
            r8.onMessageReceived(r2)
            goto L_0x005c
        L_0x0131:
            r0 = move-exception
            r2.shutdown()
            throw r0
        L_0x0136:
            r8.onDeletedMessages()
            goto L_0x005c
        L_0x013b:
            java.lang.String r1 = "google.message_id"
            java.lang.String r1 = r9.getStringExtra(r1)
            r8.onMessageSent(r1)
            goto L_0x005c
        L_0x0146:
            java.lang.String r1 = "google.message_id"
            java.lang.String r1 = r9.getStringExtra(r1)
            if (r1 != 0) goto L_0x0154
            java.lang.String r1 = "message_id"
            java.lang.String r1 = r9.getStringExtra(r1)
        L_0x0154:
            com.google.firebase.messaging.SendException r2 = new com.google.firebase.messaging.SendException
            java.lang.String r3 = "error"
            java.lang.String r3 = r9.getStringExtra(r3)
            r2.<init>(r3)
            r8.onSendError(r1, r2)
            goto L_0x005c
        L_0x0164:
            java.lang.String r1 = new java.lang.String
            java.lang.String r2 = "Received message with unknown type: "
            r1.<init>(r2)
            goto L_0x0057
        L_0x016d:
            r0 = move-exception
        L_0x016e:
            java.lang.String r0 = java.lang.String.valueOf(r0)
            java.lang.String r1 = "FirebaseMessaging"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            java.lang.String r3 = java.lang.String.valueOf(r0)
            int r3 = r3.length()
            int r3 = r3 + 20
            r2.<init>(r3)
            java.lang.String r3 = "Message ack failed: "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.StringBuilder r0 = r2.append(r0)
            java.lang.String r0 = r0.toString()
            android.util.Log.w(r1, r0)
            goto L_0x0063
        L_0x0196:
            java.lang.String r1 = "com.google.firebase.messaging.NOTIFICATION_DISMISS"
            boolean r1 = r1.equals(r0)
            if (r1 == 0) goto L_0x01a9
            boolean r0 = com.google.firebase.messaging.MessagingAnalytics.shouldUploadMetrics(r9)
            if (r0 == 0) goto L_0x0063
            com.google.firebase.messaging.MessagingAnalytics.logNotificationDismiss(r9)
            goto L_0x0063
        L_0x01a9:
            java.lang.String r1 = "com.google.firebase.messaging.NEW_TOKEN"
            boolean r0 = r1.equals(r0)
            if (r0 == 0) goto L_0x01bc
            java.lang.String r0 = "token"
            java.lang.String r0 = r9.getStringExtra(r0)
            r8.onNewToken(r0)
            goto L_0x0063
        L_0x01bc:
            java.lang.String r0 = r9.getAction()
            java.lang.String r0 = java.lang.String.valueOf(r0)
            int r1 = r0.length()
            if (r1 == 0) goto L_0x01d7
            java.lang.String r1 = "Unknown intent action: "
            java.lang.String r0 = r1.concat(r0)
        L_0x01d0:
            java.lang.String r1 = "FirebaseMessaging"
            android.util.Log.d(r1, r0)
            goto L_0x0063
        L_0x01d7:
            java.lang.String r0 = new java.lang.String
            java.lang.String r1 = "Unknown intent action: "
            r0.<init>(r1)
            goto L_0x01d0
        L_0x01df:
            r0 = move-exception
            goto L_0x016e
        L_0x01e1:
            r0 = move-exception
            goto L_0x016e
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.messaging.FirebaseMessagingService.zzd(android.content.Intent):void");
    }
}
