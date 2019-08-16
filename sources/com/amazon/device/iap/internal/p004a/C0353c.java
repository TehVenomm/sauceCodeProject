package com.amazon.device.iap.internal.p004a;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.C0394c;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.util.C0406b;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.device.iap.model.RequestId;
import com.amazon.device.iap.model.UserData;
import com.amazon.device.iap.model.UserDataResponse;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.drive.DriveFile;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.math.BigDecimal;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.Currency;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.a.c */
public final class C0353c implements C0394c {
    /* access modifiers changed from: private */

    /* renamed from: a */
    public static final String f32a = C0353c.class.getSimpleName();

    /* renamed from: a */
    private Product m17a(String str, JSONObject jSONObject) throws JSONException {
        ProductType valueOf = ProductType.valueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
        JSONObject jSONObject2 = jSONObject.getJSONObject("priceJson");
        Currency instance = Currency.getInstance(jSONObject2.optString(Param.CURRENCY));
        String str2 = instance.getSymbol() + new BigDecimal(jSONObject2.optString("value"));
        String optString = jSONObject.optString("title");
        String optString2 = jSONObject.optString("description");
        return new ProductBuilder().setSku(str).setProductType(valueOf).setDescription(optString2).setPrice(str2).setSmallIconUrl(jSONObject.optString("smallIconUrl")).setTitle(optString).build();
    }

    /* renamed from: a */
    private Receipt m18a(JSONObject jSONObject) throws ParseException {
        String optString = jSONObject.optString("receiptId");
        String optString2 = jSONObject.optString("sku");
        ProductType valueOf = ProductType.valueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
        Date parse = C0352b.f31a.parse(jSONObject.optString("purchaseDate"));
        String optString3 = jSONObject.optString("cancelDate");
        return new ReceiptBuilder().setReceiptId(optString).setSku(optString2).setProductType(valueOf).setPurchaseDate(parse).setCancelDate((optString3 == null || optString3.length() == 0) ? null : C0352b.f31a.parse(optString3)).build();
    }

    /* renamed from: a */
    private void m20a(Intent intent) throws JSONException {
        PurchaseUpdatesResponse b = m23b(intent);
        if (b.getRequestStatus() == RequestStatus.SUCCESSFUL) {
            String optString = new JSONObject(intent.getStringExtra("purchaseUpdatesOutput")).optString("offset");
            Log.i(f32a, "Offset for PurchaseUpdatesResponse:" + optString);
            C0406b.m162a(b.getUserData().getUserId(), optString);
        }
        mo6202a((Object) b);
    }

    /* renamed from: a */
    private void m21a(String str) {
        try {
            Context b = C0401d.m137d().mo6264b();
            Bundle bundle = new Bundle();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("requestId", str);
            jSONObject.put("packageName", b.getPackageName());
            jSONObject.put("sdkVersion", PurchasingService.SDK_VERSION);
            bundle.putString("userInput", jSONObject.toString());
            Intent intent = new Intent("com.amazon.testclient.iap.appUserId");
            intent.addFlags(DriveFile.MODE_READ_ONLY);
            intent.putExtras(bundle);
            b.startService(intent);
        } catch (JSONException e) {
            C0409e.m170b(f32a, "Error in sendGetUserDataRequest.");
        }
    }

    /* renamed from: a */
    private void m22a(String str, String str2) {
        try {
            Context b = C0401d.m137d().mo6264b();
            boolean equals = AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(str.substring("GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX".length() + 1, "GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX".length() + 2));
            String a = C0406b.m161a(str2);
            Log.i(f32a, "send PurchaseUpdates with user id:" + str2 + ";reset flag:" + equals + ", local cursor:" + a + ", parsed from old requestId:" + str);
            RequestId requestId = new RequestId();
            Bundle bundle = new Bundle();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("requestId", requestId.toString());
            if (equals) {
                a = null;
            }
            jSONObject.put("offset", a);
            jSONObject.put("sdkVersion", PurchasingService.SDK_VERSION);
            jSONObject.put("packageName", b.getPackageName());
            bundle.putString("purchaseUpdatesInput", jSONObject.toString());
            Intent intent = new Intent("com.amazon.testclient.iap.purchaseUpdates");
            intent.addFlags(DriveFile.MODE_READ_ONLY);
            intent.putExtras(bundle);
            b.startService(intent);
        } catch (JSONException e) {
            C0409e.m170b(f32a, "Error in sendPurchaseUpdatesRequest.");
        }
    }

    /* renamed from: b */
    private PurchaseUpdatesResponse m23b(Intent intent) {
        Exception exc;
        RequestId requestId;
        UserData userData;
        boolean z;
        List list;
        List list2;
        RequestStatus requestStatus = RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("purchaseUpdatesOutput"));
            requestId = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                requestStatus = RequestStatus.valueOf(jSONObject.optString("status"));
                z = jSONObject.optBoolean("isMore");
                try {
                    String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                    userData = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                    try {
                        if (requestStatus == RequestStatus.SUCCESSFUL) {
                            ArrayList arrayList = new ArrayList();
                            try {
                                JSONArray optJSONArray = jSONObject.optJSONArray("receipts");
                                if (optJSONArray != null) {
                                    for (int i = 0; i < optJSONArray.length(); i++) {
                                        JSONObject optJSONObject = optJSONArray.optJSONObject(i);
                                        try {
                                            arrayList.add(m18a(optJSONObject));
                                        } catch (Exception e) {
                                            Log.e(f32a, "Failed to parse receipt from json:" + optJSONObject);
                                        }
                                    }
                                }
                                list2 = arrayList;
                            } catch (Exception e2) {
                                exc = e2;
                                list = arrayList;
                            }
                        } else {
                            list2 = null;
                        }
                    } catch (Exception e3) {
                        exc = e3;
                        list = null;
                        Log.e(f32a, "Error parsing purchase updates output", exc);
                        list2 = list;
                        return new PurchaseUpdatesResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipts(list2).setHasMore(z).build();
                    }
                } catch (Exception e4) {
                    exc = e4;
                    userData = null;
                    list = null;
                }
            } catch (Exception e5) {
                exc = e5;
                userData = null;
                z = false;
                list = null;
                Log.e(f32a, "Error parsing purchase updates output", exc);
                list2 = list;
                return new PurchaseUpdatesResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipts(list2).setHasMore(z).build();
            }
        } catch (Exception e6) {
            exc = e6;
            requestId = null;
            userData = null;
            z = false;
            list = null;
            Log.e(f32a, "Error parsing purchase updates output", exc);
            list2 = list;
            return new PurchaseUpdatesResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipts(list2).setHasMore(z).build();
        }
        return new PurchaseUpdatesResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipts(list2).setHasMore(z).build();
    }

    /* renamed from: c */
    private void m24c(Intent intent) {
        mo6202a((Object) m25d(intent));
    }

    /* renamed from: d */
    private ProductDataResponse m25d(Intent intent) {
        Map map;
        Exception exc;
        LinkedHashSet linkedHashSet;
        RequestId requestId;
        Map map2;
        ProductDataResponse.RequestStatus requestStatus = ProductDataResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("itemDataOutput"));
            requestId = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                requestStatus = ProductDataResponse.RequestStatus.valueOf(jSONObject.optString("status"));
                if (requestStatus != ProductDataResponse.RequestStatus.FAILED) {
                    linkedHashSet = new LinkedHashSet();
                    try {
                        map2 = new HashMap();
                    } catch (Exception e) {
                        map = null;
                        exc = e;
                        Log.e(f32a, "Error parsing item data output", exc);
                        map2 = map;
                        return new ProductDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setProductData(map2).setUnavailableSkus(linkedHashSet).build();
                    }
                    try {
                        JSONArray optJSONArray = jSONObject.optJSONArray("unavailableSkus");
                        if (optJSONArray != null) {
                            for (int i = 0; i < optJSONArray.length(); i++) {
                                linkedHashSet.add(optJSONArray.getString(i));
                            }
                        }
                        JSONObject optJSONObject = jSONObject.optJSONObject("items");
                        if (optJSONObject != null) {
                            Iterator keys = optJSONObject.keys();
                            while (keys.hasNext()) {
                                String str = (String) keys.next();
                                map2.put(str, m17a(str, optJSONObject.optJSONObject(str)));
                            }
                        }
                    } catch (Exception e2) {
                        map = map2;
                        exc = e2;
                        Log.e(f32a, "Error parsing item data output", exc);
                        map2 = map;
                        return new ProductDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setProductData(map2).setUnavailableSkus(linkedHashSet).build();
                    }
                } else {
                    linkedHashSet = null;
                    map2 = null;
                }
            } catch (Exception e3) {
                map = null;
                exc = e3;
                linkedHashSet = null;
                Log.e(f32a, "Error parsing item data output", exc);
                map2 = map;
                return new ProductDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setProductData(map2).setUnavailableSkus(linkedHashSet).build();
            }
        } catch (Exception e4) {
            map = null;
            exc = e4;
            linkedHashSet = null;
            requestId = null;
            Log.e(f32a, "Error parsing item data output", exc);
            map2 = map;
            return new ProductDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setProductData(map2).setUnavailableSkus(linkedHashSet).build();
        }
        return new ProductDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setProductData(map2).setUnavailableSkus(linkedHashSet).build();
    }

    /* renamed from: e */
    private void m26e(Intent intent) {
        UserDataResponse f = m27f(intent);
        if (f.getRequestId() == null || !f.getRequestId().toString().startsWith("GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX")) {
            mo6202a((Object) f);
        } else if (f.getUserData() == null || C0408d.m167a(f.getUserData().getUserId())) {
            Log.e(f32a, "No Userid found in userDataResponse" + f);
            mo6202a((Object) new PurchaseUpdatesResponseBuilder().setRequestId(f.getRequestId()).setRequestStatus(RequestStatus.FAILED).setUserData(f.getUserData()).setReceipts(new ArrayList()).setHasMore(false).build());
        } else {
            Log.i(f32a, "sendGetPurchaseUpdates with user id" + f.getUserData().getUserId());
            m22a(f.getRequestId().toString(), f.getUserData().getUserId());
        }
    }

    /* renamed from: f */
    private UserDataResponse m27f(Intent intent) {
        RequestId requestId;
        UserData userData = null;
        UserDataResponse.RequestStatus requestStatus = UserDataResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("userOutput"));
            requestId = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                requestStatus = UserDataResponse.RequestStatus.valueOf(jSONObject.optString("status"));
                try {
                    if (requestStatus == UserDataResponse.RequestStatus.SUCCESSFUL) {
                        String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                        userData = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                    }
                } catch (Exception e) {
                    e = e;
                    Log.e(f32a, "Error parsing userid output", e);
                    return new UserDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).build();
                }
            } catch (Exception e2) {
                e = e2;
            }
        } catch (Exception e3) {
            e = e3;
            requestId = null;
            Log.e(f32a, "Error parsing userid output", e);
            return new UserDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).build();
        }
        return new UserDataResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).build();
    }

    /* renamed from: g */
    private void m28g(Intent intent) {
        mo6202a((Object) m29h(intent));
    }

    /* renamed from: h */
    private PurchaseResponse m29h(Intent intent) {
        RequestId requestId;
        UserData userData;
        Receipt receipt = null;
        PurchaseResponse.RequestStatus requestStatus = PurchaseResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("purchaseOutput"));
            requestId = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                userData = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                try {
                    requestStatus = PurchaseResponse.RequestStatus.safeValueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PURCHASE_STATUS));
                } catch (Exception e) {
                    e = e;
                    Log.e(f32a, "Error parsing purchase output", e);
                    return new PurchaseResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipt(receipt).build();
                }
            } catch (Exception e2) {
                e = e2;
                userData = null;
                Log.e(f32a, "Error parsing purchase output", e);
                return new PurchaseResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipt(receipt).build();
            }
            try {
                JSONObject optJSONObject = jSONObject.optJSONObject("receipt");
                if (optJSONObject != null) {
                    receipt = m18a(optJSONObject);
                }
            } catch (Exception e3) {
                e = e3;
                Log.e(f32a, "Error parsing purchase output", e);
                return new PurchaseResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipt(receipt).build();
            }
        } catch (Exception e4) {
            e = e4;
            requestId = null;
            userData = null;
            Log.e(f32a, "Error parsing purchase output", e);
            return new PurchaseResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipt(receipt).build();
        }
        return new PurchaseResponseBuilder().setRequestId(requestId).setRequestStatus(requestStatus).setUserData(userData).setReceipt(receipt).build();
    }

    /* renamed from: a */
    public void mo6196a(Context context, Intent intent) {
        C0409e.m168a(f32a, "handleResponse");
        try {
            String string = intent.getExtras().getString("responseType");
            if (string.equalsIgnoreCase("com.amazon.testclient.iap.purchase")) {
                m28g(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.appUserId")) {
                m26e(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.itemData")) {
                m24c(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.purchaseUpdates")) {
                m20a(intent);
            }
        } catch (Exception e) {
            Log.e(f32a, "Error handling response.", e);
        }
    }

    /* renamed from: a */
    public void mo6197a(RequestId requestId) {
        C0409e.m168a(f32a, "sendGetUserDataRequest");
        m21a(requestId.toString());
    }

    /* renamed from: a */
    public void mo6198a(RequestId requestId, String str) {
        C0409e.m168a(f32a, "sendPurchaseRequest");
        try {
            Context b = C0401d.m137d().mo6264b();
            Bundle bundle = new Bundle();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("sku", str);
            jSONObject.put("requestId", requestId.toString());
            jSONObject.put("packageName", b.getPackageName());
            jSONObject.put("sdkVersion", PurchasingService.SDK_VERSION);
            bundle.putString("purchaseInput", jSONObject.toString());
            Intent intent = new Intent("com.amazon.testclient.iap.purchase");
            intent.addFlags(DriveFile.MODE_READ_ONLY);
            intent.putExtras(bundle);
            b.startService(intent);
        } catch (JSONException e) {
            C0409e.m170b(f32a, "Error in sendPurchaseRequest.");
        }
    }

    /* renamed from: a */
    public void mo6199a(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        C0409e.m168a(f32a, "sendNotifyPurchaseFulfilled");
        try {
            Context b = C0401d.m137d().mo6264b();
            Bundle bundle = new Bundle();
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("requestId", requestId.toString());
            jSONObject.put("packageName", b.getPackageName());
            jSONObject.put("receiptId", str);
            jSONObject.put("fulfillmentResult", fulfillmentResult);
            jSONObject.put("sdkVersion", PurchasingService.SDK_VERSION);
            bundle.putString("purchaseFulfilledInput", jSONObject.toString());
            Intent intent = new Intent("com.amazon.testclient.iap.purchaseFulfilled");
            intent.addFlags(DriveFile.MODE_READ_ONLY);
            intent.putExtras(bundle);
            b.startService(intent);
        } catch (JSONException e) {
            C0409e.m170b(f32a, "Error in sendNotifyPurchaseFulfilled.");
        }
    }

    /* renamed from: a */
    public void mo6200a(RequestId requestId, Set<String> set) {
        C0409e.m168a(f32a, "sendItemDataRequest");
        try {
            Context b = C0401d.m137d().mo6264b();
            Bundle bundle = new Bundle();
            JSONObject jSONObject = new JSONObject();
            JSONArray jSONArray = new JSONArray(set);
            jSONObject.put("requestId", requestId.toString());
            jSONObject.put("packageName", b.getPackageName());
            jSONObject.put("skus", jSONArray);
            jSONObject.put("sdkVersion", PurchasingService.SDK_VERSION);
            bundle.putString("itemDataInput", jSONObject.toString());
            Intent intent = new Intent("com.amazon.testclient.iap.itemData");
            intent.addFlags(DriveFile.MODE_READ_ONLY);
            intent.putExtras(bundle);
            b.startService(intent);
        } catch (JSONException e) {
            C0409e.m170b(f32a, "Error in sendItemDataRequest.");
        }
    }

    /* renamed from: a */
    public void mo6201a(RequestId requestId, boolean z) {
        String str = "GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX:" + (z ? 1 : 0) + ":" + new RequestId().toString();
        C0409e.m168a(f32a, "sendPurchaseUpdatesRequest/sendGetUserData first:" + str);
        m21a(str);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public void mo6202a(final Object obj) {
        C0408d.m164a(obj, "response");
        Context b = C0401d.m137d().mo6264b();
        final PurchasingListener a = C0401d.m137d().mo6257a();
        if (b == null || a == null) {
            C0409e.m168a(f32a, "PurchasingListener is not set. Dropping response: " + obj);
            return;
        }
        new Handler(b.getMainLooper()).post(new Runnable() {
            public void run() {
                try {
                    if (obj instanceof ProductDataResponse) {
                        a.onProductDataResponse((ProductDataResponse) obj);
                    } else if (obj instanceof UserDataResponse) {
                        a.onUserDataResponse((UserDataResponse) obj);
                    } else if (obj instanceof PurchaseUpdatesResponse) {
                        a.onPurchaseUpdatesResponse((PurchaseUpdatesResponse) obj);
                    } else if (obj instanceof PurchaseResponse) {
                        a.onPurchaseResponse((PurchaseResponse) obj);
                    } else {
                        C0409e.m170b(C0353c.f32a, "Unknown response type:" + obj.getClass().getName());
                    }
                } catch (Exception e) {
                    C0409e.m170b(C0353c.f32a, "Error in sendResponse: " + e);
                }
            }
        });
    }
}
