package p018jp.colopl.drapro;

import android.os.Handler;
import android.util.Log;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import com.unity3d.player.UnityPlayer;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.apache.http.message.BasicNameValuePair;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import p018jp.colopl.iab.IabException;
import p018jp.colopl.iab.IabHelper;
import p018jp.colopl.iab.IabHelper.OnConsumeMultiFinishedListener;
import p018jp.colopl.iab.IabHelper.QueryInventoryFinishedListener;
import p018jp.colopl.iab.IabResult;
import p018jp.colopl.iab.Inventory;
import p018jp.colopl.iab.Purchase;
import p018jp.colopl.iab.SkuDetails;
import p018jp.colopl.network.HttpPostAsyncTask;
import p018jp.colopl.network.HttpRequestListener;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.InAppBillingHelper */
public class InAppBillingHelper {
    private static final String TAG = "InAppBillingHelper";
    public static StartActivity activity;
    static ArrayList<Purchase> consumeList = new ArrayList<>();
    private static Handler handler;
    public static IabHelper mHelper;
    /* access modifiers changed from: private */
    public static ArrayList<Purchase> mPromotionPurchaseInfo = new ArrayList<>();
    public static String paymentReturnUrl;
    private static String productId;
    public static String productName;
    static int requestCount = 0;
    public static String userId;
    public static String userIdHash;

    static void ConsumePromotionItems() {
        Log.d(TAG, "ConsumePromotionItems requestCount:" + requestCount + " consumeList:" + consumeList.size());
        if (requestCount != 0) {
            return;
        }
        if (consumeList.size() > 0) {
            mHelper.consumeAsync((List<Purchase>) consumeList, (OnConsumeMultiFinishedListener) new OnConsumeMultiFinishedListener() {
                public void onConsumeMultiFinished(List<Purchase> list, List<IabResult> list2) {
                    boolean z;
                    boolean z2 = false;
                    int i = 0;
                    while (i < list.size()) {
                        if (((IabResult) list2.get(i)).isSuccess()) {
                            InAppBillingHelper.mPromotionPurchaseInfo.remove(list.get(i));
                            z = true;
                        } else {
                            z = z2;
                        }
                        i++;
                        z2 = z;
                    }
                    InAppBillingHelper.consumeList.clear();
                    InAppBillingHelper.FinishCheckPromotion(z2);
                }
            });
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
                boolean z;
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
                    boolean z2 = false;
                    for (final Purchase purchase : allPurchases) {
                        String[] strArr = split;
                        int length = strArr.length;
                        int i = 0;
                        while (true) {
                            if (i >= length) {
                                z = false;
                                break;
                            }
                            if (purchase.getSku().equals(strArr[i])) {
                                z = true;
                                break;
                            }
                            i++;
                        }
                        if (z) {
                            Log.d(InAppBillingHelper.TAG, "checkAndGivePromotionitems Purchase:" + purchase.getSku());
                            InAppBillingHelper.requestCount++;
                            String str = NetworkHelper.getHost() + "/ajax/payments/inappbilling/promotion";
                            ArrayList arrayList = new ArrayList();
                            arrayList.add(new BasicNameValuePair("mainToken", InAppBillingHelper.activity.getConfig().getSession().getSid()));
                            arrayList.add(new BasicNameValuePair("signedData", purchase.getOriginalJson()));
                            arrayList.add(new BasicNameValuePair("signature", purchase.getSignature()));
                            arrayList.add(new BasicNameValuePair("iabver", "3"));
                            arrayList.add(new BasicNameValuePair("apv", String.valueOf(InAppBillingHelper.activity.getConfig().getVersionCode())));
                            HttpPostAsyncTask httpPostAsyncTask = new HttpPostAsyncTask(InAppBillingHelper.activity, str, arrayList);
                            httpPostAsyncTask.setListener(new HttpRequestListener() {
                                public void onReceiveError(HttpPostAsyncTask httpPostAsyncTask, Exception exc) {
                                    InAppBillingHelper.requestCount--;
                                    Log.e(InAppBillingHelper.TAG, "[PromoCode] Http request error : " + exc.getMessage());
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
                            z2 = true;
                        }
                    }
                    if (!z2) {
                        InAppBillingHelper.FinishCheckPromotion(false);
                    }
                }
            }
        });
    }

    public static void getProductDatas(String str) throws IabException {
        final List asList = Arrays.asList(str.split("----"));
        if (!mHelper.IsSetupDone()) {
            UnityPlayer.UnitySendMessage("ShopReceiver", "getProductDatas", "");
        } else {
            mHelper.queryInventoryAsync(true, asList, new QueryInventoryFinishedListener() {
                public void onQueryInventoryFinished(IabResult iabResult, Inventory inventory) {
                    if (iabResult.isFailure()) {
                        Util.dLog(InAppBillingHelper.TAG, "getProductDatas fail error: " + iabResult.getResponse());
                        UnityPlayer.UnitySendMessage("ShopReceiver", "getProductDatas", "");
                        return;
                    }
                    JSONArray jSONArray = new JSONArray();
                    int i = 0;
                    while (true) {
                        int i2 = i;
                        if (i2 >= asList.size()) {
                            break;
                        }
                        SkuDetails skuDetails = inventory.getSkuDetails((String) asList.get(i2));
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
                        i = i2 + 1;
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
        }
    }

    public static String getProductId() {
        productId = NetworkHelper.getSharedString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
        return productId;
    }

    public static void getSkuDetails() throws IabException {
        mHelper.queryInventoryAsync(true, Arrays.asList(AppConsts.itemCodeId), new QueryInventoryFinishedListener() {
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
        });
    }

    public static void init(IabHelper iabHelper, StartActivity startActivity) {
        mHelper = iabHelper;
        activity = startActivity;
        Util.dLog(TAG, "  " + (activity == null));
    }

    public static void requestMarket(String str, String str2, String str3) {
        if (!mHelper.IsSetupDone()) {
            UnityPlayer.UnitySendMessage("ShopReceiver", "buyItem", String.valueOf(3));
            return;
        }
        productName = AppConsts.getProductNameById(str, activity);
        setProductId(str);
        userId = str2;
        userIdHash = str3;
        Util.dLog(TAG, "productName:" + productName + ", productId: " + str);
        activity.inappbillingStart(productId);
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
        ArrayList arrayList = new ArrayList();
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
