package com.amazon.device.iap.internal.p005b.p008c;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.ProductType;
import com.amazon.venezia.command.SuccessResult;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.util.HashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.c.b */
public final class C0370b extends C0371c {

    /* renamed from: b */
    private static final String f52b = C0370b.class.getSimpleName();

    public C0370b(C0378e eVar, Set<String> set) {
        super(eVar, "1.0", set);
    }

    /* renamed from: a */
    private Product m58a(String str, Map map) throws IllegalArgumentException {
        String str2 = (String) map.get(str);
        try {
            JSONObject jSONObject = new JSONObject(str2);
            ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
            String string = jSONObject.getString("description");
            String optString = jSONObject.optString(Param.PRICE);
            return new ProductBuilder().setSku(str).setProductType(valueOf).setDescription(string).setPrice(optString).setSmallIconUrl(jSONObject.getString("iconUrl")).setTitle(jSONObject.getString("title")).build();
        } catch (JSONException e) {
            throw new IllegalArgumentException("error in parsing json string" + str2);
        }
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws RemoteException, KiwiException {
        Map data = successResult.getData();
        C0409e.m168a(f52b, "data: " + data);
        LinkedHashSet linkedHashSet = new LinkedHashSet();
        HashMap hashMap = new HashMap();
        for (String str : this.f53a) {
            if (!data.containsKey(str)) {
                linkedHashSet.add(str);
            } else {
                try {
                    hashMap.put(str, m58a(str, data));
                } catch (IllegalArgumentException e) {
                    linkedHashSet.add(str);
                    C0409e.m170b(f52b, "Error parsing JSON for SKU " + str + ": " + e.getMessage());
                }
            }
        }
        C0378e b = mo6233b();
        b.mo6221d().mo6227a((Object) new ProductDataResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUnavailableSkus(linkedHashSet).setProductData(hashMap).build());
        return true;
    }
}
