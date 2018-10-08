package com.amazon.device.iap.internal.p005b.p009d;

import com.amazon.device.iap.internal.model.PurchaseUpdatesResponseBuilder;
import com.amazon.device.iap.internal.model.ReceiptBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p005b.C0197e;
import com.amazon.device.iap.internal.p005b.C0199a;
import com.amazon.device.iap.internal.p005b.C0215d;
import com.amazon.device.iap.internal.p014c.C0231a;
import com.amazon.device.iap.internal.p014c.C0233c;
import com.amazon.device.iap.internal.util.C0240a;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseUpdatesResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import com.facebook.internal.ServerProtocol;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONException;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.d.d */
public final class C0214d extends C0212b {
    /* renamed from: b */
    private static final String f52b = C0214d.class.getSimpleName();
    /* renamed from: c */
    private static final Date f53c = new Date(0);

    public C0214d(C0197e c0197e) {
        super(c0197e, "1.0", true);
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws Exception {
        String string;
        int i = 0;
        Map data = successResult.getData();
        C0244e.m173a(f52b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str2 = (String) data.get("requestId");
        str2 = (String) data.get("marketplace");
        List arrayList = new ArrayList();
        JSONArray jSONArray = new JSONArray((String) data.get("receipts"));
        for (int i2 = 0; i2 < jSONArray.length(); i2++) {
            try {
                Receipt a = C0240a.m159a(jSONArray.getJSONObject(i2), str, null);
                arrayList.add(a);
                if (ProductType.ENTITLED == a.getProductType()) {
                    C0233c.m134a().m136a(str, a.getReceiptId(), a.getSku());
                }
            } catch (C0199a e) {
                C0244e.m175b(f52b, "fail to parse receipt, requestId:" + e.m77a());
            } catch (C0215d e2) {
                C0244e.m175b(f52b, "fail to verify receipt, requestId:" + e2.m99a());
            } catch (Throwable th) {
                C0244e.m175b(f52b, "fail to verify receipt, requestId:" + th.getMessage());
            }
        }
        JSONArray jSONArray2 = new JSONArray((String) data.get("revocations"));
        while (i < jSONArray2.length()) {
            try {
                string = jSONArray2.getString(i);
                arrayList.add(new ReceiptBuilder().setSku(string).setProductType(ProductType.ENTITLED).setPurchaseDate(null).setCancelDate(f53c).setReceiptId(C0233c.m134a().m135a(str, string)).build());
            } catch (JSONException e3) {
                C0244e.m175b(f52b, "fail to parse JSON[" + i + "] in \"" + jSONArray2 + "\"");
            }
            i++;
        }
        string = (String) data.get("cursor");
        boolean equalsIgnoreCase = ServerProtocol.DIALOG_RETURN_SCOPES_TRUE.equalsIgnoreCase((String) data.get("hasMore"));
        C0197e b = m58b();
        Object build = new PurchaseUpdatesResponseBuilder().setRequestId(b.m72c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(new UserDataBuilder().setUserId(str).setMarketplace(str2).build()).setReceipts(arrayList).setHasMore(equalsIgnoreCase).build();
        build.getReceipts().addAll(C0231a.m120a().m129b(build.getUserData().getUserId()));
        b.m73d().m117a(build);
        b.m73d().m118a("newCursor", string);
        return true;
    }
}
