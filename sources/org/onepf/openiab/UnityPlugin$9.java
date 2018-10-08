package org.onepf.openiab;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

class UnityPlugin$9 extends BroadcastReceiver {
    private static final String TAG = "YandexBillingReceiver";
    final /* synthetic */ UnityPlugin this$0;

    UnityPlugin$9(UnityPlugin unityPlugin) {
        this.this$0 = unityPlugin;
    }

    private void purchaseStateChanged(Intent intent) {
        Log.d(TAG, "purchaseStateChanged intent: " + intent);
        UnityPlugin.access$000(this.this$0).handleActivityResult(10001, -1, intent);
    }

    public void onReceive(Context context, Intent intent) {
        String action = intent.getAction();
        Log.d(TAG, "onReceive intent: " + intent);
        if (UnityPlugin.YANDEX_STORE_ACTION_PURCHASE_STATE_CHANGED.equals(action)) {
            purchaseStateChanged(intent);
        }
    }
}
