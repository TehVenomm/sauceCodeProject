package com.amazon.device.iap.internal.p005b.p010e;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.model.UserDataBuilder;
import com.amazon.device.iap.internal.model.UserDataResponseBuilder;
import com.amazon.device.iap.internal.p005b.C0378e;
import com.amazon.device.iap.internal.util.C0408d;
import com.amazon.device.iap.internal.util.C0409e;
import com.amazon.device.iap.model.UserData;
import com.amazon.device.iap.model.UserDataResponse;
import com.amazon.device.iap.model.UserDataResponse.RequestStatus;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

/* renamed from: com.amazon.device.iap.internal.b.e.d */
public final class C0383d extends C0381b {

    /* renamed from: b */
    private static final String f71b = C0383d.class.getSimpleName();

    public C0383d(C0378e eVar) {
        super(eVar, "1.0");
    }

    /* access modifiers changed from: protected */
    /* renamed from: a */
    public boolean mo6206a(SuccessResult successResult) throws RemoteException, KiwiException {
        C0409e.m168a(f71b, "onSuccessInternal: result = " + successResult);
        Map data = successResult.getData();
        C0409e.m168a(f71b, "data: " + data);
        String str = (String) data.get(AmazonAppstoreBillingService.JSON_KEY_USER_ID);
        C0378e b = mo6233b();
        if (C0408d.m167a(str)) {
            b.mo6221d().mo6227a((Object) new UserDataResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(RequestStatus.FAILED).build());
            return false;
        }
        UserData build = new UserDataBuilder().setUserId(str).setMarketplace(f69a).build();
        UserDataResponse build2 = new UserDataResponseBuilder().setRequestId(b.mo6220c()).setRequestStatus(RequestStatus.SUCCESSFUL).setUserData(build).build();
        b.mo6221d().mo6228a(AmazonAppstoreBillingService.JSON_KEY_USER_ID, build.getUserId());
        b.mo6221d().mo6227a((Object) build2);
        return true;
    }
}
