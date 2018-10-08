package org.onepf.openiab;

import android.util.Log;
import com.unity3d.player.UnityPlayer;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper$Options;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;

class UnityPlugin$1 implements Runnable {
    final /* synthetic */ UnityPlugin this$0;
    final /* synthetic */ OpenIabHelper$Options val$options;

    /* renamed from: org.onepf.openiab.UnityPlugin$1$1 */
    class C16461 implements OnIabSetupFinishedListener {
        C16461() {
        }

        public void onIabSetupFinished(IabResult iabResult) {
            Log.d(UnityPlugin.TAG, "Setup finished.");
            if (iabResult.isFailure()) {
                Log.e(UnityPlugin.TAG, "Problem setting up in-app billing: " + iabResult);
                UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnBillingNotSupported", iabResult.getMessage());
                return;
            }
            Log.d(UnityPlugin.TAG, "Setup successful.");
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnBillingSupported", "");
        }
    }

    UnityPlugin$1(UnityPlugin unityPlugin, OpenIabHelper$Options openIabHelper$Options) {
        this.this$0 = unityPlugin;
        this.val$options = openIabHelper$Options;
    }

    public void run() {
        UnityPlugin.access$002(this.this$0, new OpenIabHelper(UnityPlayer.currentActivity, this.val$options));
        UnityPlugin.access$100(this.this$0);
        Log.d(UnityPlugin.TAG, "Starting setup.");
        UnityPlugin.access$000(this.this$0).startSetup(new C16461());
    }
}
