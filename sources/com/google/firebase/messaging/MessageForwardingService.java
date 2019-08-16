package com.google.firebase.messaging;

import android.app.IntentService;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import com.google.firebase.messaging.cpp.DebugLogging;
import com.google.firebase.messaging.cpp.MessageWriter;

public class MessageForwardingService extends IntentService {
    public static final String ACTION_REMOTE_INTENT = "com.google.android.c2dm.intent.RECEIVE";
    private static final String TAG = "FIREBASE_MSG_FWDR";

    public MessageForwardingService() {
        super(TAG);
    }

    static void handleIntent(Context context, Intent intent, MessageWriter messageWriter) {
        String action = intent == null ? "null intent" : intent.getAction() == null ? "(null)" : intent.getAction();
        String valueOf = String.valueOf(action);
        DebugLogging.log(TAG, valueOf.length() != 0 ? "onHandleIntent ".concat(valueOf) : new String("onHandleIntent "));
        if (intent != null && intent.getAction() != null && intent.getAction().equals(ACTION_REMOTE_INTENT)) {
            Bundle extras = intent.getExtras();
            String valueOf2 = String.valueOf(extras == null ? "(null)" : extras.toString());
            DebugLogging.log(TAG, valueOf2.length() != 0 ? "extras: ".concat(valueOf2) : new String("extras: "));
            if (extras != null) {
                RemoteMessage remoteMessage = new RemoteMessage(extras);
                String valueOf3 = String.valueOf(remoteMessage.toString());
                DebugLogging.log(TAG, valueOf3.length() != 0 ? "message: ".concat(valueOf3) : new String("message: "));
                if (remoteMessage.getFrom() != null && remoteMessage.getMessageId() != null) {
                    messageWriter.writeMessage(context, remoteMessage, true, intent.getData());
                }
            }
        }
    }

    /* access modifiers changed from: protected */
    public void onHandleIntent(Intent intent) {
        handleIntent(this, intent, MessageWriter.defaultInstance());
    }
}
