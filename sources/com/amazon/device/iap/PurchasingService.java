package com.amazon.device.iap;

import android.content.Context;
import android.util.Log;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.C0402e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

public final class PurchasingService {
    public static final String BUILD_VERSION = "2.0.46.0";
    public static final boolean IS_SANDBOX_MODE = C0402e.m149a();
    public static final String SDK_VERSION = "2.0.1";
    private static final String TAG = PurchasingService.class.getSimpleName();

    private PurchasingService() {
        Log.i(TAG, "In-App Purchasing SDK initializing. SDK Version 2.0.1/Build No 2.0.46.0, IS_SANDBOX_MODE: " + IS_SANDBOX_MODE);
    }

    public static RequestId getProductData(Set<String> set) {
        return C0401d.m137d().mo6259a(set);
    }

    public static RequestId getPurchaseUpdates(boolean z) {
        return C0401d.m137d().mo6260a(z);
    }

    public static RequestId getUserData() {
        return C0401d.m137d().mo6265c();
    }

    public static void notifyFulfillment(String str, FulfillmentResult fulfillmentResult) {
        C0401d.m137d().mo6263a(str, fulfillmentResult);
    }

    public static RequestId purchase(String str) {
        return C0401d.m137d().mo6258a(str);
    }

    public static void registerListener(Context context, PurchasingListener purchasingListener) {
        C0401d.m137d().mo6262a(context, purchasingListener);
    }
}
