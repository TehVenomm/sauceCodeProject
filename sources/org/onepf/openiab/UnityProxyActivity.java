package org.onepf.openiab;

import android.app.Activity;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.util.Log;
import org.onepf.oms.appstore.googleUtils.IabResult;

public class UnityProxyActivity extends Activity {
    static final String ACTION_FINISH = "org.onepf.openiab.ACTION_FINISH";
    private BroadcastReceiver broadcastReceiver;

    /* renamed from: org.onepf.openiab.UnityProxyActivity$1 */
    class C15981 extends BroadcastReceiver {
        C15981() {
        }

        public void onReceive(Context context, Intent intent) {
            Log.d(UnityPlugin.TAG, "Finish broadcast was received");
            if (!UnityProxyActivity.this.isFinishing()) {
                UnityProxyActivity.this.finish();
            }
        }
    }

    protected void onActivityResult(int i, int i2, Intent intent) {
        Log.d(UnityPlugin.TAG, "onActivityResult(" + i + ", " + i2 + ", " + intent);
        if (UnityPlugin.instance().getHelper().handleActivityResult(i, i2, intent)) {
            Log.d(UnityPlugin.TAG, "onActivityResult handled by IABUtil.");
        } else {
            super.onActivityResult(i, i2, intent);
        }
    }

    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.broadcastReceiver = new C15981();
        registerReceiver(this.broadcastReceiver, new IntentFilter(ACTION_FINISH));
        if (UnityPlugin.sendRequest) {
            UnityPlugin.sendRequest = false;
            Intent intent = getIntent();
            String stringExtra = intent.getStringExtra("sku");
            String stringExtra2 = intent.getStringExtra("developerPayload");
            if (intent.getBooleanExtra("inapp", true)) {
                try {
                    UnityPlugin.instance().getHelper().launchPurchaseFlow(this, stringExtra, 10001, UnityPlugin.instance().getPurchaseFinishedListener(), stringExtra2);
                    return;
                } catch (IllegalStateException e) {
                    UnityPlugin.instance().getPurchaseFinishedListener().onIabPurchaseFinished(new IabResult(3, "Cannot start purchase process. Billing unavailable."), null);
                    return;
                }
            }
            UnityPlugin.instance().getHelper().launchSubscriptionPurchaseFlow(this, stringExtra, 10001, UnityPlugin.instance().getPurchaseFinishedListener(), stringExtra2);
        }
    }

    protected void onDestroy() {
        super.onDestroy();
        unregisterReceiver(this.broadcastReceiver);
    }
}
