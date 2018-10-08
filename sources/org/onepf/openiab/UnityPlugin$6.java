package org.onepf.openiab;

import android.util.Log;
import com.unity3d.player.UnityPlayer;
import org.json.JSONException;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;

class UnityPlugin$6 implements QueryInventoryFinishedListener {
    final /* synthetic */ UnityPlugin this$0;

    UnityPlugin$6(UnityPlugin unityPlugin) {
        this.this$0 = unityPlugin;
    }

    public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
        Log.d(UnityPlugin.TAG, "Query inventory finished.");
        if (iabResult.isFailure()) {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnQueryInventoryFailed", iabResult.getMessage());
            return;
        }
        Log.d(UnityPlugin.TAG, "Query inventory was successful.");
        try {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnQueryInventorySucceeded", UnityPlugin.access$200(this.this$0, inventory));
        } catch (JSONException e) {
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnQueryInventoryFailed", "Couldn't serialize the inventory");
        }
    }
}
