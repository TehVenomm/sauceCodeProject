package com.amazon.device.iap.internal.p001b.p004c;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse.RequestStatus;
import com.amazon.device.iap.model.ProductType;
import com.amazon.venezia.command.SuccessResult;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.math.BigDecimal;
import java.util.Currency;
import java.util.HashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.c.a */
public final class C0207a extends C0206c {
    /* renamed from: b */
    private static final String f47b = C0207a.class.getSimpleName();

    public C0207a(C0197e c0197e, Set<String> set) {
        super(c0197e, "2.0", set);
    }

    /* renamed from: a */
    private Product m82a(String str, Map map) throws IllegalArgumentException {
        String str2 = (String) map.get(str);
        try {
            JSONObject jSONObject = new JSONObject(str2);
            ProductType valueOf = ProductType.valueOf(jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE).toUpperCase());
            String string = jSONObject.getString("description");
            String optString = jSONObject.optString(Param.PRICE, null);
            if (C0243d.m172a(optString)) {
                JSONObject optJSONObject = jSONObject.optJSONObject("priceJson");
                if (optJSONObject != null) {
                    Currency instance = Currency.getInstance(optJSONObject.getString(Param.CURRENCY));
                    optString = instance.getSymbol() + new BigDecimal(optJSONObject.getString(Param.VALUE));
                }
            }
            return new ProductBuilder().setSku(str).setProductType(valueOf).setDescription(string).setPrice(optString).setSmallIconUrl(jSONObject.getString("iconUrl")).setTitle(jSONObject.getString("title")).build();
        } catch (JSONException e) {
            throw new IllegalArgumentException("error in parsing json string" + str2);
        }
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws RemoteException, KiwiException {
        Map data = successResult.getData();
        C0244e.m173a(f47b, "data: " + data);
        Set linkedHashSet = new LinkedHashSet();
        Map hashMap = new HashMap();
        for (String str : this.a) {
            if (data.containsKey(str)) {
                try {
                    hashMap.put(str, m82a(str, data));
                } catch (IllegalArgumentException e) {
                    IllegalArgumentException illegalArgumentException = e;
                    linkedHashSet.add(str);
                    String str2 = (String) data.get(str);
                    C0244e.m175b(f47b, "Error parsing JSON for SKU " + str + ": " + illegalArgumentException.getMessage());
                }
            } else {
                linkedHashSet.add(str);
            }
        }
        C0197e b = m58b();
        b.m73d().m117a(new ProductDataResponseBuilder().setRequestId(b.m72c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUnavailableSkus(linkedHashSet).setProductData(hashMap).build());
        return true;
    }
}
