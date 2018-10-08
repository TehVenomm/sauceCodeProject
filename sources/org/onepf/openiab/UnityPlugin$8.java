package org.onepf.openiab;

import android.util.Log;
import com.unity3d.player.UnityPlayer;
import org.json.JSONException;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Purchase;

class UnityPlugin$8 implements OnConsumeFinishedListener {
    final /* synthetic */ UnityPlugin this$0;

    UnityPlugin$8(UnityPlugin unityPlugin) {
        this.this$0 = unityPlugin;
    }

    public void onConsumeFinished(Purchase purchase, IabResult iabResult) {
        Log.d(UnityPlugin.TAG, "Consumption finished. Purchase: " + purchase + ", result: " + iabResult);
        purchase.setSku(SkuManager.getInstance().getSku(purchase.getAppstoreName(), purchase.getSku()));
        if (iabResult.isFailure()) {
            Log.e(UnityPlugin.TAG, "Error while consuming: " + iabResult);
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnConsumePurchaseFailed", iabResult.getMessage());
            return;
        }
        Log.d(UnityPlugin.TAG, "Consumption successful. Provisioning.");
        try {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnConsumePurchaseSucceeded", UnityPlugin.access$300(this.this$0, purchase));
        } catch (JSONException e) {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnConsumePurchaseFailed", "Couldn't serialize the purchase");
        }
    }
}
