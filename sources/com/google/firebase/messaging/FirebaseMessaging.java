package com.google.firebase.messaging;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.tasks.Task;
import com.google.firebase.FirebaseApp;
import com.google.firebase.iid.FirebaseInstanceId;
import java.util.regex.Pattern;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

public class FirebaseMessaging {
    public static final String INSTANCE_ID_SCOPE = "FCM";
    private static final Pattern zzdw = Pattern.compile("[a-zA-Z0-9-_.~%]{1,900}");
    private static FirebaseMessaging zzdx;
    private final FirebaseInstanceId zzdm;

    private FirebaseMessaging(FirebaseInstanceId firebaseInstanceId) {
        this.zzdm = firebaseInstanceId;
    }

    public static FirebaseMessaging getInstance() {
        FirebaseMessaging firebaseMessaging;
        synchronized (FirebaseMessaging.class) {
            try {
                if (zzdx == null) {
                    zzdx = new FirebaseMessaging(FirebaseInstanceId.getInstance());
                }
                firebaseMessaging = zzdx;
            } finally {
                Class<FirebaseMessaging> cls = FirebaseMessaging.class;
            }
        }
        return firebaseMessaging;
    }

    public boolean isAutoInitEnabled() {
        return this.zzdm.zzq();
    }

    public void send(RemoteMessage remoteMessage) {
        if (TextUtils.isEmpty(remoteMessage.getTo())) {
            throw new IllegalArgumentException("Missing 'to'");
        }
        Context applicationContext = FirebaseApp.getInstance().getApplicationContext();
        Intent intent = new Intent("com.google.android.gcm.intent.SEND");
        Intent intent2 = new Intent();
        intent2.setPackage("com.google.example.invalidpackage");
        intent.putExtra(SettingsJsonConstants.APP_KEY, PendingIntent.getBroadcast(applicationContext, 0, intent2, 0));
        intent.setPackage("com.google.android.gms");
        intent.putExtras(remoteMessage.zzee);
        applicationContext.sendOrderedBroadcast(intent, "com.google.android.gtalkservice.permission.GTALK_SERVICE");
    }

    public void setAutoInitEnabled(boolean z) {
        this.zzdm.zzb(z);
    }

    public Task<Void> subscribeToTopic(String str) {
        if (str != null && str.startsWith("/topics/")) {
            Log.w("FirebaseMessaging", "Format /topics/topic-name is deprecated. Only 'topic-name' should be used in subscribeToTopic.");
            str = str.substring(8);
        }
        if (str == null || !zzdw.matcher(str).matches()) {
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 78).append("Invalid topic name: ").append(str).append(" does not match the allowed format [a-zA-Z0-9-_.~%]{1,900}").toString());
        }
        FirebaseInstanceId firebaseInstanceId = this.zzdm;
        String valueOf = String.valueOf("S!");
        String valueOf2 = String.valueOf(str);
        return firebaseInstanceId.zza(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
    }

    public Task<Void> unsubscribeFromTopic(String str) {
        if (str != null && str.startsWith("/topics/")) {
            Log.w("FirebaseMessaging", "Format /topics/topic-name is deprecated. Only 'topic-name' should be used in unsubscribeFromTopic.");
            str = str.substring(8);
        }
        if (str == null || !zzdw.matcher(str).matches()) {
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 78).append("Invalid topic name: ").append(str).append(" does not match the allowed format [a-zA-Z0-9-_.~%]{1,900}").toString());
        }
        FirebaseInstanceId firebaseInstanceId = this.zzdm;
        String valueOf = String.valueOf("U!");
        String valueOf2 = String.valueOf(str);
        return firebaseInstanceId.zza(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
    }
}
