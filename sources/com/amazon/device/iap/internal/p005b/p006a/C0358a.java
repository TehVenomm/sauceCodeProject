package com.amazon.device.iap.internal.p005b.p006a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.util.C0404a;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.a.a */
public final class C0358a extends C0360c {

    /* renamed from: a */
    private static final String f39a = C0358a.class.getSimpleName();

    public C0358a(C0378e eVar) {
        super(eVar, "2.0");
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws Exception {
        Receipt receipt;
        Map data = successResult.getData();
        C0409e.m168a(f39a, "data: " + data);
        String str = (String) getCommandData().get("requestId");
        String str2 = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str3 = (String) data.get("marketplace");
        String str4 = (String) data.get("receipt");
        if (C0408d.m167a(str4)) {
            mo6207a(str2, str3, str, RequestStatus.FAILED);
            return false;
        }
        JSONObject jSONObject = new JSONObject(str4);
        RequestStatus safeValueOf = RequestStatus.safeValueOf(jSONObject.getString("orderStatus"));
        if (safeValueOf == RequestStatus.SUCCESSFUL) {
            try {
                receipt = C0404a.m154a(jSONObject, str2, str);
            } catch (Throwable th) {
                mo6207a(str2, str3, str, RequestStatus.FAILED);
                return false;
            }
        } else {
            receipt = null;
        }
        C0378e b = mo6233b();
        b.mo6221d().mo6227a((Object) new PurchaseResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(safeValueOf).setUserData(new UserDataBuilder().setUserId(str2).setMarketplace(str3).build()).setReceipt(receipt).build());
        return true;
    }
}
