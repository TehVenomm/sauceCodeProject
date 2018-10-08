package org.onepf.openiab;

import android.content.Intent;
import android.util.Log;
import com.unity3d.player.UnityPlayer;
import org.json.JSONException;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Purchase;

class UnityPlugin$7 implements IabHelper$OnIabPurchaseFinishedListener {
    final /* synthetic */ UnityPlugin this$0;

    UnityPlugin$7(UnityPlugin unityPlugin) {
        this.this$0 = unityPlugin;
    }

    public void onIabPurchaseFinished(IabResult iabResult, Purchase purchase) {
        UnityPlayer.currentActivity.sendBroadcast(new Intent("org.onepf.openiab.ACTION_FINISH"));
        Log.d(UnityPlugin.TAG, "Purchase finished: " + iabResult + ", purchase: " + purchase);
        if (iabResult.isFailure()) {
            Log.e(UnityPlugin.TAG, "Error purchasing: " + iabResult);
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnPurchaseFailed", iabResult.getResponse() + "|" + iabResult.getMessage());
            return;
        }
        Log.d(UnityPlugin.TAG, "Purchase successful.");
        try {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnPurchaseSucceeded", UnityPlugin.access$300(this.this$0, purchase));
        } catch (JSONException e) {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnPurchaseFailed", "-1|Couldn't serialize the purchase");
        }
    }
}
