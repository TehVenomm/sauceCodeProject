package com.amazon.device.iap.internal.p005b.p009d;

import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0357a;
import com.amazon.device.iap.internal.p005b.C0373d;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.util.C0404a;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.d.c */
public final class C0376c extends C0375b {

    /* renamed from: b */
    private static final String f58b = C0376c.class.getSimpleName();

    public C0376c(C0378e eVar, boolean z) {
        super(eVar, "2.0", z);
    }

    /* renamed from: a */
    private List<Receipt> m65a(String str, String str2, String str3) throws JSONException {
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArray = new JSONArray(str2);
        for (int i = 0; i < jSONArray.length(); i++) {
            try {
                arrayList.add(C0404a.m154a(jSONArray.getJSONObject(i), str, str3));
            } catch (C0357a e) {
                C0409e.m170b(f58b, "fail to parse receipt, requestId:" + e.mo6205a());
            } catch (C0373d e2) {
                C0409e.m170b(f58b, "fail to verify receipt, requestId:" + e2.mo6215a());
            } catch (Throwable th) {
                C0409e.m170b(f58b, "fail to verify receipt, requestId:" + th.getMessage());
            }
        }
        return arrayList;
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws Exception {
        Map data = successResult.getData();
        C0409e.m168a(f58b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str2 = (String) data.get("marketplace");
        List a = m65a(str, (String) data.get("receipts"), (String) data.get("requestId"));
        String str3 = (String) data.get("cursor");
        boolean booleanValue = Boolean.valueOf((String) data.get("hasMore")).booleanValue();
        C0378e b = mo6233b();
        PurchaseUpdatesResponse build = new PurchaseUpdatesResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str2).build()).setReceipts(a).setHasMore(booleanValue).build();
        b.mo6221d().mo6228a("newCursor", str3);
        b.mo6221d().mo6227a((Object) build);
        return true;
    }
}
