package com.google.firebase.messaging;

import android.app.IntentService;
import android.content.Intent;
import android.os.Bundle;
import com.google.firebase.messaging.cpp.DebugLogging;
import com.google.firebase.messaging.cpp.ListenerService;

public class MessageForwardingService extends IntentService {
    public static final String ACTION_REMOTE_INTENT = "com.google.android.c2dm.intent.RECEIVE";
    private static final String TAG = "FIREBASE_MSG_FWDR";

    public MessageForwardingService() {
        super(TAG);
    }

    protected void onHandleIntent(Intent intent) {
        Object action = intent == null ? "null intent" : intent.getAction() == null ? "(null)" : intent.getAction();
        String valueOf = String.valueOf(action);
        DebugLogging.log(TAG, valueOf.length() != 0 ? "onHandleIntent ".concat(valueOf) : new String("onHandleIntent "));
        if (intent != null && intent.getAction() != null && intent.getAction().equals(ACTION_REMOTE_INTENT)) {
            Bundle extras = intent.getExtras();
            valueOf = String.valueOf(extras == null ? "(null)" : extras.toString());
            DebugLogging.log(TAG, valueOf.length() != 0 ? "extras: ".concat(valueOf) : new String("extras: "));
            if (extras != null) {
                RemoteMessage remoteMessage = new RemoteMessage(extras);
                valueOf = String.valueOf(remoteMessage.toString());
                DebugLogging.log(TAG, valueOf.length() != 0 ? "message: ".concat(valueOf) : new String("message: "));
                if (remoteMessage.getFrom() != null && remoteMessage.getMessageId() != null) {
                    ListenerService.onMessageReceived(this, remoteMessage, true);
                }
            }
        }
    }
}
