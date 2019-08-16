package com.google.firebase;

import android.content.Intent;
import android.os.Bundle;
import com.google.firebase.messaging.MessageForwardingService;
import com.unity3d.player.UnityPlayerActivity;

public class MessagingUnityPlayerActivity extends UnityPlayerActivity {
    private static final String EXTRA_FROM = "google.message_id";
    private static final String EXTRA_MESSAGE_ID_KEY = "google.message_id";
    private static final String EXTRA_MESSAGE_ID_KEY_SERVER = "message_id";

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        if (this.mUnityPlayer != null) {
            this.mUnityPlayer.quit();
            this.mUnityPlayer = null;
        }
        super.onCreate(bundle);
    }

    /* access modifiers changed from: protected */
    public void onNewIntent(Intent intent) {
        Bundle extras = intent.getExtras();
        if (extras != null) {
            String string = extras.getString("google.message_id");
            String string2 = extras.getString("google.message_id");
            if (string2 == null) {
                string2 = extras.getString(EXTRA_MESSAGE_ID_KEY_SERVER);
            }
            if (!(string == null || string2 == null)) {
                Intent intent2 = new Intent(this, MessageForwardingService.class);
                intent2.setAction(MessageForwardingService.ACTION_REMOTE_INTENT);
                intent2.putExtras(intent);
                startService(intent2);
            }
            setIntent(intent);
        }
    }
}
