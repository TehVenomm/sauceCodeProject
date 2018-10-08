package com.amazon.device.iap.internal.p001b.p006e;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.util.C0243d;
import com.amazon.device.iap.internal.util.C0244e;
import com.amazon.device.iap.model.UserData;
import com.amazon.device.iap.model.UserDataResponse.RequestStatus;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.e.d */
public final class C0220d extends C0218b {
    /* renamed from: b */
    private static final String f63b = C0220d.class.getSimpleName();

    public C0220d(C0197e c0197e) {
        super(c0197e, "1.0");
    }

    /* renamed from: a */
    protected boolean mo1187a(SuccessResult successResult) throws RemoteException, KiwiException {
        C0244e.m173a(f63b, "onSuccessInternal: result = " + successResult);
        Map data = successResult.getData();
        C0244e.m173a(f63b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        C0197e b = m58b();
        if (C0243d.m172a(str)) {
            b.m73d().m117a(new UserDataResponseBuilder().setRequestId(b.m72c()).setRequestStatus(RequestStatus.FAILED).build());
            return false;
        }
        UserData build = new UserDataBuilder().setUserId(str).setMarketplace(a).build();
        Object build2 = new UserDataResponseBuilder().setRequestId(b.m72c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(build).build();
        b.m73d().m118a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, build.getUserId());
        b.m73d().m117a(build2);
        return true;
    }
}
