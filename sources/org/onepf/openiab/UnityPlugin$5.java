package org.onepf.openiab;

import com.unity3d.player.UnityPlayer;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.Purchase;

class UnityPlugin$5 implements Runnable {
    final /* synthetic */ UnityPlugin this$0;
    final /* synthetic */ String val$json;

    UnityPlugin$5(UnityPlugin unityPlugin, String str) {
        this.this$0 = unityPlugin;
        this.val$json = str;
    }

    public void run() {
        try {
            JSONObject jSONObject = new JSONObject(this.val$json);
            String string = jSONObject.getString("appstoreName");
            String string2 = jSONObject.getString("originalJson");
            String string3 = jSONObject.getString("packageName");
            String string4 = jSONObject.getString("token");
            if (string2 == null || string2.equals("") || string2.equals("null")) {
                UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnConsumePurchaseFailed", "Original json is invalid: " + this.val$json);
                return;
            }
            Purchase purchase = new Purchase(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE), string2, jSONObject.getString("signature"), string);
            purchase.setPackageName(string3);
            purchase.setToken(string4);
            UnityPlugin.access$000(this.this$0).consumeAsync(purchase, this.this$0._consumeFinishedListener);
        } catch (JSONException e) {
            e.printStackTrace();
            UnityPlayer.UnitySendMessage("OpenIABEventManager", "OnConsumePurchaseFailed", "Invalid json: " + this.val$json + ". " + e);
        }
    }
}
