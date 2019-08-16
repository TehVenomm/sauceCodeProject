package com.amazon.device.iap.internal.p005b.p009d;

import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0357a;
import com.amazon.device.iap.internal.p005b.C0373d;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.p014c.C0395a;
import com.amazon.device.iap.internal.p014c.C0398c;
import com.amazon.device.iap.internal.util.C0404a;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseUpdatesResponse;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import com.facebook.internal.ServerProtocol;
import java.util.ArrayList;
import java.util.Date;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.d.d */
public final class C0377d extends C0375b {

    /* renamed from: b */
    private static final String f59b = C0377d.class.getSimpleName();

    /* renamed from: c */
    private static final Date f60c = new Date(0);

    public C0377d(C0378e eVar) {
        super(eVar, "1.0", true);
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws Exception {
        Map data = successResult.getData();
        C0409e.m168a(f59b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str2 = (String) data.get("requestId");
        String str3 = (String) data.get("marketplace");
        ArrayList arrayList = new ArrayList();
        JSONArray jSONArray = new JSONArray((String) data.get("receipts"));
        for (int i = 0; i < jSONArray.length(); i++) {
            try {
                Receipt a = C0404a.m154a(jSONArray.getJSONObject(i), str, null);
                arrayList.add(a);
                if (ProductType.ENTITLED == a.getProductType()) {
                    C0398c.m129a().mo6252a(str, a.getReceiptId(), a.getSku());
                }
            } catch (C0357a e) {
                C0409e.m170b(f59b, "fail to parse receipt, requestId:" + e.mo6205a());
            } catch (C0373d e2) {
                C0409e.m170b(f59b, "fail to verify receipt, requestId:" + e2.mo6215a());
            } catch (Throwable th) {
                C0409e.m170b(f59b, "fail to verify receipt, requestId:" + th.getMessage());
            }
        }
        JSONArray jSONArray2 = new JSONArray((String) data.get("revocations"));
        for (int i2 = 0; i2 < jSONArray2.length(); i2++) {
            try {
                String string = jSONArray2.getString(i2);
                arrayList.add(new ReceiptBuilder().setSku(string).setProductType(ProductType.ENTITLED).setPurchaseDate(null).setCancelDate(f60c).setReceiptId(C0398c.m129a().mo6251a(str, string)).build());
            } catch (JSONException e3) {
                C0409e.m170b(f59b, "fail to parse JSON[" + i2 + "] in \"" + jSONArray2 + "\"");
            }
        }
        String str4 = (String) data.get("cursor");
        boolean equalsIgnoreCase = ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equalsIgnoreCase((String) data.get("hasMore"));
        C0378e b = mo6233b();
        PurchaseUpdatesResponse build = new PurchaseUpdatesResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str3).build()).setReceipts(arrayList).setHasMore(equalsIgnoreCase).build();
        build.getReceipts().addAll(C0395a.m115a().mo6246b(build.getUserData().getUserId()));
        b.mo6221d().mo6227a((Object) build);
        b.mo6221d().mo6228a("newCursor", str4);
        return true;
    }
}
