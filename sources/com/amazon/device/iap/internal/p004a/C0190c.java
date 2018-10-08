package com.amazon.device.iap.internal.p004a;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import com.amazon.device.iap.PurchasingListener;
import com.amazon.device.iap.PurchasingService;
import com.amazon.device.iap.internal.C0189c;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.util.C0241b;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
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
public final class C0190c implements C0189c {
    /* renamed from: a */
    private static final String f16a = C0190c.class.getSimpleName();

    /* renamed from: a */
    private Product m31a(String str, JSONObject jSONObject) throws JSONException {
        ProductType valueOf = ProductType.valueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
        JSONObject jSONObject2 = jSONObject.getJSONObject("priceJson");
        Currency instance = Currency.getInstance(jSONObject2.optString(Param.CURRENCY));
        String str2 = instance.getSymbol() + new BigDecimal(jSONObject2.optString(Param.VALUE));
        String optString = jSONObject.optString("title");
        String optString2 = jSONObject.optString("description");
        return new ProductBuilder().setSku(str).setProductType(valueOf).setDescription(optString2).setPrice(str2).setSmallIconUrl(jSONObject.optString("smallIconUrl")).setTitle(optString).build();
    }

    /* renamed from: a */
    private Receipt m32a(JSONObject jSONObject) throws ParseException {
        String optString = jSONObject.optString("receiptId");
        String optString2 = jSONObject.optString("sku");
        ProductType valueOf = ProductType.valueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
        Date parse = C0187b.f12a.parse(jSONObject.optString("purchaseDate"));
        String optString3 = jSONObject.optString("cancelDate");
        Date parse2 = (optString3 == null || optString3.length() == 0) ? null : C0187b.f12a.parse(optString3);
        return new ReceiptBuilder().setReceiptId(optString).setSku(optString2).setProductType(valueOf).setPurchaseDate(parse).setCancelDate(parse2).build();
    }

    /* renamed from: a */
    private void m34a(Intent intent) throws JSONException {
        Object b = m37b(intent);
        if (b.getRequestStatus() == RequestStatus.SUCCESSFUL) {
            String optString = new JSONObject(intent.getStringExtra("purchaseUpdatesOutput")).optString("offset");
            Log.i(f16a, "Offset for PurchaseUpdatesResponse:" + optString);
            C0241b.m167a(b.getUserData().getUserId(), optString);
        }
        m50a(b);
    }

    /* renamed from: a */
    private void m35a(String str) {
        try {
            Context b = C0236d.m142d().m151b();
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
            C0244e.m175b(f16a, "Error in sendGetUserDataRequest.");
        }
    }

    /* renamed from: a */
    private void m36a(String str, String str2) {
        try {
            Context b = C0236d.m142d().m151b();
            boolean equals = AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(str.substring("GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX".length() + 1, "GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX".length() + 2));
            Object a = C0241b.m166a(str2);
            Log.i(f16a, "send PurchaseUpdates with user id:" + str2 + ";reset flag:" + equals + ", local cursor:" + a + ", parsed from old requestId:" + str);
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
            C0244e.m175b(f16a, "Error in sendPurchaseUpdatesRequest.");
        }
    }

    /* renamed from: b */
    private PurchaseUpdatesResponse m37b(Intent intent) {
        RequestId fromString;
        RequestStatus valueOf;
        Throwable e;
        List list;
        RequestStatus requestStatus;
        Throwable th;
        boolean z;
        RequestStatus requestStatus2;
        UserData userData;
        boolean z2;
        List list2;
        UserData userData2;
        int i = 0;
        RequestId requestId = null;
        RequestStatus requestStatus3 = RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("purchaseUpdatesOutput"));
            fromString = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                valueOf = RequestStatus.valueOf(jSONObject.optString("status"));
            } catch (Exception e2) {
                e = e2;
                list = null;
                requestStatus = requestStatus3;
                th = e;
                z = false;
                requestStatus2 = requestStatus;
                requestId = fromString;
                userData = null;
                Log.e(f16a, "Error parsing purchase updates output", th);
                z2 = z;
                valueOf = requestStatus2;
                list2 = list;
                userData2 = userData;
                fromString = requestId;
                return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
            }
            try {
                z2 = jSONObject.optBoolean("isMore");
                try {
                    String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                    userData2 = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                } catch (Throwable e3) {
                    th = e3;
                    list = null;
                    requestStatus2 = valueOf;
                    z = z2;
                    requestId = fromString;
                    userData = null;
                    Log.e(f16a, "Error parsing purchase updates output", th);
                    z2 = z;
                    valueOf = requestStatus2;
                    list2 = list;
                    userData2 = userData;
                    fromString = requestId;
                    return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
                }
                try {
                    if (valueOf == RequestStatus.SUCCESSFUL) {
                        List arrayList = new ArrayList();
                        try {
                            JSONArray optJSONArray = jSONObject.optJSONArray("receipts");
                            if (optJSONArray != null) {
                                while (i < optJSONArray.length()) {
                                    jSONObject = optJSONArray.optJSONObject(i);
                                    try {
                                        arrayList.add(m32a(jSONObject));
                                    } catch (Exception e4) {
                                        Log.e(f16a, "Failed to parse receipt from json:" + jSONObject);
                                    }
                                    i++;
                                }
                            }
                            list2 = arrayList;
                        } catch (Throwable e32) {
                            requestId = fromString;
                            userData = userData2;
                            list = arrayList;
                            th = e32;
                            requestStatus2 = valueOf;
                            z = z2;
                        }
                    } else {
                        list2 = null;
                    }
                } catch (Throwable e322) {
                    th = e322;
                    requestStatus2 = valueOf;
                    z = z2;
                    UserData userData3 = userData2;
                    list = null;
                    requestId = fromString;
                    userData = userData3;
                    Log.e(f16a, "Error parsing purchase updates output", th);
                    z2 = z;
                    valueOf = requestStatus2;
                    list2 = list;
                    userData2 = userData;
                    fromString = requestId;
                    return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
                }
            } catch (Throwable th2) {
                Throwable th3 = th2;
                requestStatus3 = valueOf;
                e = th3;
                list = null;
                requestStatus = requestStatus3;
                th2 = e;
                z = false;
                requestStatus2 = requestStatus;
                requestId = fromString;
                userData = null;
                Log.e(f16a, "Error parsing purchase updates output", th2);
                z2 = z;
                valueOf = requestStatus2;
                list2 = list;
                userData2 = userData;
                fromString = requestId;
                return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
            }
        } catch (Throwable e5) {
            z = false;
            list = null;
            requestStatus2 = requestStatus3;
            th2 = e5;
            userData = null;
            Log.e(f16a, "Error parsing purchase updates output", th2);
            z2 = z;
            valueOf = requestStatus2;
            list2 = list;
            userData2 = userData;
            fromString = requestId;
            return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
        }
        return new PurchaseUpdatesResponseBuilder().setRequestId(fromString).setRequestStatus(valueOf).setUserData(userData2).setReceipts(list2).setHasMore(z2).build();
    }

    /* renamed from: c */
    private void m38c(Intent intent) {
        m50a(m39d(intent));
    }

    /* renamed from: d */
    private ProductDataResponse m39d(Intent intent) {
        RequestId fromString;
        ProductDataResponse.RequestStatus valueOf;
        Map hashMap;
        RequestId requestId;
        Throwable th;
        Throwable e;
        Set set;
        Map map;
        RequestId requestId2;
        Throwable th2;
        Set set2 = null;
        ProductDataResponse.RequestStatus requestStatus = ProductDataResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("itemDataOutput"));
            fromString = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                valueOf = ProductDataResponse.RequestStatus.valueOf(jSONObject.optString("status"));
                try {
                    if (valueOf != ProductDataResponse.RequestStatus.FAILED) {
                        Set linkedHashSet = new LinkedHashSet();
                        try {
                            hashMap = new HashMap();
                        } catch (Throwable e2) {
                            requestId = fromString;
                            th = e2;
                            requestStatus = valueOf;
                            set = linkedHashSet;
                            map = null;
                            requestId2 = requestId;
                            Log.e(f16a, "Error parsing item data output", th);
                            fromString = requestId2;
                            return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
                        }
                        try {
                            JSONArray optJSONArray = jSONObject.optJSONArray("unavailableSkus");
                            if (optJSONArray != null) {
                                for (int i = 0; i < optJSONArray.length(); i++) {
                                    linkedHashSet.add(optJSONArray.getString(i));
                                }
                            }
                            jSONObject = jSONObject.optJSONObject("items");
                            if (jSONObject != null) {
                                Iterator keys = jSONObject.keys();
                                while (keys.hasNext()) {
                                    String str = (String) keys.next();
                                    hashMap.put(str, m31a(str, jSONObject.optJSONObject(str)));
                                }
                            }
                            set2 = linkedHashSet;
                            map = hashMap;
                        } catch (Throwable e3) {
                            requestId = fromString;
                            th = e3;
                            requestId2 = requestId;
                            ProductDataResponse.RequestStatus requestStatus2 = valueOf;
                            set = linkedHashSet;
                            map = hashMap;
                            requestStatus = requestStatus2;
                            Log.e(f16a, "Error parsing item data output", th);
                            fromString = requestId2;
                            return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
                        }
                    }
                    map = null;
                    requestStatus = valueOf;
                    set = set2;
                } catch (Exception e4) {
                    e2 = e4;
                    map = null;
                    th2 = e2;
                    requestStatus = valueOf;
                    set = null;
                    requestId2 = fromString;
                    th = th2;
                    Log.e(f16a, "Error parsing item data output", th);
                    fromString = requestId2;
                    return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
                }
            } catch (Throwable e5) {
                th2 = e5;
                valueOf = requestStatus;
                e2 = th2;
                map = null;
                th2 = e2;
                requestStatus = valueOf;
                set = null;
                requestId2 = fromString;
                th = th2;
                Log.e(f16a, "Error parsing item data output", th);
                fromString = requestId2;
                return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
            }
        } catch (Exception e6) {
            th = e6;
            set = null;
            map = null;
            Log.e(f16a, "Error parsing item data output", th);
            fromString = requestId2;
            return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
        }
        return new ProductDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setProductData(map).setUnavailableSkus(set).build();
    }

    /* renamed from: e */
    private void m40e(Intent intent) {
        Object f = m41f(intent);
        if (f.getRequestId() == null || !f.getRequestId().toString().startsWith("GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX")) {
            m50a(f);
        } else if (f.getUserData() == null || C0243d.m172a(f.getUserData().getUserId())) {
            Log.e(f16a, "No Userid found in userDataResponse" + f);
            m50a(new PurchaseUpdatesResponseBuilder().setRequestId(f.getRequestId()).setRequestStatus(RequestStatus.FAILED).setUserData(f.getUserData()).setReceipts(new ArrayList()).setHasMore(false).build());
        } else {
            Log.i(f16a, "sendGetPurchaseUpdates with user id" + f.getUserData().getUserId());
            m36a(f.getRequestId().toString(), f.getUserData().getUserId());
        }
    }

    /* renamed from: f */
    private UserDataResponse m41f(Intent intent) {
        RequestId fromString;
        Throwable e;
        UserData userData = null;
        UserDataResponse.RequestStatus requestStatus = UserDataResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("userOutput"));
            fromString = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                requestStatus = UserDataResponse.RequestStatus.valueOf(jSONObject.optString("status"));
                if (requestStatus == UserDataResponse.RequestStatus.SUCCESSFUL) {
                    String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                    userData = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                }
            } catch (Throwable e2) {
                Throwable th = e2;
                requestStatus = requestStatus;
                e2 = th;
                Log.e(f16a, "Error parsing userid output", e2);
                return new UserDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(userData).build();
            }
        } catch (Throwable e3) {
            e2 = e3;
            fromString = null;
            Log.e(f16a, "Error parsing userid output", e2);
            return new UserDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(userData).build();
        }
        return new UserDataResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(userData).build();
    }

    /* renamed from: g */
    private void m42g(Intent intent) {
        m50a(m43h(intent));
    }

    /* renamed from: h */
    private PurchaseResponse m43h(Intent intent) {
        RequestId fromString;
        UserData build;
        Throwable e;
        Receipt receipt = null;
        PurchaseResponse.RequestStatus requestStatus = PurchaseResponse.RequestStatus.FAILED;
        try {
            JSONObject jSONObject = new JSONObject(intent.getStringExtra("purchaseOutput"));
            fromString = RequestId.fromString(jSONObject.optString("requestId"));
            try {
                String optString = jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
                build = new UserDataBuilder().setUserId(optString).setMarketplace(jSONObject.optString("marketplace")).build();
                try {
                    requestStatus = PurchaseResponse.RequestStatus.safeValueOf(jSONObject.optString(AmazonAppstoreBillingService.JSON_KEY_PURCHASE_STATUS));
                    jSONObject = jSONObject.optJSONObject("receipt");
                    if (jSONObject != null) {
                        receipt = m32a(jSONObject);
                    }
                } catch (Throwable e2) {
                    Throwable th = e2;
                    requestStatus = requestStatus;
                    e2 = th;
                    Log.e(f16a, "Error parsing purchase output", e2);
                    return new PurchaseResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(build).setReceipt(receipt).build();
                }
            } catch (Throwable e3) {
                e2 = e3;
                build = null;
                Log.e(f16a, "Error parsing purchase output", e2);
                return new PurchaseResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(build).setReceipt(receipt).build();
            }
        } catch (Throwable e4) {
            e2 = e4;
            build = null;
            fromString = null;
            Log.e(f16a, "Error parsing purchase output", e2);
            return new PurchaseResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(build).setReceipt(receipt).build();
        }
        return new PurchaseResponseBuilder().setRequestId(fromString).setRequestStatus(requestStatus).setUserData(build).setReceipt(receipt).build();
    }

    /* renamed from: a */
    public void mo1180a(Context context, Intent intent) {
        C0244e.m173a(f16a, "handleResponse");
        try {
            String string = intent.getExtras().getString("responseType");
            if (string.equalsIgnoreCase("com.amazon.testclient.iap.purchase")) {
                m42g(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.appUserId")) {
                m40e(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.itemData")) {
                m38c(intent);
            } else if (string.equalsIgnoreCase("com.amazon.testclient.iap.purchaseUpdates")) {
                m34a(intent);
            }
        } catch (Throwable e) {
            Log.e(f16a, "Error handling response.", e);
        }
    }

    /* renamed from: a */
    public void mo1181a(RequestId requestId) {
        C0244e.m173a(f16a, "sendGetUserDataRequest");
        m35a(requestId.toString());
    }

    /* renamed from: a */
    public void mo1182a(RequestId requestId, String str) {
        C0244e.m173a(f16a, "sendPurchaseRequest");
        try {
            Context b = C0236d.m142d().m151b();
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
            C0244e.m175b(f16a, "Error in sendPurchaseRequest.");
        }
    }

    /* renamed from: a */
    public void mo1183a(RequestId requestId, String str, FulfillmentResult fulfillmentResult) {
        C0244e.m173a(f16a, "sendNotifyPurchaseFulfilled");
        try {
            Context b = C0236d.m142d().m151b();
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
            C0244e.m175b(f16a, "Error in sendNotifyPurchaseFulfilled.");
        }
    }

    /* renamed from: a */
    public void mo1184a(RequestId requestId, Set<String> set) {
        C0244e.m173a(f16a, "sendItemDataRequest");
        try {
            Context b = C0236d.m142d().m151b();
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
            C0244e.m175b(f16a, "Error in sendItemDataRequest.");
        }
    }

    /* renamed from: a */
    public void mo1185a(RequestId requestId, boolean z) {
        String str = "GET_USER_ID_FOR_PURCHASE_UPDATES_PREFIX:" + (z ? 1 : 0) + ":" + new RequestId().toString();
        C0244e.m173a(f16a, "sendPurchaseUpdatesRequest/sendGetUserData first:" + str);
        m35a(str);
    }

    /* renamed from: a */
    protected void m50a(final Object obj) {
        C0243d.m169a(obj, "response");
        Context b = C0236d.m142d().m151b();
        final PurchasingListener a = C0236d.m142d().m144a();
        if (b == null || a == null) {
            C0244e.m173a(f16a, "PurchasingListener is not set. Dropping response: " + obj);
            return;
        }
        new Handler(b.getMainLooper()).post(new Runnable(this) {
            /* renamed from: c */
            final /* synthetic */ C0190c f15c;

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
                        C0244e.m175b(C0190c.f16a, "Unknown response type:" + obj.getClass().getName());
                    }
                } catch (Exception e) {
                    C0244e.m175b(C0190c.f16a, "Error in sendResponse: " + e);
                }
            }
        });
    }
}
