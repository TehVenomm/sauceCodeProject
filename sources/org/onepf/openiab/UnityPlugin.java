package org.onepf.openiab;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.util.Log;
import com.facebook.share.internal.ShareConstants;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.unity3d.player.UnityPlayer;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map.Entry;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONStringer;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options;
import org.onepf.oms.OpenIabHelper.Options.Builder;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import org.onepf.oms.util.Logger;

public class UnityPlugin {
    private static final String BILLING_NOT_SUPPORTED_CALLBACK = "OnBillingNotSupported";
    private static final String BILLING_SUPPORTED_CALLBACK = "OnBillingSupported";
    private static final String CONSUME_PURCHASE_FAILED_CALLBACK = "OnConsumePurchaseFailed";
    private static final String CONSUME_PURCHASE_SUCCEEDED_CALLBACK = "OnConsumePurchaseSucceeded";
    private static final String EVENT_MANAGER = "OpenIABEventManager";
    private static final String MAP_SKU_FAILED_CALLBACK = "OnMapSkuFailed";
    private static final String PURCHASE_FAILED_CALLBACK = "OnPurchaseFailed";
    private static final String PURCHASE_SUCCEEDED_CALLBACK = "OnPurchaseSucceeded";
    private static final String QUERY_INVENTORY_FAILED_CALLBACK = "OnQueryInventoryFailed";
    private static final String QUERY_INVENTORY_SUCCEEDED_CALLBACK = "OnQueryInventorySucceeded";
    public static final int RC_REQUEST = 10001;
    public static final String TAG = "OpenIAB-UnityPlugin";
    public static final String YANDEX_STORE_ACTION_PURCHASE_STATE_CHANGED = "com.yandex.store.service.PURCHASE_STATE_CHANGED";
    public static final String YANDEX_STORE_SERVICE = "com.yandex.store.service";
    private static UnityPlugin _instance;
    public static boolean sendRequest = false;
    private BroadcastReceiver _billingReceiver = new C13479();
    OnConsumeFinishedListener _consumeFinishedListener = new C13468();
    private OpenIabHelper _helper;
    OnIabPurchaseFinishedListener _purchaseFinishedListener = new C13457();
    QueryInventoryFinishedListener _queryInventoryListener = new C13446();

    /* renamed from: org.onepf.openiab.UnityPlugin$2 */
    class C13402 implements Runnable {
        C13402() {
        }

        public void run() {
            UnityPlugin.this._helper.queryInventoryAsync(UnityPlugin.this._queryInventoryListener);
        }
    }

    /* renamed from: org.onepf.openiab.UnityPlugin$6 */
    class C13446 implements QueryInventoryFinishedListener {
        C13446() {
        }

        public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
            Log.d(UnityPlugin.TAG, "Query inventory finished.");
            if (iabResult.isFailure()) {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.QUERY_INVENTORY_FAILED_CALLBACK, iabResult.getMessage());
                return;
            }
            Log.d(UnityPlugin.TAG, "Query inventory was successful.");
            try {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.QUERY_INVENTORY_SUCCEEDED_CALLBACK, UnityPlugin.this.inventoryToJson(inventory));
            } catch (JSONException e) {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.QUERY_INVENTORY_FAILED_CALLBACK, "Couldn't serialize the inventory");
            }
        }
    }

    /* renamed from: org.onepf.openiab.UnityPlugin$7 */
    class C13457 implements OnIabPurchaseFinishedListener {
        C13457() {
        }

        public void onIabPurchaseFinished(IabResult iabResult, Purchase purchase) {
            UnityPlayer.currentActivity.sendBroadcast(new Intent("org.onepf.openiab.ACTION_FINISH"));
            Log.d(UnityPlugin.TAG, "Purchase finished: " + iabResult + ", purchase: " + purchase);
            if (iabResult.isFailure()) {
                Log.e(UnityPlugin.TAG, "Error purchasing: " + iabResult);
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.PURCHASE_FAILED_CALLBACK, iabResult.getResponse() + "|" + iabResult.getMessage());
                return;
            }
            Log.d(UnityPlugin.TAG, "Purchase successful.");
            try {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.PURCHASE_SUCCEEDED_CALLBACK, UnityPlugin.this.purchaseToJson(purchase));
            } catch (JSONException e) {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.PURCHASE_FAILED_CALLBACK, "-1|Couldn't serialize the purchase");
            }
        }
    }

    /* renamed from: org.onepf.openiab.UnityPlugin$8 */
    class C13468 implements OnConsumeFinishedListener {
        C13468() {
        }

        public void onConsumeFinished(Purchase purchase, IabResult iabResult) {
            Log.d(UnityPlugin.TAG, "Consumption finished. Purchase: " + purchase + ", result: " + iabResult);
            purchase.setSku(SkuManager.getInstance().getSku(purchase.getAppstoreName(), purchase.getSku()));
            if (iabResult.isFailure()) {
                Log.e(UnityPlugin.TAG, "Error while consuming: " + iabResult);
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.CONSUME_PURCHASE_FAILED_CALLBACK, iabResult.getMessage());
                return;
            }
            Log.d(UnityPlugin.TAG, "Consumption successful. Provisioning.");
            try {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.CONSUME_PURCHASE_SUCCEEDED_CALLBACK, UnityPlugin.this.purchaseToJson(purchase));
            } catch (JSONException e) {
                UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.CONSUME_PURCHASE_FAILED_CALLBACK, "Couldn't serialize the purchase");
            }
        }
    }

    /* renamed from: org.onepf.openiab.UnityPlugin$9 */
    class C13479 extends BroadcastReceiver {
        private static final String TAG = "YandexBillingReceiver";

        C13479() {
        }

        private void purchaseStateChanged(Intent intent) {
            Log.d(TAG, "purchaseStateChanged intent: " + intent);
            UnityPlugin.this._helper.handleActivityResult(10001, -1, intent);
        }

        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            Log.d(TAG, "onReceive intent: " + intent);
            if (UnityPlugin.YANDEX_STORE_ACTION_PURCHASE_STATE_CHANGED.equals(action)) {
                purchaseStateChanged(intent);
            }
        }
    }

    private void createBroadcasts() {
        Log.d(TAG, "createBroadcasts");
        UnityPlayer.currentActivity.registerReceiver(this._billingReceiver, new IntentFilter(YANDEX_STORE_ACTION_PURCHASE_STATE_CHANGED));
    }

    private void destroyBroadcasts() {
        Log.d(TAG, "destroyBroadcasts");
        try {
            UnityPlayer.currentActivity.unregisterReceiver(this._billingReceiver);
        } catch (Exception e) {
            Log.d(TAG, "destroyBroadcasts exception:\n" + e.getMessage());
        }
    }

    public static UnityPlugin instance() {
        if (_instance == null) {
            _instance = new UnityPlugin();
        }
        return _instance;
    }

    private String inventoryToJson(Inventory inventory) throws JSONException {
        JSONStringer object = new JSONStringer().object();
        object.key("purchaseMap").array();
        for (Entry entry : inventory.getPurchaseMap().entrySet()) {
            object.array();
            object.value(entry.getKey());
            object.value(purchaseToJson((Purchase) entry.getValue()));
            object.endArray();
        }
        object.endArray();
        object.key("skuMap").array();
        for (Entry entry2 : inventory.getSkuMap().entrySet()) {
            object.array();
            object.value(entry2.getKey());
            object.value(skuDetailsToJson((SkuDetails) entry2.getValue()));
            object.endArray();
        }
        object.endArray();
        object.endObject();
        return object.toString();
    }

    private String purchaseToJson(Purchase purchase) throws JSONException {
        return new JSONStringer().object().key(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).value(purchase.getItemType()).key(AmazonAppstoreBillingService.JSON_KEY_ORDER_ID).value(purchase.getOrderId()).key("packageName").value(purchase.getPackageName()).key("sku").value(purchase.getSku()).key("purchaseTime").value(purchase.getPurchaseTime()).key("purchaseState").value((long) purchase.getPurchaseState()).key("developerPayload").value(purchase.getDeveloperPayload()).key("token").value(purchase.getToken()).key("originalJson").value(purchase.getOriginalJson()).key("signature").value(purchase.getSignature()).key("appstoreName").value(purchase.getAppstoreName()).endObject().toString();
    }

    private String skuDetailsToJson(SkuDetails skuDetails) throws JSONException {
        return new JSONStringer().object().key(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).value(skuDetails.getItemType()).key("sku").value(skuDetails.getSku()).key(ShareConstants.MEDIA_TYPE).value(skuDetails.getType()).key(Param.PRICE).value(skuDetails.getPrice()).key("title").value(skuDetails.getTitle()).key("description").value(skuDetails.getDescription()).key("json").value(skuDetails.getJson()).endObject().toString();
    }

    private void startProxyPurchaseActivity(String str, boolean z, String str2) {
        if (instance().getHelper() == null) {
            Log.e(TAG, "OpenIAB UnityPlugin not initialized!");
            return;
        }
        sendRequest = true;
        Intent intent = new Intent(UnityPlayer.currentActivity, UnityProxyActivity.class);
        intent.putExtra("sku", str);
        intent.putExtra("inapp", z);
        intent.putExtra("developerPayload", str2);
        UnityPlayer.currentActivity.startActivity(intent);
    }

    public boolean areSubscriptionsSupported() {
        return this._helper.subscriptionsSupported();
    }

    public void consumeProduct(final String str) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                try {
                    JSONObject jSONObject = new JSONObject(str);
                    String string = jSONObject.getString("appstoreName");
                    String string2 = jSONObject.getString("originalJson");
                    String string3 = jSONObject.getString("packageName");
                    String string4 = jSONObject.getString("token");
                    if (string2 == null || string2.equals("") || string2.equals("null")) {
                        UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.CONSUME_PURCHASE_FAILED_CALLBACK, "Original json is invalid: " + str);
                        return;
                    }
                    Purchase purchase = new Purchase(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE), string2, jSONObject.getString("signature"), string);
                    purchase.setPackageName(string3);
                    purchase.setToken(string4);
                    UnityPlugin.this._helper.consumeAsync(purchase, UnityPlugin.this._consumeFinishedListener);
                } catch (JSONException e) {
                    e.printStackTrace();
                    UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.CONSUME_PURCHASE_FAILED_CALLBACK, "Invalid json: " + str + ". " + e);
                }
            }
        });
    }

    public void enableDebugLogging(boolean z) {
        Logger.setLoggable(z);
    }

    public void enableDebugLogging(boolean z, String str) {
        Logger.setLoggable(z);
    }

    public OpenIabHelper getHelper() {
        return this._helper;
    }

    public OnIabPurchaseFinishedListener getPurchaseFinishedListener() {
        return this._purchaseFinishedListener;
    }

    public void init(HashMap<String, String> hashMap) {
        initWithOptions(new Builder().addStoreKeys(hashMap).build());
    }

    public void initWithOptions(final Options options) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {

            /* renamed from: org.onepf.openiab.UnityPlugin$1$1 */
            class C13381 implements OnIabSetupFinishedListener {
                C13381() {
                }

                public void onIabSetupFinished(IabResult iabResult) {
                    Log.d(UnityPlugin.TAG, "Setup finished.");
                    if (iabResult.isFailure()) {
                        Log.e(UnityPlugin.TAG, "Problem setting up in-app billing: " + iabResult);
                        UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.BILLING_NOT_SUPPORTED_CALLBACK, iabResult.getMessage());
                        return;
                    }
                    Log.d(UnityPlugin.TAG, "Setup successful.");
                    UnityPlayer.UnitySendMessage(UnityPlugin.EVENT_MANAGER, UnityPlugin.BILLING_SUPPORTED_CALLBACK, "");
                }
            }

            public void run() {
                UnityPlugin.this._helper = new OpenIabHelper(UnityPlayer.currentActivity, options);
                UnityPlugin.this.createBroadcasts();
                Log.d(UnityPlugin.TAG, "Starting setup.");
                UnityPlugin.this._helper.startSetup(new C13381());
            }
        });
    }

    public boolean isDebugLog() {
        return Logger.isLoggable();
    }

    public void mapSku(String str, String str2, String str3) {
        try {
            SkuManager.getInstance().mapSku(str, str2, str3);
        } catch (Exception e) {
            Log.d(TAG, e.toString());
            UnityPlayer.UnitySendMessage(EVENT_MANAGER, MAP_SKU_FAILED_CALLBACK, e.toString());
        }
    }

    public void purchaseProduct(String str, String str2) {
        startProxyPurchaseActivity(str, true, str2);
    }

    public void purchaseSubscription(String str, String str2) {
        startProxyPurchaseActivity(str, false, str2);
    }

    public void queryInventory() {
        UnityPlayer.currentActivity.runOnUiThread(new C13402());
    }

    public void queryInventory(final String[] strArr) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                UnityPlugin.this._helper.queryInventoryAsync(true, Arrays.asList(strArr), UnityPlugin.this._queryInventoryListener);
            }
        });
    }

    public void queryInventory(final String[] strArr, final String[] strArr2) {
        UnityPlayer.currentActivity.runOnUiThread(new Runnable() {
            public void run() {
                UnityPlugin.this._helper.queryInventoryAsync(true, Arrays.asList(strArr), Arrays.asList(strArr2), UnityPlugin.this._queryInventoryListener);
            }
        });
    }

    public void unbindService() {
        if (this._helper != null) {
            this._helper.dispose();
            this._helper = null;
        }
        destroyBroadcasts();
    }
}
