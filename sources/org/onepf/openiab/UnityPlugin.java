package org.onepf.openiab;

import android.content.BroadcastReceiver;
import android.content.Intent;
import android.content.IntentFilter;
import android.util.Log;
import com.facebook.share.internal.ShareConstants;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.unity3d.player.UnityPlayer;
import java.util.HashMap;
import java.util.Map.Entry;
import org.json.JSONException;
import org.json.JSONStringer;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper$Options;
import org.onepf.oms.OpenIabHelper$Options.Builder;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
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
    private BroadcastReceiver _billingReceiver = new UnityPlugin$9(this);
    OnConsumeFinishedListener _consumeFinishedListener = new UnityPlugin$8(this);
    private OpenIabHelper _helper;
    IabHelper$OnIabPurchaseFinishedListener _purchaseFinishedListener = new UnityPlugin$7(this);
    QueryInventoryFinishedListener _queryInventoryListener = new UnityPlugin$6(this);

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

    public void consumeProduct(String str) {
        UnityPlayer.currentActivity.runOnUiThread(new UnityPlugin$5(this, str));
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

    public IabHelper$OnIabPurchaseFinishedListener getPurchaseFinishedListener() {
        return this._purchaseFinishedListener;
    }

    public void init(HashMap<String, String> hashMap) {
        initWithOptions(new Builder().addStoreKeys(hashMap).build());
    }

    public void initWithOptions(OpenIabHelper$Options openIabHelper$Options) {
        UnityPlayer.currentActivity.runOnUiThread(new UnityPlugin$1(this, openIabHelper$Options));
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
        UnityPlayer.currentActivity.runOnUiThread(new UnityPlugin$2(this));
    }

    public void queryInventory(String[] strArr) {
        UnityPlayer.currentActivity.runOnUiThread(new UnityPlugin$3(this, strArr));
    }

    public void queryInventory(String[] strArr, String[] strArr2) {
        UnityPlayer.currentActivity.runOnUiThread(new UnityPlugin$4(this, strArr, strArr2));
    }

    public void unbindService() {
        if (this._helper != null) {
            this._helper.dispose();
            this._helper = null;
        }
        destroyBroadcasts();
    }
}
