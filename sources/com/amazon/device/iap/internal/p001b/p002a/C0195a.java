package com.amazon.device.iap.internal.p001b.p002a;

import com.amazon.device.iap.internal.model.PurchaseResponseBuilder;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.util.C0240a;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.PurchaseResponse.RequestStatus;
import com.amazon.device.iap.model.Receipt;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.a.a */
public final class C0195a extends C0194c {
    /* renamed from: a */
    private static final String f29a = C0195a.class.getSimpleName();

    public C0195a(C0197e c0197e) {
        super(c0197e, "2.0");
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws Exception {
        Map data = successResult.getData();
        C0244e.m173a(f29a, "data: " + data);
        String str = (String) getCommandData().get("requestId");
        String str2 = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        String str3 = (String) data.get("marketplace");
        String str4 = (String) data.get("receipt");
        if (C0243d.m172a(str4)) {
            m62a(str2, str3, str, RequestStatus.FAILED);
            return false;
        }
        Receipt a;
        JSONObject jSONObject = new JSONObject(str4);
        RequestStatus safeValueOf = RequestStatus.safeValueOf(jSONObject.getString("orderStatus"));
        if (safeValueOf == RequestStatus.SUCCESSFUL) {
            try {
                a = C0240a.m159a(jSONObject, str2, str);
            } catch (Throwable th) {
                m62a(str2, str3, str, RequestStatus.FAILED);
                return false;
            }
        }
        a = null;
        C0197e b = m58b();
        b.m73d().m117a(new PurchaseResponseBuilder().setRequestId(b.m72c()).setRequestStatus(safeValueOf).setUserData(new UserDataBuilder().setUserId(str2).setMarketplace(str3).build()).setReceipt(a).build());
        return true;
    }
}
