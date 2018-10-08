package jp.colopl.drapro;

import android.os.Handler;
import android.util.Log;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import jp.colopl.iab.IabException;
import jp.colopl.iab.IabHelper;
import jp.colopl.iab.IabHelper.OnConsumeMultiFinishedListener;
import jp.colopl.iab.IabHelper.QueryInventoryFinishedListener;
import jp.colopl.iab.IabResult;
import jp.colopl.iab.Inventory;
import jp.colopl.iab.Purchase;
import jp.colopl.iab.SkuDetails;
import jp.colopl.network.HttpPostAsyncTask;
import jp.colopl.network.HttpRequestListener;
import jp.colopl.util.Util;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

public class InAppBillingHelper {
    private static final String TAG = "InAppBillingHelper";
    public static StartActivity activity;
    static ArrayList<Purchase> consumeList = new ArrayList();
    private static Handler handler;
    public static IabHelper mHelper;
    private static ArrayList<Purchase> mPromotionPurchaseInfo = new ArrayList();
    public static String paymentReturnUrl;
    private static String productId;
    public static String productName;
    static int requestCount = 0;
    public static String userId;
    public static String userIdHash;

    /* renamed from: jp.colopl.drapro.InAppBillingHelper$1 */
    static final class C12841 implements QueryInventoryFinishedListener {
        C12841() {
        }

        public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
            if (!iabResult.isFailure()) {
                JSONArray jSONArray = new JSONArray();
                for (String skuDetails : AppConsts.itemCodeId) {
                    SkuDetails skuDetails2 = inventory.getSkuDetails(skuDetails);
                    JSONObject jSONObject = new JSONObject();
                    if (skuDetails2 != null) {
                        try {
                            jSONObject.put("sku", skuDetails2.getSku());
                            jSONObject.put(Param.PRICE, skuDetails2.getPrice());
                            jSONObject.put("title", skuDetails2.getTitle());
                            jSONObject.put("description", skuDetails2.getDescription());
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                    jSONArray.put(jSONObject);
                }
                JSONObject jSONObject2 = new JSONObject();
                try {
                    jSONObject2.put("ItemList", jSONArray);
                } catch (JSONException e2) {
                    e2.printStackTrace();
                }
                UnityPlayer.UnitySendMessage("ShopReceiver", "ReceiveItemPriceData", jSONObject2.toString());
            }
        }
    }

    /* renamed from: jp.colopl.drapro.InAppBillingHelper$5 */
    static final class C12895 implements OnConsumeMultiFinishedListener {
        C12895() {
        }

        public void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2) {
            boolean z = false;
            int i = 0;
            while (i < list.size()) {
                boolean z2;
                if (((IabResult) list2.get(i)).isSuccess()) {
                    InAppBillingHelper.mPromotionPurchaseInfo.remove(list.get(i));
                    z2 = true;
                } else {
                    z2 = z;
                }
                i++;
                z = z2;
            }
            InAppBillingHelper.consumeList.clear();
            InAppBillingHelper.FinishCheckPromotion(z);
        }
    }

    static void ConsumePromotionItems() {
        Log.d(TAG, "ConsumePromotionItems requestCount:" + requestCount + " consumeList:" + consumeList.size());
        if (requestCount != 0) {
            return;
        }
        if (consumeList.size() > 0) {
            mHelper.consumeAsync(consumeList, new C12895());
        } else {
            FinishCheckPromotion(false);
        }
    }

    static void FinishCheckPromotion(boolean z) {
        Log.d(TAG, "FinishCheckPromotion success:" + z);
        UnityPlayer.UnitySendMessage("ShopReceiver", "promoteItem", String.valueOf(z));
    }

    public static void checkAndGivePromotionitems(String str) {
        Log.d(TAG, "checkAndGivePromotionitems " + str);
        final String[] split = str.split("----");
        mHelper.queryInventoryAsync(true, new QueryInventoryFinishedListener() {
            public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
                if (iabResult.isFailure()) {
                    InAppBillingHelper.FinishCheckPromotion(false);
                    return;
                }
                List<Purchase> allPurchases = inventory.getAllPurchases();
                if (allPurchases == null || allPurchases.size() <= 0) {
                    Log.d(InAppBillingHelper.TAG, "Promotion List is null or Empty");
                    InAppBillingHelper.FinishCheckPromotion(false);
                } else if (NetworkHelper.getHost() == "") {
                    Log.e(InAppBillingHelper.TAG, "Host is null!!!");
                    InAppBillingHelper.FinishCheckPromotion(false);
                } else {
                    boolean z = false;
                    for (final Purchase purchase : allPurchases) {
                        boolean z2;
                        for (Object equals : split) {
                            if (purchase.getSku().equals(equals)) {
                                z2 = true;
                                break;
                            }
                        }
                        z2 = false;
                        if (z2) {
                            Log.d(InAppBillingHelper.TAG, "checkAndGivePromotionitems Purchase:" + purchase.getSku());
                            InAppBillingHelper.requestCount++;
                            String str = NetworkHelper.getHost() + "/ajax/payments/inappbilling/promotion";
                            List arrayList = new ArrayList();
                            arrayList.add(new BasicNameValuePair("mainToken", InAppBillingHelper.activity.getConfig().getSession().getSid()));
                            arrayList.add(new BasicNameValuePair("signedData", purchase.getOriginalJson()));
                            arrayList.add(new BasicNameValuePair("signature", purchase.getSignature()));
                            arrayList.add(new BasicNameValuePair("iabver", "3"));
                            arrayList.add(new BasicNameValuePair("apv", String.valueOf(InAppBillingHelper.activity.getConfig().getVersionCode())));
                            HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(InAppBillingHelper.activity, str, arrayList);
                            httpPostAsyncTask.setListener(new HttpRequestListener() {
                                public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exception) {
                                    InAppBillingHelper.requestCount--;
                                    Log.e(InAppBillingHelper.TAG, "[PromoCode] Http request error : " + exception.getMessage());
                                    InAppBillingHelper.ConsumePromotionItems();
                                }

                                public void onReceiveResponse(HttpPostAsyncTask httpPostAsyncTask, String str) {
                                    InAppBillingHelper.requestCount--;
                                    try {
                                        JSONObject jSONObject = new JSONObject(str);
                                        Util.dLog(InAppBillingHelper.TAG, "json string: " + jSONObject.toString());
                                        if (jSONObject.getInt("code") == 100) {
                                            InAppBillingHelper.consumeList.add(purchase);
                                        }
                                    } catch (JSONException e) {
                                        Util.dLog(InAppBillingHelper.TAG, "[IABV3] onReceiveResponse json recieve error! " + e.getMessage());
                                        e.printStackTrace();
                                    }
                                    InAppBillingHelper.ConsumePromotionItems();
                                }
                            });
                            httpPostAsyncTask.execute(new Void[0]);
                            z = true;
                        }
                    }
                    if (!z) {
                        InAppBillingHelper.FinishCheckPromotion(false);
                    }
                }
            }
        });
    }

    public static void getProductDatas(String str) throws IabException {
        final List asList = Arrays.asList(str.split("----"));
        if (mHelper.IsSetupDone()) {
            mHelper.queryInventoryAsync(true, asList, new QueryInventoryFinishedListener() {
                public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
                    if (iabResult.isFailure()) {
                        Util.dLog(InAppBillingHelper.TAG, "getProductDatas fail error: " + iabResult.getResponse());
                        UnityPlayer.UnitySendMessage("ShopReceiver", "getProductDatas", "");
                        return;
                    }
                    JSONArray jSONArray = new JSONArray();
                    for (int i = 0; i < asList.size(); i++) {
                        SkuDetails skuDetails = inventory.getSkuDetails((String) asList.get(i));
                        JSONObject jSONObject = new JSONObject();
                        if (skuDetails != null) {
                            try {
                                jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, skuDetails.getSku());
                                jSONObject.put(Param.PRICE, skuDetails.getPrice());
                                jSONObject.put("priceMicros", skuDetails.getPriceMicros());
                                jSONObject.put("name", skuDetails.getTitle());
                                jSONObject.put("desc", skuDetails.getDescription());
                            } catch (JSONException e) {
                                e.printStackTrace();
                            }
                        }
                        jSONArray.put(jSONObject);
                    }
                    JSONObject jSONObject2 = new JSONObject();
                    try {
                        jSONObject2.put("shopList", jSONArray);
                    } catch (JSONException e2) {
                        e2.printStackTrace();
                    }
                    UnityPlayer.UnitySendMessage("ShopReceiver", "getProductDatas", jSONObject2.toString());
                }
            });
        } else {
            UnityPlayer.UnitySendMessage("ShopReceiver", "getProductDatas", "");
        }
    }

    public static String getProductId() {
        productId = NetworkHelper.getSharedString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
        return productId;
    }

    public static void getSkuDetails() throws IabException {
        mHelper.queryInventoryAsync(true, Arrays.asList(AppConsts.itemCodeId), new C12841());
    }

    public static void init(IabHelper iabHelper, StartActivity startActivity) {
        mHelper = iabHelper;
        activity = startActivity;
        Util.dLog(TAG, "  " + (activity == null));
    }

    public static void requestMarket(String str, String str2, String str3) {
        if (mHelper.IsSetupDone()) {
            productName = AppConsts.getProductNameById(str, activity);
            setProductId(str);
            userId = str2;
            userIdHash = str3;
            Util.dLog(TAG, "productName:" + productName + ", productId: " + str);
            activity.inappbillingStart(productId);
            return;
        }
        UnityPlayer.UnitySendMessage("ShopReceiver", "buyItem", String.valueOf(3));
    }

    public static void restorePurchasedItem(boolean z) {
        activity.restoreInventory(z);
    }

    public static void sendInventoryToUnity(Inventory inventory) {
        JSONArray jSONArray = new JSONArray();
        for (String skuDetails : AppConsts.itemCodeId) {
            SkuDetails skuDetails2 = inventory.getSkuDetails(skuDetails);
            JSONObject jSONObject = new JSONObject();
            if (skuDetails2 != null) {
                try {
                    jSONObject.put("sku", skuDetails2.getSku());
                    jSONObject.put(Param.PRICE, skuDetails2.getPrice());
                    jSONObject.put("title", skuDetails2.getTitle());
                    jSONObject.put("description", skuDetails2.getDescription());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
            jSONArray.put(jSONObject);
        }
        JSONObject jSONObject2 = new JSONObject();
        try {
            jSONObject2.put("ItemList", jSONArray);
        } catch (JSONException e2) {
            e2.printStackTrace();
        }
        UnityPlayer.UnitySendMessage("ShopReceiver", "ReceiveItemPriceData", jSONObject2.toString());
    }

    public static void setProductId(String str) {
        productId = str;
        NetworkHelper.setSharedString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
    }

    public static void setProductIdData(String str) {
        AppConsts.itemCodeId = str.split("----");
    }

    public static void setProductNameData(String str) {
        AppConsts.itemNameId = str.split("----");
    }

    public static void trackPurtraceData(final String str, final String str2, final String str3) throws IabException {
        Util.dLog(TAG, "call Track");
        List arrayList = new ArrayList();
        arrayList.add(str);
        mHelper.queryInventoryAsync(true, arrayList, new QueryInventoryFinishedListener() {
            public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
                if (!iabResult.isFailure()) {
                    SkuDetails skuDetails = inventory.getSkuDetails(str);
                    try {
                        JSONObject jSONObject = new JSONObject();
                        jSONObject.put(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID, str);
                        jSONObject.put("purchaseData", str2);
                        jSONObject.put("signature", str3);
                        jSONObject.put(Param.CURRENCY, skuDetails.getCurrency());
                        jSONObject.put(Param.PRICE, skuDetails.getPriceMicros() / 1000000.0d);
                        UnityPlayer.UnitySendMessage("ShopReceiver", "TrackPurchase", jSONObject.toString());
                    } catch (Exception e) {
                        Util.dLog(InAppBillingHelper.TAG, e.toString());
                    }
                }
            }
        });
    }
}
