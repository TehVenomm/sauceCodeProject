package com.amazon.device.iap.internal.p001b.p002a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.p010c.C0231a;
import com.amazon.device.iap.internal.p010c.C0232b;
import com.amazon.device.iap.internal.util.C0240a;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.ProductType;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.a.b */
public final class C0196b extends C0194c {
    /* renamed from: a */
    private static final String f30a = C0196b.class.getSimpleName();

    public C0196b(C0197e c0197e) {
        super(c0197e, "1.0");
    }

    /* renamed from: a */
    private void m64a(String str, String str2, String str3) {
        if (str != null && str2 != null && str3 != null) {
            try {
                JSONObject jSONObject = new JSONObject(str3);
                if (RequestStatus.safeValueOf(jSONObject.getString("orderStatus")) == RequestStatus.SUCCESSFUL) {
                    C0231a.m120a().m128a(str, str2, C0240a.m159a(jSONObject, str2, str).getReceiptId(), str3);
                }
            } catch (Throwable th) {
                C0244e.m175b(f30a, "Error in savePendingReceipt: " + th);
            }
        }
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws Exception {
        boolean z = false;
        Map data = successResult.getData();
        C0244e.m173a(f30a, "data: " + data);
        String str = (String) getCommandData().get("requestId");
        String str2 = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str3 = (String) data.get("marketplace");
        String str4 = (String) data.get("receipt");
        if (str == null || !C0232b.m131a().m132a(str)) {
            m58b().m73d().m119b();
            return true;
        } else if (C0243d.m172a(str4)) {
            m62a(str2, str3, str, RequestStatus.FAILED);
            return z;
        } else {
            Receipt receipt;
            JSONObject jSONObject = new JSONObject(str4);
            RequestStatus safeValueOf = RequestStatus.safeValueOf(jSONObject.getString("orderStatus"));
            if (safeValueOf == RequestStatus.SUCCESSFUL) {
                try {
                    Receipt a = C0240a.m159a(jSONObject, str2, str);
                    if (ProductType.CONSUMABLE == a.getProductType()) {
                        m64a(str, str2, str4);
                    }
                    receipt = a;
                } catch (Throwable th) {
                    m62a(str2, str3, str, RequestStatus.FAILED);
                    return z;
                }
            }
            receipt = null;
            C0197e b = m58b();
            b.m73d().m117a(new PurchaseResponseBuilder().setRequestId(b.m72c()).setRequestStatus(safeValueOf).setUserData(new UserDataBuilder().setUserId(str2).setMarketplace(str3).build()).setReceipt(receipt).build());
            return true;
        }
    }
}
