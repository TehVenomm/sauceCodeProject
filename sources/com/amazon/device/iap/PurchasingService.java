package com.amazon.device.iap;

import android.content.Context;
import android.util.Log;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.C0237e;
import com.amazon.device.iap.model.FulfillmentResult;
import com.amazon.device.iap.model.RequestId;
import java.util.Set;

public final class PurchasingService {
    public static final String BUILD_VERSION = "2.0.46.0";
    public static final boolean IS_SANDBOX_MODE = C0237e.m154a();
    public static final String SDK_VERSION = "2.0.1";
    private static final String TAG = PurchasingService.class.getSimpleName();

    private PurchasingService() {
        Log.i(TAG, "In-App Purchasing SDK initializing. SDK Version 2.0.1/Build No 2.0.46.0, IS_SANDBOX_MODE: " + IS_SANDBOX_MODE);
    }

    public static RequestId getProductData(Set<String> set) {
        return C0236d.m142d().m146a((Set) set);
    }

    public static RequestId getPurchaseUpdates(boolean z) {
        return C0236d.m142d().m147a(z);
    }

    public static RequestId getUserData() {
        return C0236d.m142d().m152c();
    }

    public static void notifyFulfillment(String str, FulfillmentResult fulfillmentResult) {
        C0236d.m142d().m150a(str, fulfillmentResult);
    }

    public static RequestId purchase(String str) {
        return C0236d.m142d().m145a(str);
    }

    public static void registerListener(Context context, PurchasingListener purchasingListener) {
        C0236d.m142d().m149a(context, purchasingListener);
    }
}
