package com.amazon.device.iap.internal.p005b.p006a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p014c.C0395a;
import com.amazon.device.iap.internal.p014c.C0397b;
import com.amazon.device.iap.internal.util.C0404a;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.a.b */
public final class C0359b extends C0360c {

    /* renamed from: a */
    private static final String f40a = C0359b.class.getSimpleName();

    public C0359b(C0378e eVar) {
        super(eVar, "1.0");
    }

    /* renamed from: a */
    private void m41a(String str, String str2, String str3) {
        if (str != null && str2 != null && str3 != null) {
            try {
                JSONObject jSONObject = new JSONObject(str3);
                if (RequestStatus.safeValueOf(jSONObject.getString("orderStatus")) == RequestStatus.SUCCESSFUL) {
                    C0395a.m115a().mo6245a(str, str2, C0404a.m154a(jSONObject, str2, str).getReceiptId(), str3);
                }
            } catch (Throwable th) {
                C0409e.m170b(f40a, "Error in savePendingReceipt: " + th);
            }
        }
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws Exception {
        Receipt receipt;
        boolean z = false;
        Map data = successResult.getData();
        C0409e.m168a(f40a, "data: " + data);
        String str = (String) getCommandData().get("requestId");
        String str2 = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str3 = (String) data.get("marketplace");
        String str4 = (String) data.get("receipt");
        if (str == null || !C0397b.m126a().mo6249a(str)) {
            mo6233b().mo6221d().mo6229b();
            return true;
        } else if (C0408d.m167a(str4)) {
            mo6207a(str2, str3, str, RequestStatus.FAILED);
            return z;
        } else {
            JSONObject jSONObject = new JSONObject(str4);
            RequestStatus safeValueOf = RequestStatus.safeValueOf(jSONObject.getString("orderStatus"));
            if (safeValueOf == RequestStatus.SUCCESSFUL) {
                try {
                    Receipt a = C0404a.m154a(jSONObject, str2, str);
                    if (ProductType.CONSUMABLE == a.getProductType()) {
                        m41a(str, str2, str4);
                    }
                    receipt = a;
                } catch (Throwable th) {
                    mo6207a(str2, str3, str, RequestStatus.FAILED);
                    return z;
                }
            } else {
                receipt = null;
            }
            C0378e b = mo6233b();
            b.mo6221d().mo6227a((Object) new PurchaseResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(safeValueOf).setUserData(new UserDataBuilder().setUserId(str2).setMarketplace(str3).build()).setReceipt(receipt).build());
            return true;
        }
    }
}
