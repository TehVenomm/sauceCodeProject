package com.amazon.device.iap.internal.p001b.p005d;

import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.p001b.C0199a;
import com.amazon.device.iap.internal.p001b.C0215d;
import com.amazon.device.iap.internal.util.C0240a;
import com.amazon.device.iap.internal.util.C0244e;
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
public final class C0213c extends C0212b {
    /* renamed from: b */
    private static final String f51b = C0213c.class.getSimpleName();

    public C0213c(C0197e c0197e, boolean z) {
        super(c0197e, "2.0", z);
    }

    /* renamed from: a */
    private List<Receipt> m96a(String str, String str2, String str3) throws JSONException {
        List<Receipt> arrayList = new ArrayList();
        JSONArray jSONArray = new JSONArray(str2);
        for (int i = 0; i < jSONArray.length(); i++) {
            try {
                arrayList.add(C0240a.m159a(jSONArray.getJSONObject(i), str, str3));
            } catch (C0199a e) {
                C0244e.m175b(f51b, "fail to parse receipt, requestId:" + e.m77a());
            } catch (C0215d e2) {
                C0244e.m175b(f51b, "fail to verify receipt, requestId:" + e2.m99a());
            } catch (Throwable th) {
                C0244e.m175b(f51b, "fail to verify receipt, requestId:" + th.getMessage());
            }
        }
        return arrayList;
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws Exception {
        Map data = successResult.getData();
        C0244e.m173a(f51b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str2 = (String) data.get("marketplace");
        List a = m96a(str, (String) data.get("receipts"), (String) data.get("requestId"));
        String str3 = (String) data.get("cursor");
        boolean booleanValue = Boolean.valueOf((String) data.get("hasMore")).booleanValue();
        C0197e b = m58b();
        Object build = new PurchaseUpdatesResponseBuilder().setRequestId(b.m72c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str2).build()).setReceipts(a).setHasMore(booleanValue).build();
        b.m73d().m118a("newCursor", str3);
        b.m73d().m117a(build);
        return true;
    }
}
