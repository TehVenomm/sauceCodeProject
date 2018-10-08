package com.google.firebase.messaging;

import android.content.Context;
import android.content.Intent;
import android.text.TextUtils;
import android.util.Log;
import com.google.firebase.FirebaseApp;
import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.zzl;
import java.util.regex.Pattern;

public class FirebaseMessaging {
    public static final String INSTANCE_ID_SCOPE = "FCM";
    private static final Pattern zzmjv = Pattern.compile("[a-zA-Z0-9-_.~%]{1,900}");
    private static FirebaseMessaging zzmjw;
    private final FirebaseInstanceId zzmiv;

    private FirebaseMessaging(FirebaseInstanceId firebaseInstanceId) {
        this.zzmiv = firebaseInstanceId;
    }

    public static FirebaseMessaging getInstance() {
        synchronized (FirebaseMessaging.class) {
            Class instance;
            try {
                if (zzmjw == null) {
                    instance = FirebaseInstanceId.getInstance();
                    zzmjw = new FirebaseMessaging(instance);
                }
                FirebaseMessaging firebaseMessaging = zzmjw;
                return firebaseMessaging;
            } finally {
                instance = FirebaseMessaging.class;
            }
        }
    }

    public void send(RemoteMessage remoteMessage) {
        if (TextUtils.isEmpty(remoteMessage.getTo())) {
            throw new IllegalArgumentException("Missing 'to'");
        }
        Context applicationContext = FirebaseApp.getInstance().getApplicationContext();
        String zzdg = zzl.zzdg(applicationContext);
        if (zzdg == null) {
            Log.e("FirebaseMessaging", "Google Play services package is missing. Impossible to send message");
            return;
        }
        Intent intent = new Intent("com.google.android.gcm.intent.SEND");
        zzl.zzd(applicationContext, intent);
        intent.setPackage(zzdg);
        intent.putExtras(remoteMessage.mBundle);
        applicationContext.sendOrderedBroadcast(intent, "com.google.android.gtalkservice.permission.GTALK_SERVICE");
    }

    public void subscribeToTopic(String str) {
        if (str != null && str.startsWith("/topics/")) {
            Log.w("FirebaseMessaging", "Format /topics/topic-name is deprecated. Only 'topic-name' should be used in subscribeToTopic.");
            Object substring = str.substring(8);
        }
        if (substring == null || !zzmjv.matcher(substring).matches()) {
            throw new IllegalArgumentException(new StringBuilder((String.valueOf(substring).length() + 55) + String.valueOf("[a-zA-Z0-9-_.~%]{1,900}").length()).append("Invalid topic name: ").append(substring).append(" does not match the allowed format ").append("[a-zA-Z0-9-_.~%]{1,900}").toString());
        }
        FirebaseInstanceId firebaseInstanceId = this.zzmiv;
        String valueOf = String.valueOf("S!");
        String valueOf2 = String.valueOf(substring);
        firebaseInstanceId.zzpq(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
    }

    public void unsubscribeFromTopic(String str) {
        if (str != null && str.startsWith("/topics/")) {
            Log.w("FirebaseMessaging", "Format /topics/topic-name is deprecated. Only 'topic-name' should be used in unsubscribeFromTopic.");
            Object substring = str.substring(8);
        }
        if (substring == null || !zzmjv.matcher(substring).matches()) {
            throw new IllegalArgumentException(new StringBuilder((String.valueOf(substring).length() + 55) + String.valueOf("[a-zA-Z0-9-_.~%]{1,900}").length()).append("Invalid topic name: ").append(substring).append(" does not match the allowed format ").append("[a-zA-Z0-9-_.~%]{1,900}").toString());
        }
        FirebaseInstanceId firebaseInstanceId = this.zzmiv;
        String valueOf = String.valueOf("U!");
        String valueOf2 = String.valueOf(substring);
        firebaseInstanceId.zzpq(valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
    }
}
